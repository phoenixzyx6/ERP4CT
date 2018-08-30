
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Resources;
using System.Web.Mvc;
using System.Linq;
namespace ZLERP.Web.Controllers
{
    public class PartStockContractItemController : BaseController<PartStockContractItem, int>
    {
        public override ActionResult Add(PartStockContractItem PartStockContractItem)
        {
            string stockPlanId = PartStockContractItem.StockPlanID;
            string contractId = PartStockContractItem.ContractID;
            string condition = string.Format("StockPlanID='{0}' AND ContractID='{1}'", stockPlanId, contractId);
            IList<PartStockContractItem> list = this.service.GetGenericService<PartStockContractItem>().All(condition, "ID", true);
            if (list.Count > 0)
            {
                return OperateResult(false, string.Format("计划{0}在合同{1}中已经存在!",stockPlanId,contractId), null);
            }
            //合同中不允许添加多个同样的采购计划
            return base.Add(PartStockContractItem);
        }

    }    
}
