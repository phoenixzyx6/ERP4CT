
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
    public class QualityExamController : BaseController<QualityExam, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.ExamTypes = HelperExtensions.SelectListData<ZLERP.Model.Dic>
               ("DicName", "TreeCode", " ParentID='ExamType' and (TreeCode='FDE' OR TreeCode='FSJE' OR TreeCode='JSJE' OR TreeCode='KCE' OR TreeCode='KDE' OR TreeCode='KYE' OR TreeCode='KZE')", "ID", true, null);
            ViewBag.Commission = HelperExtensions.SelectListData<ZLERP.Model.Commission>
               ("ID", "ID", "", "ID", true, null);
            ViewBag.ConStrength = HelperExtensions.SelectListData<ZLERP.Model.ConStrength>
                ("ConStrengthCode", "ConStrengthCode", "ID", true);
            ViewBag.CustMixprop = HelperExtensions.SelectListData<ZLERP.Model.CustMixprop>
               ("MixpropCode", "ID", "", "ID", true, null);
            return base.Index();
        }

        public override System.Web.Mvc.ActionResult Add(QualityExam QualityExam)
        {
            return base.Add(QualityExam);
        }

        public override System.Web.Mvc.ActionResult Update(QualityExam QualityExam)
        {
            return base.Update(QualityExam);
        }

    }    
}
