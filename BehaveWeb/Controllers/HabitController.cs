using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Behave.BehaveCore.DataClasses;
using Behave.BehaveCore.DBUtils;

namespace Behave.BehaveWeb.Controllers
{
    public class HabitController : ApiController
    {
        // GET api/habit
        public List<Habit> Get()
        {
            var habitList = new HabitList();

            using (SqlConnection conn = Connection.Create())
            {
                DatabaseResultCode result = habitList.LoadFromDB(conn);
                switch (result)
                {
                    case DatabaseResultCode.okay:
                        return habitList.Habits;
                    case DatabaseResultCode.notFound:
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
                DatabaseResultCode result = habit.LoadFromDB(conn);
                switch (result)
                {
                    case DatabaseResultCode.okay:
                        return habit;
                    case DatabaseResultCode.notFound:
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    default:
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }
        }

        // POST api/habit
        public void Post([FromBody] Habit habit)
        {
            habit.HabitId = null;
            using (SqlConnection conn = Connection.Create())
            {
                DatabaseResultCode result = habit.SaveToDB(conn);
                switch (result)
                {
                    case DatabaseResultCode.okay:
                    case DatabaseResultCode.notFound:
                        break;
                    default:
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }
        }

        // PUT api/habit/5
        public void Put(int id, [FromBody]Habit habit)
        {
            habit.HabitId = id;
            using (SqlConnection conn = Connection.Create())
            {
                DatabaseResultCode result = habit.SaveToDB(conn);
                switch (result)
                {
                    case DatabaseResultCode.okay:
                    case DatabaseResultCode.notFound:
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
                DatabaseResultCode result = habit.DeleteFromDB(conn);
                switch (result)
                {
                    case DatabaseResultCode.okay:
                    case DatabaseResultCode.notFound:
                        break;
                    default:
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }
        }
    }
}
