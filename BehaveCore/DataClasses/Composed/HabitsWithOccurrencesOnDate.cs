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
    public class HabitsWithOccurrencesOnDate : ICrudOrmList
    {
        public HabitsWithOccurrencesOnDate(User user, DateTime date)
        {
            _user = user;
            _date = date;
            HabitList = new List<HabitsWithOccurrences>();
        }
        private User _user;
        private DateTime _date;

        public List<HabitsWithOccurrences> HabitList { get; set; }

        public DbResult LoadFromDB(System.Data.SqlClient.SqlConnection dbConn)
        {
            throw new NotImplementedException();
        }
    }
}
