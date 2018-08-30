using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class Lab_BurntInfoController : BaseController<Lab_BurntInfo, int?>
    {
        public override ActionResult Add(Lab_BurntInfo Lab_BurntInfo)
        {
            return base.Add(Lab_BurntInfo);
        }
        public override ActionResult Update(Lab_BurntInfo Lab_BurntInfo)
        {
            return base.Update(Lab_BurntInfo);
        }
        
    }
}
