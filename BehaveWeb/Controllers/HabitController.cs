using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Web.Http;
using Behave.BehaveCore.DataClasses;
using Behave.BehaveCore.DBUtils;

namespace Behave.BehaveWeb.Controllers
{
    public class HabitController : ApiController
    {
        // GET api/habit
        public HabitList Get()
        {
            var habitList = new HabitList();

            using (SqlConnection conn = Connection.Create())
            {
                conn.Open();

                DbResult result = habitList.LoadFromDB(conn);
                switch (result)
                {
                    case DbResult.Okay:
                        return habitList;
                    case DbResult.NotFound:
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    default:
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }
        }

        // GET api/habit/5
        public Habit Get(int id)
        {
            var habit = new Habit();
            habit.HabitId = id;

            using (SqlConnection conn = Connection.Create())
            {
                conn.Open();

                DbResult result = habit.LoadFromDB(conn);
                switch (result)
                {
                    case DbResult.Okay:
                        return habit;
                    case DbResult.NotFound:
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    default:
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }
        }

        // POST api/habit
        public int Post([FromBody] Habit habit) // Create
        {
            habit.HabitId = null;
            using (SqlConnection conn = Connection.Create())
            {
                conn.Open();

                DbResult result = habit.SaveToDB(conn);
                switch (result)
                {
                    case DbResult.Okay:
                    case DbResult.NotFound:
                        break;
                    default:
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }
            return habit.HabitId ?? -1;
        }

        // PUT api/habit/5 
        public void Put(int id, [FromBody]Habit habit) // Update
        {
            habit.HabitId = id;
            using (SqlConnection conn = Connection.Create())
            {
                conn.Open();

                DbResult result = habit.SaveToDB(conn);
                switch (result)
                {
                    case DbResult.Okay:
                    case DbResult.NotFound:
                        break;
                    default:
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }
        }

        // DELETE api/habit/5
        public void Delete(int id)
        {
            var habit = new Habit();
            habit.HabitId = id;
            using (SqlConnection conn = Connection.Create())
            {
                conn.Open();

                DbResult result = habit.DeleteFromDB(conn);
                switch (result)
                {
                    case DbResult.Okay:
                    case DbResult.NotFound:
                        break;
                    default:
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }
        }
    }
}
