using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class Lab_FA_ItemsController : BaseController<Lab_FA_Items, int?>
    {
        public override ActionResult Add(Lab_FA_Items Lab_FA_Items)
        {
            return base.Add(Lab_FA_Items);
        }

        public override ActionResult Update(Lab_FA_Items Lab_FA_Items)
        {
            return base.Update(Lab_FA_Items);
        }
    }   

}
