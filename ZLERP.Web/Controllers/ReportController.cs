using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZLERP.Web.Controllers
{
    public class ReportController : Controller
    {
        
        public ActionResult Index(string path, string report)
        {
            ViewBag.ReportUrl = string.Format("~/Reports/{0}/{1}.aspx", path,report);
            if (!System.IO.File.Exists(Server.MapPath(ViewBag.ReportUrl)))
            {//避免提示xxx.aspx不存在的问题
                ViewBag.ReportUrl = "javascript:void 0";
               
            }
            return View();
        }

        public ActionResult Report()
        {
            string currentid = Request.QueryString["f"];
            return View();
        }     

    }
}
