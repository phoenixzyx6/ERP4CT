using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class Lab_FAController : BaseController<Lab_FA, int?>
    {
        public override ActionResult Update(Lab_FA Lab_FA)
        {
            return base.Update(Lab_FA);
        }
    }   

}
