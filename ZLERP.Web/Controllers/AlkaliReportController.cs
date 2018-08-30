
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
    public class AlkaliReportController : BaseController<AlkaliReport, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.WAStuff = HelperExtensions.SelectListData<ZLERP.Model.StuffInfo>
                ("StuffName", "StuffName", "IsUsed=1 and StuffTypeID='WA'", "StuffName", true, null);
            ViewBag.CEStuff = HelperExtensions.SelectListData<ZLERP.Model.StuffInfo>
                ("StuffName", "StuffName", "IsUsed=1 and StuffTypeID='CE'", "StuffName", true, null);
            ViewBag.FAStuff = HelperExtensions.SelectListData<ZLERP.Model.StuffInfo>
                ("StuffName", "StuffName", "IsUsed=1 and StuffTypeID like 'FA%'", "StuffName", true, null);
            ViewBag.CAStuff = HelperExtensions.SelectListData<ZLERP.Model.StuffInfo>
                ("StuffName", "StuffName", "IsUsed=1 and StuffTypeID like 'CA%'", "StuffName", true, null);
            ViewBag.AIRStuff = HelperExtensions.SelectListData<ZLERP.Model.StuffInfo>
                ("StuffName", "StuffName", "IsUsed=1 and StuffTypeID like 'AIR%'", "StuffName", true, null);
            ViewBag.ADMStuff = HelperExtensions.SelectListData<ZLERP.Model.StuffInfo>
                ("StuffName", "StuffName", "IsUsed=1 and StuffTypeID like 'ADM%'", "StuffName", true, null);
            ViewBag.CEExam = HelperExtensions.SelectListData<ZLERP.Model.CEExam>
                ("ID", "ID", "", "ID", true, null);
            ViewBag.FAExam = HelperExtensions.SelectListData<ZLERP.Model.FAExam>
               ("ID", "ID", "", "ID", true, null);
            ViewBag.CAExam = HelperExtensions.SelectListData<ZLERP.Model.CAExam>
               ("ID", "ID", "", "ID", true, null);
            ViewBag.AIR1Exam = HelperExtensions.SelectListData<ZLERP.Model.AIR1Exam>
               ("ID", "ID", "", "ID", true, null);
            ViewBag.AIR2Exam = HelperExtensions.SelectListData<ZLERP.Model.AIR2Exam>
               ("ID", "ID", "", "ID", true, null);
            ViewBag.ADMExam = HelperExtensions.SelectListData<ZLERP.Model.ADMExam>
               ("ID", "ID", "", "ID", true, null);
            ViewBag.ADM2Exam = HelperExtensions.SelectListData<ZLERP.Model.ADM2Exam>
               ("ID", "ID", "", "ID", true, null);
            return base.Index();
        }
    }    
}
