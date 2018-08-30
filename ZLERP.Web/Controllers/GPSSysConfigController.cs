using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Business;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class GPSSysConfigController : BaseController<SysConfig, int>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.SysConfigs = this.service.SysConfig.Query().Where(p => p.ConfigType == "GPS").ToList();
            return base.Index();
        }
    }
}
