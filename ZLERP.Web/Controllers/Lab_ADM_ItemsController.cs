using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class Lab_ADM_ItemsController : BaseController<Lab_ADM_Items, int?>
    {
        public override ActionResult Add(Lab_ADM_Items Lab_ADM_Items)
        {
            return base.Add(Lab_ADM_Items);
        }

        public override ActionResult Update(Lab_ADM_Items Lab_ADM_Items)
        {
            return base.Update(Lab_ADM_Items);
        }
    }   

}
