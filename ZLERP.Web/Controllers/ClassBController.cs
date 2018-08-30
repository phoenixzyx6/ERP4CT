
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
    public class ClassBController : BaseController<ClassB, string>
    {
        public ActionResult ClassType(string id) {
            base.InitCommonViewBag();
            ViewBag.classType = id;
            return View();
        }

        public override ActionResult Add(ClassB ClassB)
        {
            return base.Add(ClassB);
        }
    }    
}
