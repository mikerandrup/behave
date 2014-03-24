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
    public class Habit : ICrudOrmItem
    {
        public int? HabitId { get; set; }
        public int UserId { get; set; }
        public float Importance { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }

        public DbResult LoadFromDB(SqlConnection dbConn)
        {
            DbResult resultCode = DbResult.Unknown;

            try
            {
                SqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Habits WHERE HabitId = @identity";

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@identity",
                    Value = HabitId
                });

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        UserId = (int)reader["UserId"];
                        Importance = (float)reader["Importance"];
                        Title = reader["Title"].ToString();
                        Details = reader["Details"].ToString();

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

            return resultCode;
        }

        public DbResult SaveToDB(SqlConnection dbConn)
        {
            if (HabitId == null) // Create
            {
                var query = new StringBuilder();

                query.Append("INSERT INTO habits ")
                     .Append("(UserId, Importance, Title, Details) ")
                     .Append("VALUES (@UserId, @Importance, @Title, @Details); ")
                     .Append("SELECT @identity = cast(scope_identity() as int)");

                SqlCommand insertCommand = dbConn.CreateCommand();
                insertCommand.CommandText = query.ToString();
                insertCommand.Parameters.AddWithValue("@UserId", this.UserId);
                insertCommand.Parameters.AddWithValue("@Importance", this.Importance);
                insertCommand.Parameters.AddWithValue("@Title", this.Title);
                insertCommand.Parameters.AddWithValue("@Details", this.Details);
                insertCommand.Parameters.Add("@identity", SqlDbType.Int);
                insertCommand.Parameters["@identity"].Direction = ParameterDirection.Output;
                insertCommand.ExecuteNonQuery();

                this.HabitId = Convert.ToInt32(insertCommand.Parameters["@identity"].Value);
                
                return DbResult.Okay;
            }
            else // Update
            {
                var query = new StringBuilder();

                query.Append("UPDATE habits ")
                     .Append("SET UserId=@UserId, Importance=@Importance, Title=@Title, Details=@Details ")
                     .Append("WHERE HabitId=@HabitId");

                SqlCommand insertCommand = dbConn.CreateCommand();
                insertCommand.CommandText = query.ToString();
                insertCommand.Parameters.AddWithValue("@HabitId", this.HabitId);
                insertCommand.Parameters.AddWithValue("@UserId", this.UserId);
                insertCommand.Parameters.AddWithValue("@Importance", this.Importance);
                insertCommand.Parameters.AddWithValue("@Title", this.Title);
                insertCommand.Parameters.AddWithValue("@Details", this.Details);
                insertCommand.ExecuteNonQuery();

                return DbResult.Okay;
            }
        }

        public DbResult DeleteFromDB(SqlConnection dbConn)
        {
            DbResult result = DbResult.Unknown;

            try
            {
                SqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "UPDATE habits SET IsDeleted=false WHERE HabitId = @identity";

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@identity",
                    Value = HabitId
                });

                cmd.ExecuteNonQuery();
                result = DbResult.Okay;
            }

            #pragma warning disable 0168
            catch (SqlException exc)
            #pragma warning restore 0168
            {
                result = DbResult.Error;
            }

            return result;
        }
    }

    public class HabitList : ICrudOrmList
    {
        public HabitList()
        {
            Habits = new List<Habit>();
        }

        public int UserId = BehaveUser.DEFAULT_GLOBAL_USERID; // default user ID until we implement users
        public List<Habit> Habits { get; set; }

        public DbResult LoadFromDB(SqlConnection dbConn)
        {
            DbResult resultCode = DbResult.Unknown;

            try
            {
                SqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Habits WHERE UserId = @userId AND IsDeleted=false";

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@userId",
                    Value = UserId
                });

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new Habit();

                        item.HabitId = (int)reader["HabitId"];
                        item.UserId = (int)reader["UserId"];
                        item.Importance = (float)reader["Importance"];
                        item.Title = reader["Title"].ToString();
                        item.Details = reader["Details"].ToString();

                        Habits.Add(item);

                        resultCode = DbResult.Okay;
                    }
                }

                if (Habits.Count < 1)
                {
                    resultCode = DbResult.NotFound;
                }
                
            }

            #pragma warning disable 0168
            catch (SqlException exc)
            #pragma warning restore 0168
            {
                resultCode = DbResult.Error;
            }

            return resultCode;
        }
    }
}
