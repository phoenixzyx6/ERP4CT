
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Resources;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class ConStrAssessController : BaseController<ConStrAssess, string>
    {
        public override System.Web.Mvc.ActionResult Add(ConStrAssess csa)
        {
            return base.Add(csa);
        }

        public override System.Web.Mvc.ActionResult Update(ConStrAssess csa)
        {
            return base.Update(csa);
        }

        public ActionResult Stat(string id)
        {
            bool result = this.service.ConStrAssess.HandleStat(id);
            if (result)
            {
                return OperateResult(result, Lang.Msg_Operate_Success, result);
            }
            else
            {
                return OperateResult(result, Lang.Msg_Operate_Failed, result);
            }
        }

        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.ConStrength = HelperExtensions.SelectListData<ZLERP.Model.ConStrength>
                ("ConStrengthCode", "ConStrengthCode", "ID", true);
            return base.Index();
        }
    }    
}
