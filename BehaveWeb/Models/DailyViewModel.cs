using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Behave.BehaveCore.DataClasses;

namespace Behave.BehaveWeb.Models
{
    public class DailyViewModel : ViewModelBase
    {
        public DailyViewModel(DateTime requestedDate, BehaveUser user)
        {
            CurrentUser = user;

            // Change DateTime to midnight on requested day
            DailyDate = new DateTime(
                year: requestedDate.Year,
                month: requestedDate.Month,
                day: requestedDate.Day
            );

            HabitsWithOccurrences = new List<SingleHabitWithOccurrences>();
        }
        public DailyViewModel() : this(DateTime.Now, null) { }

        public DateTime DailyDate { get; set; }
        public DateTime NextDay { get { return DailyDate.AddDays(1); } }
        public DateTime PriorDay { get { return DailyDate.AddDays(-1); } } 

        public BehaveUser CurrentUser { get; set; }
        public List<SingleHabitWithOccurrences> HabitsWithOccurrences { get; set; }

        public void PopulateData(SqlConnection dbConn)
        {
            int userId = CurrentUser.UserId ?? BehaveUser.DEFAULT_GLOBAL_USERID;

            var habitList = new HabitList() { 
                UserId = userId
            };
            habitList.LoadFromDB(dbConn);

            var occurrenceList = new OccurrenceList(
                userId: userId,
                beginTime: DailyDate
            );
            occurrenceList.LoadFromDB(dbConn);

            foreach (var habit in habitList.Habits)
            {
                var currentSingleHabitWithOccurrences = new SingleHabitWithOccurrences();
                currentSingleHabitWithOccurrences.Habit = habit;

                foreach (var occurrence in occurrenceList.Occurrences)
                {
                    if (occurrence.HabitId == habit.HabitId)
                    {
                        currentSingleHabitWithOccurrences.Occurrences.Add(occurrence);
                    }
                }

                HabitsWithOccurrences.Add(currentSingleHabitWithOccurrences);
            }
        }
    }
}