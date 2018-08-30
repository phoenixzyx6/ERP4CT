
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class LinkManRecordController : BaseController<LinkManRecord, int>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            //前场工长最近十天的行程记录
            var data = this.service.GetGenericService<LinkManRecord>().All(string.Format("BuildTime > '{0}'", DateTime.Today.AddDays(-10)), "BuildTime", false);
            return View(data);
        }
    }    
}
