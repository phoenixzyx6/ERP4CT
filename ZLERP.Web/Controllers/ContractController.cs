using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    /// <summary>
    /// 合同管理
    /// </summary>
    public class ContractController : BaseController<Contract,string>
    {

        [ValidateInput(false)]
        public override ActionResult Add(Contract Contract)
        {
            //Contract.DianziString = "";
            //if (Contract.Dianzi1 != null)
            //{
            //    Contract.DianziString += "垫资" + Contract.Dianzi1 + "万元。";
            //}
            //if (Contract.Dianzi2 != null)
            //{
            //    Contract.DianziString += "垫满一次付" + Contract.Dianzi2 + "%。";
            //}
            //if (Contract.Dianzi3 != null)
            //{
            //    Contract.DianziString += "供应量月结" + Contract.Dianzi3 + "%。";
            //}
            //if (Contract.Dianzi4 != null)
            //{
            //    Contract.DianziString += "推迟" + Contract.Dianzi4 + "月付。";
            //}
            //if (Contract.Dianzi6 != null)
            //{
            //    Contract.DianziString += "供应量垫资月结" + Contract.Dianzi6 + "%。";
            //}
            //if (Contract.Dianzi7 != null)
            //{
            //    Contract.DianziString += "推迟" + Contract.Dianzi7 + "月付。";
            //}
            //if (Contract.Dianzi5 != null)
            //{
            //    Contract.DianziString += "尾款在工程完工后" + Contract.Dianzi5 + "个月内按月等额支付完毕。";
            //}
            return base.Add(Contract);
        }

        public ActionResult ControlIndex()
        {
            ViewBag.ConStrength = HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "ConStrengthCode", true);
            ViewBag.Regions = HelperExtensions.SelectListData<Region>("RegionName", "ID", "ID", true);
            ViewBag.Sales = HelperExtensions.SelectListData<User>("TrueName",
                 "ID",
                 string.Format("UserType='{0}'", Params["salestype"]),
                 "TrueName",
                 true,
                 "");
            ViewBag.LinkManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '51' AND IsUsed=1 ", "ID", true, "");
            base.InitCommonViewBag();
            string funcId = Request.QueryString["f"];
            ViewBag.Buttons3 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 3)));
            ViewBag.Buttons4 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 4)));
            ViewBag.ExtraPumpList = new List<SelectListItem>(){
                    new SelectListItem(){ Value = "设置润管砂浆", Text = "设置润管砂浆" },
                    new SelectListItem(){ Value = "设置臂架泵", Text = "设置臂架泵" },
            };
            return View();
        }
        /// <summary>
        /// 付款
        /// </summary>
        /// <returns></returns>
        public ActionResult PayIndex()
        {
            base.InitCommonViewBag();
            ViewBag.DZType = new List<SelectListItem>(){
                    new SelectListItem(){ Value = "累计垫资", Text = "累计垫资" },
                    new SelectListItem(){ Value = "当月应收垫资", Text = "当月应收垫资" },
            };
            return View();
        }

        public override ActionResult Update(Contract Contract)
        {
            //Contract.DianziString = "";
            //if (Contract.Dianzi1 != null)
            //{
            //    Contract.DianziString += "垫资" + Contract.Dianzi1 + "万元。";
            //}
            //if (Contract.Dianzi2 != null)
            //{
            //    Contract.DianziString += "垫满一次付" + Contract.Dianzi2 + "%。";
            //}
            //if (Contract.Dianzi3 != null)
            //{
            //    Contract.DianziString += "供应量月结" + Contract.Dianzi3 + "%。";
            //}
            //if (Contract.Dianzi4 != null)
            //{
            //    Contract.DianziString += "推迟" + Contract.Dianzi4 + "月付。";
            //}
            //if (Contract.Dianzi6 != null)
            //{
            //    Contract.DianziString += "供应量垫资月结" + Contract.Dianzi6 + "%。";
            //}
            //if (Contract.Dianzi7 != null)
            //{
            //    Contract.DianziString += "推迟" + Contract.Dianzi7 + "月付。";
            //}
            //if (Contract.Dianzi5 != null)
            //{
            //    Contract.DianziString += "尾款在工程完工后" + Contract.Dianzi5 + "个月内按月等额支付完毕。";
            //}
            return base.Update(Contract);
        }

        /// <summary>
        /// 结算确认
        /// </summary>
        /// <param name="Contract"></param>
        /// <returns></returns>
        public ActionResult Confirm(Contract Contract)
        {
            Contract entity = this.service.Contract.Get(Contract.ID);
            entity.FootDate = Contract.FootDate;
            List<ShippingDocument> sdlists = this.service.ShippingDocument.All().Where(p=>p.ContractID==Contract.ID && p.BuildTime<Contract.FootDate && p.IsJS==false).ToList();
            foreach(var a in sdlists)
            {
                this.service.Contract.JS(a);
            }
            return base.Update(entity);
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="ConsMixprop"></param>
        /// <returns></returns>
        public ActionResult Audit(Contract Contract)
        {
            try
            {
                this.service.Contract.Audit(Contract);
                this.service.SysLog.Log(Model.Enums.SysLogType.Audit, Contract.ID, null, "合同审核");
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="auditStatus"></param>
        /// <returns></returns>
        public ActionResult UnAudit(string contractID, int auditStatus) 
        {
            try
            {
                this.service.Contract.UnAudit(contractID, auditStatus);
                this.service.SysLog.Log(Model.Enums.SysLogType.UnAudit, contractID, null, "合同取消审核");
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, "");
            }
            
        }
        /// <summary>
        /// 快速解除限制
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="contractStatus"></param>
        /// <param name="contractLimitType"></param>
        /// <returns></returns>
        public ActionResult QuickUnfreeze(string contractID, string contractStatus, string contractLimitType)
        {
            try
            {
                this.service.Contract.QuickUnfreeze(contractID, contractStatus, contractLimitType);
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, "");
            }
        }
        
        /// <summary>
        /// 快速锁定合同
        /// </summary>
        /// <param name="contractID"></param>
        /// <returns></returns>
        public ActionResult QuickLock(string contractID,string remark)
        {
            try
            {
                this.service.Contract.QuickLock(contractID,remark);
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, "");
            }
        }

        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.ConStrength = HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "ConStrengthCode", true);
            ViewBag.Regions = HelperExtensions.SelectListData<Region>("RegionName", "ID", "ID", true);
            ViewBag.Sales = HelperExtensions.SelectListData<User>("TrueName",
                 "ID",
                 string.Format("UserType='{0}'", Params["salestype"]),
                 "TrueName",
                 true,
                 "");
            ViewBag.LinkManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '51' AND IsUsed=1 ", "ID", true, "");
            return base.Index();
        }

        /// <summary>
        /// 将合同置为完工
        /// </summary>
        /// <param name="contractID"></param>
        /// <returns></returns>
        public ActionResult SetComplete(string contractID)
        {
            bool result = this.service.Contract.SetComplete(contractID);
            return OperateResult(result, Lang.Msg_Operate_Success, result);
        }

        /// <summary>
        /// 将合同置为未完工
        /// </summary>
        /// <param name="contractID"></param>
        /// <returns></returns>
        public ActionResult SetUnComplete(string contractID)
        {
            bool result = this.service.Contract.SetUnComplete(contractID);
            return OperateResult(result, Lang.Msg_Operate_Success, result);
        }

        /// <summary>
        /// 获取特性价格
        /// </summary>
        /// <param name="identityName"></param>
        /// <param name="identityType"></param>
        /// <returns></returns>
        public ActionResult getIdentityPrice(string identityName, string identityType)
        {
            dynamic identity = this.service.Contract.getIdentityPrice(identityName, identityType);

            return this.Json(identity);
        }
        /// <summary>
        /// 导入砼强度
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="conStrength"></param>
        /// <returns></returns>
        public ActionResult Import(string contractID, string[] conStrength)
        {
            try
            {
                bool result = this.service.Contract.Import(contractID, conStrength);
                return OperateResult(true, Lang.Msg_Operate_Success, result);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                log.Error(ex.Message, ex);
                return HandleExceptionResult(ex);
            }
        }
    }
}
