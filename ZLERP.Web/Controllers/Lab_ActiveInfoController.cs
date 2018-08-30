using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class Lab_ActiveInfoController : BaseController<Lab_ActiveInfo, int?>
    {
        public override ActionResult Add(Lab_ActiveInfo Lab_ActiveInfo)
        {
            return base.Add(Lab_ActiveInfo);
        }
        public override ActionResult Update(Lab_ActiveInfo Lab_ActiveInfo)
        {
            return base.Update(Lab_ActiveInfo);
        }

    }
}
