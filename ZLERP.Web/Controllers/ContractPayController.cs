using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    /// <summary>
    /// 合同付款
    /// </summary>
    public class ContractPayController : BaseController<ContractPay, int>
    {
        public override ActionResult Add(ContractPay ContractPay)
        {
            return base.Add(ContractPay);
        }
    }
}
