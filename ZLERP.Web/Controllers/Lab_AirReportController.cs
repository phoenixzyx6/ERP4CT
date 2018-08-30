using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class Lab_AirReportController : BaseController<Lab_AirReport, string>
    {

        public override ActionResult Update(Lab_AirReport Lab_AirReport)
        {
            return base.Update(Lab_AirReport);
        }
    }
}
