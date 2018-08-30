
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
    public class EquipmentController : BaseController<Equipment, string>
    {
        public override ActionResult Add(Equipment Equipment)
        {
            return base.Add(Equipment);
        }

        public override ActionResult Update(Equipment Equipment)
        {
            return base.Update(Equipment);
        }
        public override ActionResult Index()
        {
            var supplyInfoList = this.service.GetGenericService<SupplyInfo>().All("SupplyKind = 'Su4'", "ID", true);
            ViewBag.supplyInfoList = new SelectList(supplyInfoList, "ID", "SupplyName");
            var classBList = this.service.GetGenericService<ClassB>().All("ClassID='EquipType'", "ID", true);
            ViewBag.ClassBList = new SelectList(classBList, "ID", "ClassBName");
            var departmentList = this.service.GetGenericService<Department>().All("", "ID", true);
            ViewBag.departmentList = new SelectList(departmentList, "ID", "DepartmentName");
            return base.Index();
        }
    }    
}
