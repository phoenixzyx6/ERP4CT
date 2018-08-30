
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
    public class StuffSellController : BaseController<StuffSell, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.StuffList = HelperExtensions.SelectListData<StuffInfo>("StuffName", "ID", "StuffName", true);

            return base.Index();
        }
    }    
}
