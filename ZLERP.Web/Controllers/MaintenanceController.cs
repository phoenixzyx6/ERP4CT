
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class MaintenanceController : BaseController<Maintenance, string>
    {
        public override ActionResult Index()
        {
            base.InitCommonViewBag();
            return View();
        }

    }    
}
