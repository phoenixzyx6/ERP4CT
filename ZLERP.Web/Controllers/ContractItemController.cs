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
    public class ContractItemController : BaseController<ContractItem, int>
    {

        public override ActionResult Add(ContractItem contractItem)
        {
            return base.Add(contractItem);
        }
        /// <summary>
        /// 判断砼强度是否属于合同里面
        /// </summary>
        /// <param name="contractid"></param>
        /// <param name="constrength"></param>
        /// <returns></returns>
        public ActionResult getContractItem(string contractid, string constrength)
        {
            var cons = this.service.GetGenericService<ContractItem>().Find("contractid='" + contractid + "' and constrength='"+constrength+"'", 1, 100, "", "");
            if (cons.ToList().Count > 0)
            {
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            else
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, null);
            }
        }
    }
}
