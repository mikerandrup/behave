using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Behave.BehaveCore.DataClasses;

namespace Behave.BehaveWeb.Models
{
    public class DailyViewModel
    {
        public DailyViewModel()
        {
            Date = DateTime.Now;
            CurrentUser = new User();
            HabitsWithOccurrences = new HabitsWithOccurrencesOnDate(
                CurrentUser, Date
            );
        }

        public DateTime Date { get; set; }
        public User CurrentUser { get; set; }
        public HabitsWithOccurrencesOnDate HabitsWithOccurrences { get; set; }
    }
}