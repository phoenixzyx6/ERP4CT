using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Linq;
using ZLERP.Model;
using ZLERP.Business;
using System.Web.Mvc;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;
using ZLERP.Resources;
using ZLERP.Web.Controllers.Attributes;

namespace ZLERP.Web.Controllers
{
    public class CustomerPlanController : BaseController<CustomerPlan, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
                //string userId = AuthorizationService.CurrentUserID;
                //if(!string.IsNullOrEmpty(userId)){
                //var customers = this.service.Customer.Query()
                //    .Where(p => p.Buyer == userId)
                //    .Select(p=>p.ID)
                //    .ToList();
                ////用户合同列表
                //var contracts = this.service.Contract.Query()
                //    .Where(p => customers.Contains(p.CustomerID))
                //    .ToList();
                //var contractIds = contracts.Select(c => c.ID).ToList();

                //ViewBag.Contracts = new SelectList(contracts, "ID", "ContractName");

                //var tasks = this.service.ProduceTask.Query()
                //    .Where(p => contractIds.Contains(p.ContractID))
                //    .ToList();

                //ViewBag.Tasks = tasks.ToJson();
                ViewBag.Regions = HelperExtensions.SelectListData<Region>("RegionName", "ID", "ID", true);
                //ViewBag.ConStrength = HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "ConStrengthCode", true);
                
                var pumpList = this.service.Car.GetPumpList()
                .OrderBy(p => p.ID);
                ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");

                ViewBag.LinkManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '51' AND IsUsed=1 ", "ID", true, "");
                ViewBag.PumpManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '52' AND IsUsed=1 ", "ID", true, "");

