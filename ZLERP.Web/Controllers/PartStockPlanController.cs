
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Model.Enums;
using System.Linq;

namespace ZLERP.Web.Controllers
{
    public class PartStockPlanController : BaseController<PartStockPlan, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            var partInfo = this.service.GetGenericService<PartInfo>().All("", "ID", true);
            partInfo.Insert(0, new PartInfo());
            ViewBag.PartInfoDics = new SelectList(partInfo, "ID", "PartName");

            var parentDepart = this.service.GetGenericService<Department>().All("", "ID", true);
            parentDepart.Insert(0, new Department());
            ViewBag.DepartDics = new SelectList(parentDepart, "ID", "DepartmentName");

            return base.Index();
        }

        public override ActionResult Add(PartStockPlan PartStockPlan)
        {
            return base.Add(PartStockPlan);
        }

        /// <summary>
        /// 查询审核通过且不在该合同中的数据
        /// </summary>
        /// <param name="ContractID"></param>
        /// <returns></returns>
        public ActionResult FindPlan(Lib.Web.Mvc.JQuery.JqGrid.JqGridRequest request, string ContractID, string PartID)
        {
            string[] array = this.service.GetGenericService<PartStockContractItem>().Query().Where(c => c.ContractID == ContractID).Select(c => c.StockPlanID).ToArray();
            string existId = string.Format("'{0}'", string.Join("','", array));
            string condition = string.Format("ID not in({0}) AND AuditStatus = 1", existId);
            return base.Find(request, condition);
        }

    }    
}
