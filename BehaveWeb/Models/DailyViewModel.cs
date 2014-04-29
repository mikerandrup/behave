using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Behave.BehaveCore.DataClasses;
using System.Linq;
using System.Web.Script.Serialization;

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

        public enum Gestures
        {
            farLeft,
            slightLeft,
            tap,
            slightRight,
            farRight
        }

        private Dictionary<Gestures, OccurrenceType> _gestureRules;
        private Dictionary<Gestures, OccurrenceType> EventCodeByGestureRules
        {
            get
            {
                if (_gestureRules == null)
                {
                    _gestureRules = new Dictionary<Gestures, OccurrenceType>();
                    _gestureRules.Add(Gestures.farLeft, OccurrenceType.UnplannedNotComplete);
                    _gestureRules.Add(Gestures.slightLeft, OccurrenceType.PlannedNotComplete);
                    _gestureRules.Add(Gestures.tap, OccurrenceType.Completed);
                    _gestureRules.Add(Gestures.slightRight, OccurrenceType.Completed);
                    _gestureRules.Add(Gestures.farRight, OccurrenceType.PartiallyCompleted);
                }
                return _gestureRules;
            }
        }

        private Dictionary<OccurrenceType, bool> _reasonRules;
        private Dictionary<OccurrenceType, bool> RequireReasonByEventRules
        {
            get
            {
                if (_reasonRules == null)
                {
                    _reasonRules = new Dictionary<OccurrenceType, bool>();
                    _reasonRules.Add(OccurrenceType.UnplannedNotComplete, true);
                    _reasonRules.Add(OccurrenceType.PlannedNotComplete, true);
                    _reasonRules.Add(OccurrenceType.Completed, false);
                    _reasonRules.Add(OccurrenceType.PartiallyCompleted, true);
                }
                return _reasonRules;
            }
        }

        public struct GestureRule
        {
            public string GestureName;
            public int OccurrenceCode;
            public bool RequiresReason;
        }

        public List<GestureRule> GestureRuleSet
        {
            get
            {
                var ruleSet = new List<GestureRule>();

                foreach (KeyValuePair<Gestures, OccurrenceType> kvRule in EventCodeByGestureRules)
                {
                    ruleSet.Add(
                        new GestureRule()
                        {
                            GestureName = kvRule.Key.ToString(),
                            OccurrenceCode = (int)kvRule.Value,
                            RequiresReason = RequireReasonByEventRules[kvRule.Value]
                        }
                    );
                }

                return ruleSet;
            }
        }


        public DateTime DailyDate { get; set; }
        public DateTime NextDay { get { return DailyDate.AddDays(1); } }
        public DateTime PriorDay { get { return DailyDate.AddDays(-1); } }

        public BehaveUser CurrentUser { get; set; }
        public List<SingleHabitWithOccurrences> HabitsWithOccurrences { get; set; }

        public void PopulateData(SqlConnection dbConn)
        {
            int userId = CurrentUser.UserId ?? BehaveUser.DEFAULT_GLOBAL_USERID;

            var habitList = new HabitList()
            {
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