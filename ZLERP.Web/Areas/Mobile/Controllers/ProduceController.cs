using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Web.Controllers;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using ZLERP.Web.Controllers.Attributes;
using Lib.Web.Mvc.JQuery.JqGrid;
namespace ZLERP.Web.Areas.Mobile.Controllers
{
    public class ProduceController : BaseController<CustomerPlan, string>
    {
         [MobileAuthorizeAttribute]
        public override ActionResult Index()
        {
            var plans =  this.service.CustomerPlan.Query()
                .Where(p => p.PlanDate ==DateTime.Today)
                 .OrderBy(p=>p.NeedDate)
                .ToList();
            return View(plans);
        }

        [MobileAuthorizeAttribute]
        public ActionResult ProduceStats()
        {
            return View();
        }
        [HttpPost,MobileAuthorizeAttribute]
        public ActionResult ProduceStats(string startDate, string endDate) {
           
            if (string.IsNullOrEmpty(startDate)) {
                ModelState.AddModelError("StartDate", "*");
                return View();
            }
            if (string.IsNullOrEmpty(endDate))
            {
                ModelState.AddModelError("EndDate", "*");
                return View();
            }
               var result =  this.service.ShippingDocument.Query()
                    .Where(p => p.ProduceDate >= Convert.ToDateTime(startDate)
                        && p.ProduceDate <= Convert.ToDateTime(endDate)
                        && p.IsEffective == true && p.ShipDocType == "0")
                        .GroupBy(x => new { x.ProjectName, x.ConStrength })
                        .Select(g => new ShippingDocument { ProjectName = g.Key.ProjectName, ConStrength=g.Key.ConStrength, ParCube = g.Sum(p => p.ParCube), SendCube = g.Sum(p => p.SendCube) })
                       
                        .ToList();
               return View(result);
            
           
            //SqlServerHelper helper = new SqlServerHelper();
            //helper.ex
        
        }
        [MobileAuthorizeAttribute]
        public ActionResult StuffInStats() {
            return View();
        }
        [HttpPost, MobileAuthorizeAttribute]
        public ActionResult StuffInStats(string startDate, string endDate)
        {
            if (string.IsNullOrEmpty(startDate))
            {
                ModelState.AddModelError("StartDate", "*");
                return View();
            }
            if (string.IsNullOrEmpty(endDate))
            {
                ModelState.AddModelError("EndDate", "*");
                return View();
            }
            var stuffIn = this.service.StuffIn.Query().Where(p => p.OutDate >= Convert.ToDateTime(startDate)
                        && p.OutDate <= Convert.ToDateTime(endDate))
                        .GroupBy(x=> new{x.SupplyInfo.SupplyName, x.StuffInfo.StuffName, x.Spec})
                        .Select(g=> new StuffIn{  SupplyID = g.Key.SupplyName, StuffID = g.Key.StuffName, Spec= g.Key.Spec, InNum=g.Sum(p=>p.InNum), Version = g.Count()})
                        .ToList();
            //var stuffInfo = this.service.StuffInfo.Query();
            //var supplyInfo = this.service.GetGenericService<SupplyInfo>().Query();

            //var result = from s in stuffIn
            //             from si in stuffInfo
            //             from su in supplyInfo
            //             where s.StuffID == si.ID && s.SupplyID == su.ID
            //             && s.OutDate >= Convert.ToDateTime(startDate)
            //              && s.OutDate <= Convert.ToDateTime(endDate)
            //              select s.supp
             
            return View(stuffIn);
        }

        /// <summary>
        /// 今日任务
        /// </summary>
        /// <returns></returns>
        [MobileAuthorizeAttribute]
        public ActionResult Tasks()
        {
            var req = new JqGridRequest();
            req.PageIndex = 0;
            req.RecordsCount = 20;
            req.SortingName = "ID";
            req.SortingOrder = JqGridSortingOrders.Desc;
            var tasks = this.service.ProduceTask.GetRangeTimeTasks(req);
            return View(tasks);
        }
    }
}
