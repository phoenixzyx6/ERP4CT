
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web.Mvc;

namespace ZLERP.Web.Controllers
{
    public class QualityExamItemController : BaseController<QualityExamItem, int>
    {
        public ActionResult AddWithQEId(string QEId)
        {
            QualityExamItem item = new QualityExamItem();
            item.QualityExamID = QEId;
            item.AgeTime = 0;
            item.DoTime = DateTime.Now;
            item.TestTime = DateTime.Now.AddDays(28);
            return base.Add(item);
        }

        public override ActionResult Update(QualityExamItem QualityExamItem)
        {
            return base.Update(QualityExamItem);
        }
    }    
}
