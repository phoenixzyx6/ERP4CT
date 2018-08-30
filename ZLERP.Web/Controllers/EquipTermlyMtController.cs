using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ZLERP.Web.Controllers.Attributes;
using ZLERP.Model;
using System.Linq;
using ZLERP.Web.Helpers;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class EquipTermlyMtController : BaseController<EquipTermlyMt, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.ClassBEquip = HelperExtensions.SelectListData<ClassB>("ClassBName", "ID", "ClassID ='" + Request.QueryString["p1"] + "'", "ID", true, "");
            ViewBag.Users = HelperExtensions.SelectListData<User>("TrueName", "ID", "", "ID", true, "");
            ViewBag.Departments = HelperExtensions.SelectListData<Department>("DepartmentName", "ID", "", "ID", true, "");
            ViewBag.PartInfo = HelperExtensions.SelectListData<PartInfo>("PartName", "ID", "", "ID", true, "");
            return base.Index();
        }

        public override ActionResult Add(EquipTermlyMt EquipTermlyMt)
        {
            return base.Add(EquipTermlyMt);
        }

        public override ActionResult Update(EquipTermlyMt EquipTermlyMt)
        {
            if (ModelState.IsValid)
            {
                return base.Update(EquipTermlyMt);
            }
            else
            {
                var m = ModelState.Values.Where(p => p.Errors.Count > 0)
                    .Select(c => string.Join(",", c.Errors.Select(p => p.ErrorMessage).ToList())).ToList();

                return OperateResult(false, string.Join("<br/>", m), null);
            }
        }
    }    
}
