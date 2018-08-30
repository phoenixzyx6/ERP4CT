using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class ContractProductController : BaseController<ContractProduct, int>
    {
        public override ActionResult Add(ContractProduct ContractProduct)
        {
            return base.Add(ContractProduct);
        }

    }
}
