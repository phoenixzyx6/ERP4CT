
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class GoodsInvalidateController : BaseController<GoodsInvalidate, string>
    {
        public override ActionResult Index()
        {
            ViewBag.GoodsInfo = HelperExtensions.SelectListData<GoodsInfo>("GoodsName", "ID", "1=1", "ID", true, "");
            return base.Index();
        }
    }    
}
