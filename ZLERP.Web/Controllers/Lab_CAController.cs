using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class Lab_CAController : BaseController<Lab_CA, int?>
    {
        public override ActionResult Update(Lab_CA Lab_CA)
        {
            return base.Update(Lab_CA);
        }
    }   

}
