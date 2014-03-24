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
    public class BehaveUser : ICrudOrmItem
    {
        public const int DEFAULT_GLOBAL_USERID = 2; // Single user initially, multi-user later

        public int? UserId { get { return DEFAULT_GLOBAL_USERID; }}

        public DbResult LoadFromDB(SqlConnection dbConn)
        {
            return DbResult.Okay;
        }

        public DbResult SaveToDB(SqlConnection dbConn)
        {
            return DbResult.Okay;
        }

        public DbResult DeleteFromDB(SqlConnection dbConn)
        {
            return DbResult.Okay;
        }
    }

    public class UserList : ICrudOrmList
    {
        public DbResult LoadFromDB(SqlConnection dbConn)
        {
            return DbResult.Okay;
        }
    }
}
