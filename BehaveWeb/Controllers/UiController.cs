using System;
using System.Data.SqlClient;
using System.Web.Mvc;
using Behave.BehaveCore.DataClasses;
using Behave.BehaveCore.DBUtils;
using Behave.BehaveWeb.Models;

namespace Behave.BehaveWeb.Controllers
{
    public class UiController : Controller
    {

        public ActionResult Index()
        {
            var defaultUser = new BehaveUser();
            var today = DateTime.Now;
            var vm = new DailyViewModel(today, defaultUser);

            using (SqlConnection conn = Connection.Create())
            {
                conn.Open();
                vm.PopulateData(dbConn: conn);
            }

            return View(vm);
        }

        public ActionResult Users()
        {
            return View();
        }

        public ActionResult Habits()
        {
            return View();
        }

        public ActionResult Occurrences()
        {
            return View();
        }
    }
}
