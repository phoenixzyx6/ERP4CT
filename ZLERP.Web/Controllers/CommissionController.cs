
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
    public class CommissionController : BaseController<Commission, string>
    {
        public override System.Web.Mvc.ActionResult Add(Commission Commission)
        {
            ProduceTask task = this.service.ProduceTask.Get(Commission.TaskID);
            task.IsDatum = true;
            this.service.ProduceTask.Update(task, null);

            return base.Add(Commission);
        }

        public override System.Web.Mvc.ActionResult Update(Commission Commission)
        {
            return base.Update(Commission);
        }

        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.ConStrength = HelperExtensions.SelectListData<ZLERP.Model.ConStrength>
                ("ConStrengthCode", "ConStrengthCode", "ID", true);

            ViewBag.Formula = HelperExtensions.SelectListData<ZLERP.Model.Formula>
                ("FormulaName", "ID", "ID", true);
            ViewBag.CustMixprop = HelperExtensions.SelectListData<ZLERP.Model.CustMixprop>
                ("MixpropCode", "ID", "ID", true);
            return base.Index();
        }

        
        public override System.Web.Mvc.ActionResult Delete(string[] id)
        {
            foreach (string str in id)
            {
                Commission obj = this.service.GetGenericService<Commission>().Get(str);
                ProduceTask task = this.service.ProduceTask.Get(obj.TaskID);
                task.IsDatum = false;
                this.service.ProduceTask.Update(task);
            }
            return base.Delete(id);
        }
    }    
}
