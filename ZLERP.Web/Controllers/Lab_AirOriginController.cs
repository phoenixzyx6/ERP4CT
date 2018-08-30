using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class Lab_AirOriginController : BaseController<Lab_AirOrigin, string>
    {
        public override ActionResult Add(Lab_AirOrigin Lab_AirOrigin)
        {
            return base.Add(Lab_AirOrigin);
        }
        public override ActionResult Update(Lab_AirOrigin Lab_AirOrigin)
        {
            return base.Update(Lab_AirOrigin);
        }
    }
}
