using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class Lab_Air2ReportController : BaseController<Lab_Air2Report, string>
    {

        public override ActionResult Update(Lab_Air2Report Lab_Air2Report)
        {
            return base.Update(Lab_Air2Report);
        }
    }
}
