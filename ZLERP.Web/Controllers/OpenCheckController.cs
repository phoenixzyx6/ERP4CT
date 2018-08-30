
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class OpenCheckController : BaseController<OpenCheck, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.WAStuff = HelperExtensions.SelectListData<ZLERP.Model.StuffInfo>
                ("StuffName", "StuffName", "IsUsed=1 and StuffTypeID='WA'", "StuffName", true, "");
            ViewBag.CEStuff = HelperExtensions.SelectListData<ZLERP.Model.StuffInfo>
                ("StuffName", "StuffName", "IsUsed=1 and StuffTypeID='CE'", "StuffName", true, "");
            ViewBag.FAStuff = HelperExtensions.SelectListData<ZLERP.Model.StuffInfo>
                ("StuffName", "StuffName", "IsUsed=1 and StuffTypeID like 'FA%'", "StuffName", true, "");
            ViewBag.CAStuff = HelperExtensions.SelectListData<ZLERP.Model.StuffInfo>
                ("StuffName", "StuffName", "IsUsed=1 and StuffTypeID like 'CA%'", "StuffName", true, "");
            ViewBag.CA1Stuff = HelperExtensions.SelectListData<ZLERP.Model.StuffInfo>
                ("StuffName", "StuffName", "IsUsed=1 and StuffTypeID like 'CA%'", "StuffName", true, "");
            ViewBag.AIRStuff = HelperExtensions.SelectListData<ZLERP.Model.StuffInfo>
                ("StuffName", "StuffName", "IsUsed=1 and StuffTypeID like 'AIR%'", "StuffName", true, "");
            ViewBag.ADMStuff = HelperExtensions.SelectListData<ZLERP.Model.StuffInfo>
                ("StuffName", "StuffName", "IsUsed=1 and StuffTypeID like 'ADM%'", "StuffName", true, "");

            ViewBag.WAExam = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("ID", "ID", "IsUsed=1", "ID", true, "");
            ViewBag.CEExam = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("ID", "ID", "IsUsed=1", "ID", true, "");
            ViewBag.FAExam = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("ID", "ID", "IsUsed=1", "ID", true, "");
            ViewBag.CAExam = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("ID", "ID", "IsUsed=1", "ID", true, "");
            ViewBag.CA1Exam = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("ID", "ID", "IsUsed=1", "ID", true, "");
            ViewBag.AIR1Exam = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("ID", "ID", "IsUsed=1", "ID", true, "");
            ViewBag.AIR2Exam = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("ID", "ID", "IsUsed=1", "ID", true, "");
            ViewBag.ADMExam = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("ID", "ID", "IsUsed=1", "ID", true, "");
            ViewBag.ADM2Exam = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("ID", "ID", "IsUsed=1", "ID", true, "");

            ViewBag.WASupply = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("SupplyID", "SupplyID", "IsUsed=1", "SupplyID", true, "");
            ViewBag.CESupply = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("SupplyID", "SupplyID", "IsUsed=1", "SupplyID", true, "");
            ViewBag.FASupply = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("SupplyID", "SupplyID", "IsUsed=1", "SupplyID", true, "");
            ViewBag.CASupply = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("SupplyID", "SupplyID", "IsUsed=1", "SupplyID", true, "");
            ViewBag.CA1Supply = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("SupplyID", "SupplyID", "IsUsed=1", "SupplyID", true, "");
            ViewBag.AIR1Supply = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("SupplyID", "SupplyID", "IsUsed=1", "SupplyID", true, "");
            ViewBag.AIR2Supply = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("SupplyID", "SupplyID", "IsUsed=1", "SupplyID", true, "");
            ViewBag.ADMSupply = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("SupplyID", "SupplyID", "IsUsed=1", "SupplyID", true, "");
            ViewBag.ADM2Supply = HelperExtensions.SelectListData<ZLERP.Model.StuffExam>("SupplyID", "SupplyID", "IsUsed=1", "SupplyID", true, "");

            return base.Index();
        }
    }
}
