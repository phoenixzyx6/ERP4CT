using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class SiloProductLineController : BaseController<SiloProductLine,int>
    {
        public override JsonResult ListData(string textField, string valueField, string orderBy = "ID", bool ascending = false, string condition = "")
        {
            //var data = this.m_ServiceBase.All(new List<string> { valueField, textField }, condition, orderBy, ascending);
            //var data = this.m_ServiceBase.All();
            var data = this.m_ServiceBase.Find(condition, 1, 100, orderBy, "");
            return Json(new SelectList(data, valueField, textField));
        }
        
    }
}
