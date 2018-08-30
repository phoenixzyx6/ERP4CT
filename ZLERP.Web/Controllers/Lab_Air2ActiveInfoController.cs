using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class Lab_Air2ActiveInfoController : BaseController<Lab_Air2ActiveInfo, int?>
    {
        public override ActionResult Add(Lab_Air2ActiveInfo Lab_Air2ActiveInfo)
        {
            return base.Add(Lab_Air2ActiveInfo);
        }
        public override ActionResult Update(Lab_Air2ActiveInfo Lab_Air2ActiveInfo)
        {
            return base.Update(Lab_Air2ActiveInfo);
        }

    }
}
