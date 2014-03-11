using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Web.Http;
using Behave.BehaveCore.DataClasses;
using Behave.BehaveCore.DBUtils;

namespace Behave.BehaveWeb.Controllers
{
    public class OccurrenceController : ApiController
    {
        // GET api/habit
        public OccurrenceList Get()
        {
            var occurrenceList = new OccurrenceList(
                BehaveCore.DataClasses.User.DEFAULT_GLOBAL_USERID
            );

            using (SqlConnection conn = Connection.Create())
            {
                conn.Open();

                DbResult result = occurrenceList.LoadFromDB(conn);
                switch (result)
                {
                    case DbResult.Okay:
                        return occurrenceList;
                    case DbResult.NotFound:
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                        break;
                    default:
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }
        }

        // GET api/habit/5
        public Occurrence Get(int id)
        {
            var occurrence = new Occurrence();
            occurrence.OccurrenceId = id;

            using (SqlConnection conn = Connection.Create())
            {
                conn.Open();

                DbResult result = occurrence.LoadFromDB(conn);
                switch (result)
                {
                    case DbResult.Okay:
                        return occurrence;
                    case DbResult.NotFound:
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    default:
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }
        }

        // POST api/habit
        public int Post([FromBody] Occurrence occurrence) // Create
        {
            occurrence.OccurrenceId = null;
            using (SqlConnection conn = Connection.Create())
            {
                conn.Open();

                DbResult result = occurrence.SaveToDB(conn);
                switch (result)
                {
                    case DbResult.Okay:
                    case DbResult.NotFound:
                        break;
                    default:
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }
            return occurrence.OccurrenceId ?? -1;
        }

        // PUT api/habit/5 
        public void Put(int id, [FromBody]Occurrence occurrence) // Update
        {
            occurrence.OccurrenceId = id;
            using (SqlConnection conn = Connection.Create())
            {
                conn.Open();

                DbResult result = occurrence.SaveToDB(conn);
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
            var occurrence = new Occurrence();
            occurrence.OccurrenceId = id;
            using (SqlConnection conn = Connection.Create())
            {
                conn.Open();

                DbResult result = occurrence.DeleteFromDB(conn);
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
