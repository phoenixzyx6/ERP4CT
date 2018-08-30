
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class ConPriceController : BaseController<ConPrice, int>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.ConStrengthList = ZLERP.Web.Helpers.HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "ConStrengthCode", true);
            return base.Index();
        }
    }    
}
