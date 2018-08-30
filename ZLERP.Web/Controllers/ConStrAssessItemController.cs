
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
    public class ConStrAssessItemController : BaseController<ConStrAssessItem, int>
    {
        public override System.Web.Mvc.ActionResult Add(ConStrAssessItem csaitem)
        {
            return base.Add(csaitem);
        }

        public override System.Web.Mvc.ActionResult Update(ConStrAssessItem csaitem)
        {
            return base.Update(csaitem);
        }

        public ActionResult AddByPid(string pid)
        {
            ConStrAssessItem csaitem = new ConStrAssessItem();
            csaitem.ConStrAssessID = pid;
            csaitem.Exam1Str = 0;
            return base.Add(csaitem);
        }

    }    
}
