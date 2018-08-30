using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using ZLERP.Business;
using ZLERP.Web.Helpers;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class FinanceController : ServiceBasedController
    {

        public ActionResult Report(string id)
        {
            ViewBag.ReportUrl = "~/Reports/?rpt=" + id;
            return View();
        }
    }
}
