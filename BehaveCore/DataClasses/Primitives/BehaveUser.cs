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
    public class BehaveUser
    {
        public BehaveUser() : this(token: null) { }
        public BehaveUser(string token)
        {
            AuthToken = token;
        }

        public bool IsAuthenticated(SqlConnection dbConn)
        {
            bool isAuthenticated = false;

            if (LoadFromDB(dbConn) == DbResult.Okay) {
                isAuthenticated = true;
            }

            return isAuthenticated;
        }

        public const int DEFAULT_GLOBAL_USERID = 2; // Single user initially, multi-user later

        private int? _userId;
        public int? UserId
        {
            get
            {
                return _userId ?? DEFAULT_GLOBAL_USERID; // phasing in multi-user after single-user with auth
            }
            set
            {
                _userId = value; // TODO: get rid of areas where we set this
            }
        }
        public string AuthToken { get; set; }

        private DbResult LoadFromDB(SqlConnection dbConn)
        {
            DbResult resultCode = DbResult.NotFound;

            if (!String.IsNullOrWhiteSpace(AuthToken))
            {
                try
                {
                    SqlCommand cmd = dbConn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Users WHERE OathUserToken = @token";

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@token",
                        Value = AuthToken
                    });

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            UserId = (int)reader["UserId"];
                            resultCode = DbResult.Okay;
                        }
                        else
                        {
                            resultCode = DbResult.NotFound;
                        }
                    }
                }

                #pragma warning disable 0168
                catch (SqlException exc)
                #pragma warning restore 0168
                {
                    resultCode = DbResult.Error;
                }

            }

            return resultCode;
        }
    }

    public class UserList : ICrudOrmList
    {
        public DbResult LoadFromDB(SqlConnection dbConn)
        {
            throw new NotImplementedException();
            //return DbResult.Okay;
        }
    }
}
