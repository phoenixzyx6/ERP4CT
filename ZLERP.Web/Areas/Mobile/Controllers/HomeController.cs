using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Web.Controllers.Attributes;

namespace ZLERP.Web.Areas.Mobile.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Mobile/Home/
        [MobileAuthorizeAttribute]
        public ActionResult Index()
        {
            return View();
        }

    }
}
