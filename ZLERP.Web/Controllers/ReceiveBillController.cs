using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class ReceiveBillController : BaseController<ReceiveBill, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.Sales = HelperExtensions.SelectListData<User>("TrueName", "ID", string.Format("UserType='{0}'", UserType.Sales), "TrueName", true, "");
            //收款员限定为 销售会计
            ViewBag.ReceiveUsers = HelperExtensions.SelectListData<User>("TrueName", "ID", string.Format("UserType='{0}'", UserType.SaleAccountant), "TrueName", true, "");
            return base.Index();
        }
        /// <summary>
        /// 查询本期供货数量
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public JsonResult GetSendCube(string contractId, string beginDate, string endDate)
        {
            if (string.IsNullOrEmpty(beginDate))
                beginDate = "1753-01-01";
            if (string.IsNullOrEmpty(endDate)) {
                endDate = "9999-12-31";
            }
            var query = this.service.ShippingDocument.Query()
                .Where(p=>p.IsEffective == true)
                .Where(p => p.ContractID == contractId)
                .Where(p => p.ProduceDate >= Convert.ToDateTime(beginDate))
                .Where(p => p.ProduceDate <= Convert.ToDateTime(endDate));
            return Json(query.Count());
        }

    }
}
