using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class Lab_ADMController : BaseController<Lab_ADM, int?>
    {
        public override ActionResult Update(Lab_ADM Lab_ADM)
        {
            return base.Update(Lab_ADM);
        }
    }   

}
