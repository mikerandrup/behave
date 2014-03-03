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
    public class UiController : Controller
    {

        public ActionResult Index()
        {
            return View();
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
