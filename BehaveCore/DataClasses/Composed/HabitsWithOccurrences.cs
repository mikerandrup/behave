using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Behave.BehaveCore.DBUtils;

namespace Behave.BehaveCore.DataClasses
{
    public class HabitsWithOccurrences
    {
        public HabitsWithOccurrences(Habit habit)
        {
            _habit = habit;
            Occurrences = new List<Occurrence>();
        }
        private Habit _habit;
        public Habit Habit { get { return _habit; } }

        public List<Occurrence> Occurrences { get; set; }
    }
}
