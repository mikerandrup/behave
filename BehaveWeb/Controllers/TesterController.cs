using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Behave.BehaveCore;
using Behave.BehaveCore.DBUtils;

namespace Behave.BehaveWeb.Controllers
{
    public class TesterController : Controller
    {

        public ActionResult Database()
        {
            bool wasSuccessful = true;
            string helpfulMessage = "Nothing to report";

            try
            {
                using (SqlConnection conn = Connection.Create())
                {
                    conn.Open();
                }
            }
            catch (Exception exc)
            {
                helpfulMessage = exc.Message;
                wasSuccessful = false;
            }

            var jsonResult = new JsonResult();
            jsonResult.Data = new
            {
                success = wasSuccessful,
                message = helpfulMessage
            };
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return jsonResult;
        }

    }
}
