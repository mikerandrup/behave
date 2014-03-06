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
    public class Occurrence : ICrudOrmItem
    {
        public int? OccurrenceId { get; set; }
        public DateTime EventTime { get; set; }
        public int HabitId { get; set; }
        public string Notes { get; set; }

        public DbResult LoadFromDB(SqlConnection dbConn)
        {
            DbResult resultCode = DbResult.Unknown;

            try
            {
                SqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Occurrences WHERE OccurrenceId = @identity";

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@identity",
                    Value = OccurrenceId
                });

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        EventTime = (DateTime)reader["EventTime"];
                        HabitId = (int)reader["HabitId"];
                        Notes = reader["Notes"].ToString();

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
            if (OccurrenceId == null) // Create
            {
                var query = new StringBuilder();

                query.Append("INSERT INTO Occurrences ")
                     .Append("(EventTime, HabitId, Notes) ")
                     .Append("VALUES (@EventTime, @HabitId, @Notes); ")
                     .Append("SELECT @identity = cast(scope_identity() as int)");

                SqlCommand insertCommand = dbConn.CreateCommand();
                insertCommand.CommandText = query.ToString();
                insertCommand.Parameters.AddWithValue("@EventTime", this.EventTime);
                insertCommand.Parameters.AddWithValue("@HabitId", this.HabitId);
                insertCommand.Parameters.AddWithValue("@Notes", this.Notes);
                insertCommand.Parameters.Add("@identity", SqlDbType.Int);
                insertCommand.Parameters["@identity"].Direction = ParameterDirection.Output;
                insertCommand.ExecuteNonQuery();

                this.OccurrenceId = Convert.ToInt32(insertCommand.Parameters["@identity"].Value);
                
                return DbResult.Okay;
            }
            else // Update
            {
                var query = new StringBuilder();

                query.Append("UPDATE Occurrences ")
                     .Append("SET EventTime=@EventTime, HabitId=@HabitId, Notes=@Notes ")
                     .Append("WHERE OccurrenceId=@OccurrenceId");

                SqlCommand insertCommand = dbConn.CreateCommand();
                insertCommand.CommandText = query.ToString();
                insertCommand.Parameters.AddWithValue("@EventTime", this.EventTime);
                insertCommand.Parameters.AddWithValue("@HabitId", this.HabitId);
                insertCommand.Parameters.AddWithValue("@Notes", this.Notes);
                insertCommand.Parameters.AddWithValue("@OccurrenceId", this.OccurrenceId);
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
                cmd.CommandText = "DELETE FROM Occurrences WHERE OccurrenceId = @identity";

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@identity",
                    Value = OccurrenceId
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

    public class OccurrenceList : ICrudOrmList
    {
        public OccurrenceList()
        {
            Occurrences = new List<Occurrence>();
        }

        public List<Occurrence> Occurrences { get; set; }

        public DbResult LoadFromDB(SqlConnection dbConn)
        {
            DbResult resultCode = DbResult.Unknown;

            try
            {
                SqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Occurrences "; // WHERE UserId = @userId"; //TODO: Revisit schema to represent users in occurrences?

                //cmd.Parameters.Add(new SqlParameter
                //{
                //    ParameterName = "@userId",
                //    Value = UserId
                //});

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new Occurrence();

                        item.OccurrenceId = (int)reader["OccurrenceId"];
                        item.EventTime = (DateTime)reader["EventTIme"];
                        item.HabitId = (int)reader["HabitId"];
                        item.Notes = reader["Notes"].ToString();

                        Occurrences.Add(item);

                        resultCode = DbResult.Okay;
                    }
                }

                if (Occurrences.Count < 1)
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
