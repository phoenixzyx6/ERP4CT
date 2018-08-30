using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class CustMixpropItemController : BaseController<CustMixpropItem,int>
    {
        public override ActionResult Add(CustMixpropItem cmpitems)
        {
            return base.Add(cmpitems);
        }

        public override ActionResult Update(CustMixpropItem cmpitems)
        {
            return base.Update(cmpitems);
        }

    }
}
