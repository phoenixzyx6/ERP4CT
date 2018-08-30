using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class SiloController : BaseController<Silo,string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            //原材料列表
            var parentStuffInfo = this.service.GetGenericService<StuffInfo>().All("", "OrderNum", true).Where(s => s.IsUsed).ToList();
            parentStuffInfo.Insert(0, new StuffInfo());
            ViewBag.StuffInfoDics = new SelectList(parentStuffInfo, "ID", "StuffName");
            
            //机组信息列表
            var pllist=HelperExtensions.SelectListData<ProductLine>("ProductLineName", "ID", "IsUsed=1", "ID", true, "");
            ViewBag.ProductLineList = pllist;

            return base.Index();
        }
        /// <summary>
        /// 筒仓列表数据【格式：筒仓名称（筒仓简称）】
        /// </summary>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public override JsonResult ListData(string textField, string valueField, string orderBy = "ID",
            bool ascending = false,
            string condition = "")
        {
            var data = this.m_ServiceBase.All(new List<string> { valueField, textField, "SiloShortName" }, condition, orderBy, ascending);
            foreach (var d in data) {
                d.SiloName = string.Format("{0}({1})", d.SiloName, d.SiloShortName);
            }
            return Json(new SelectList(data, valueField, textField));
        }
        /// <summary>
        /// 桶仓库存状态
        /// </summary>
        /// <returns></returns>
        public ActionResult SiloStatus()
        {
            base.InitCommonViewBag();
            var siloList = this.service.Silo.All("OrderNum", true);
            ViewBag.ProductLine = this.service.ProductLine.Query().Where(p=>p.IsUsed).ToList();
            ViewBag.SiloProductLine = this.service.GetGenericService<SiloProductLine>().All().OrderBy(m=>m.Silo.OrderNum);
            return View(siloList);
        }

    }
}
