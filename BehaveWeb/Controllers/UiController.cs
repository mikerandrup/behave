﻿using System;
using System.Data.SqlClient;
using System.Web.Mvc;
using Behave.BehaveCore.DataClasses;
using Behave.BehaveCore.DBUtils;
using Behave.BehaveWeb.Models;

namespace Behave.BehaveWeb.Controllers
{
    [Authorize]
    public class UiController : Controller
    {
        public ActionResult Index(DateTime? date)
        {
            DailyViewModel vm = PrepareDailyViewModel(date);
            return View(vm);
        }

        public ActionResult Legacy(DateTime? date)
        {
            DailyViewModel vm = PrepareDailyViewModel(date);
            return View(vm);
        }

        private static DailyViewModel PrepareDailyViewModel(DateTime? date)
        {
            var defaultUser = new BehaveUser();

            var vm = new DailyViewModel(date ?? DateTime.Now, defaultUser);

            using (SqlConnection conn = Connection.Create())
            {
                conn.Open();
                vm.PopulateData(dbConn: conn);
            }
            return vm;
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