                //ViewBag.SupplyUnit = HelperExtensions.SelectListData<SupplyInfo>("SupplyName", "SupplyName", "supplyID in('1010401','1010405')", "SupplyName", true, "");
                //ViewBag.SupplyUnit = HelperExtensions.SelectListData<SupplyInfo>("SupplyName", "ID", "supplyID in('1010401','1010405')", "ID", true, null);
                //ViewBag.SupplyUnit = new List<SelectListItem>(){
                //    new SelectListItem(){ Value = "重庆天助商品混凝土搅拌有限公司", Text = "重庆天助商品混凝土搅拌有限公司" },
                //    new SelectListItem(){ Value = "重庆市城投混凝土有限公司", Text = "重庆市城投混凝土有限公司" }};
                ViewBag.NeedDateList = new List<SelectListItem>(){
                    new SelectListItem(){ Value = "", Text = "" },
                    new SelectListItem(){ Value = "待定", Text = "待定" },
                    new SelectListItem(){ Value = "00:00", Text = "00:00" },
                    new SelectListItem(){ Value = "00:30", Text = "00:30" },
                    new SelectListItem(){ Value = "01:00", Text = "01:00" },
                    new SelectListItem(){ Value = "01:30", Text = "01:30" },
                    new SelectListItem(){ Value = "02:00", Text = "02:00" },
                    new SelectListItem(){ Value = "02:30", Text = "02:30" },
                    new SelectListItem(){ Value = "03:00", Text = "03:00" },
                    new SelectListItem(){ Value = "03:30", Text = "03:30" },
                    new SelectListItem(){ Value = "04:00", Text = "04:00" },
                    new SelectListItem(){ Value = "04:30", Text = "04:30" },
                    new SelectListItem(){ Value = "05:00", Text = "05:00" },
                    new SelectListItem(){ Value = "05:30", Text = "05:30" },
                    new SelectListItem(){ Value = "06:00", Text = "06:00" },
                    new SelectListItem(){ Value = "06:30", Text = "06:30" },
                    new SelectListItem(){ Value = "07:00", Text = "07:00" },
                    new SelectListItem(){ Value = "07:30", Text = "07:30" },
                    new SelectListItem(){ Value = "08:00", Text = "08:00" },
                    new SelectListItem(){ Value = "08:30", Text = "08:30" },
                    new SelectListItem(){ Value = "09:00", Text = "09:00" },
                    new SelectListItem(){ Value = "09:30", Text = "09:30" },
                    new SelectListItem(){ Value = "10:00", Text = "10:00" },
                    new SelectListItem(){ Value = "10:30", Text = "10:30" },
                    new SelectListItem(){ Value = "11:00", Text = "11:00" },
                    new SelectListItem(){ Value = "11:30", Text = "11:30" },
                    new SelectListItem(){ Value = "12:00", Text = "12:00" },
                    new SelectListItem(){ Value = "12:30", Text = "12:30" },
                    new SelectListItem(){ Value = "13:00", Text = "13:00" },
                    new SelectListItem(){ Value = "13:30", Text = "13:30" },
                    new SelectListItem(){ Value = "14:00", Text = "14:00" },
                    new SelectListItem(){ Value = "14:30", Text = "14:30" },
                    new SelectListItem(){ Value = "15:00", Text = "15:00" },
                    new SelectListItem(){ Value = "15:30", Text = "15:30" },
                    new SelectListItem(){ Value = "16:00", Text = "16:00" },
                    new SelectListItem(){ Value = "16:30", Text = "16:30" },
                    new SelectListItem(){ Value = "17:00", Text = "17:00" },
                    new SelectListItem(){ Value = "17:30", Text = "17:30" },
                    new SelectListItem(){ Value = "18:00", Text = "18:00" },
                    new SelectListItem(){ Value = "18:30", Text = "18:30" },
                    new SelectListItem(){ Value = "19:00", Text = "19:00" },
                    new SelectListItem(){ Value = "19:30", Text = "19:30" },
                    new SelectListItem(){ Value = "20:00", Text = "20:00" },
                    new SelectListItem(){ Value = "20:30", Text = "20:30" },
                    new SelectListItem(){ Value = "21:00", Text = "21:00" },
                    new SelectListItem(){ Value = "21:30", Text = "21:30" },
                    new SelectListItem(){ Value = "22:00", Text = "22:00" },
                    new SelectListItem(){ Value = "22:30", Text = "22:30" },
                    new SelectListItem(){ Value = "23:00", Text = "23:00" },
                    new SelectListItem(){ Value = "23:30", Text = "23:30" }
                };
           // }
            return base.Index();
        }
        
        public  override ActionResult Find(Lib.Web.Mvc.JQuery.JqGrid.JqGridRequest request, string condition)
        {
            //if (string.IsNullOrEmpty(condition))
            //{
            //    condition = string.Format("Builder='{0}'", AuthorizationService.CurrentUserID);
            //}
            //else
            //    condition += string.Format(" AND Builder='{0}'", AuthorizationService.CurrentUserID);

            return base.Find(request, condition);
        }
        [HttpPost]
        public ActionResult ManageFind(Lib.Web.Mvc.JQuery.JqGrid.JqGridRequest request, string condition)
        {
            return base.Find(request, condition);
        }

        public ActionResult ManageIndex()
        {
            base.InitCommonViewBag();
            ViewBag.ConStrength = HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "ConStrengthCode", true);
            ViewBag.Regions = HelperExtensions.SelectListData<Region>("RegionName", "ID", "ID", true);
            var pumpList = this.service.Car.GetPumpList()
                .OrderBy(p => p.ID);
            ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");
            ViewBag.SupplyUnit1 = HelperExtensions.SelectListData<SupplyInfo>("SupplyName", "SupplyName", "supplyID in('1010401','1010405')", "SupplyName", true, null);
            return View();
        }
        [HttpPost, HandleAjaxError]
        public JsonResult Auditing(CustomerPlan plan)
        { 
            bool ret = this.service.CustomerPlan.Auditing(plan);
            return OperateResult(ret, ret ? Lang.Msg_Operate_Success : Lang.Msg_Operate_Failed, false);
        }

        public ActionResult MultAudit(string[] ids) {
            bool ret = this.service.CustomerPlan.MultAudit(ids);
            return OperateResult(ret, ret ? Lang.Msg_Operate_Success : Lang.Msg_Operate_Failed, false);
        }
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult CancelAudit(string[] ids)
        {
            string errmsg="";
            bool ret = this.service.CustomerPlan.CancelAudit(ids,ref errmsg);
            return OperateResult(ret, ret ? Lang.Msg_Operate_Success : Lang.Msg_Operate_Failed +":"+ errmsg, false);
        }
        /// <summary>
        /// 组合任务单数据显示在autocomplete
        /// </summary>
        /// <param name="q"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {

            IList<Contract> data;
            q = FilterSpecial(q);
            if (string.IsNullOrEmpty(q))
            {
                data = this.service.Contract.Find(condition, 1, 30, orderBy, ascending ? "ASC" : "DESC");
            }
            else
            {
                string where = string.Format("({0} like '%{1}%' or ID like '%{1}%') ", textField, q);
                if (!string.IsNullOrEmpty(condition))
                    where += " AND " + condition;
                data = this.service.Contract.Find(
                    where,
                    1,
                    30,
                    orderBy,
                    ascending ? "ASC" : "DESC");
            }

            var dataList = data.Select(p => new
            {
                Text = string.Format("<strong>{0}</strong>[{5}]<br/>{3}：{1}<br/>{4}：{2}",
                        HelperExtensions.Eval(p, textField),
                        p.CustName,
                        p.ContractName,
                        Lang.Entity_Property_CustName,
                        Lang.Entity_Property_ContractName,
                        p.ID),

                Value = HelperExtensions.Eval(p, valueField)
            });
            return Json(new SelectList(dataList, "Value", "Text"));

        }

        public override ActionResult Update(CustomerPlan entity)
        {
            var planInDB = this.service.CustomerPlan.Get(entity.ID);

            if (planInDB != null && planInDB.AuditStatus == (int)AuditStatus.Pass)
            { 
                return OperateResult(false, "已审核的计划不能修改", null);
            }
            return base.Update(entity);
        }

        /// <summary>
        /// 取得砼强度下拉列表数据
        /// </summary>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public virtual JsonResult ListDataStrengthInfo(string id, string textField, string valueField, string orderBy = "ID",bool ascending = false,string condition = "")
        {
            IEnumerable<SelectListItem> list = HelperExtensions.SelectListData<ContractItem>("ConStrength", "ID", condition, "ConStrength", true, null);
            return Json(list);
        }
    }    
}
