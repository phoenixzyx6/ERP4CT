using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class Lab_Air2OriginController : BaseController<Lab_Air2Origin, string>
    {
        public override ActionResult Add(Lab_Air2Origin Lab_Air2Origin)
        {
            return base.Add(Lab_Air2Origin);
        }
        public override ActionResult Update(Lab_Air2Origin Lab_Air2Origin)
        {
            return base.Update(Lab_Air2Origin);
        }
    }
}
