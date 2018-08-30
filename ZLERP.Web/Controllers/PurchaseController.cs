
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Web.Helpers;
using ZLERP.Business;
using ZLERP.Resources;
using Lib.Web.Mvc.JQuery.JqGrid;

namespace ZLERP.Web.Controllers
{
    public class PurchaseController : BaseController<Purchase, int>
    {
        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="ParaArr"></param>
        /// <returns></returns>
        public ActionResult InGoods(string[] ParaArr)
        {
            try
            {
                string str = this.service.Purchase.InGoods(ParaArr[0], Convert.ToDecimal(ParaArr[1]));
                if (string.IsNullOrEmpty(str))
                    return OperateResult(true, Lang.Msg_Operate_Success, str);
                else
                    return OperateResult(false, Lang.Msg_Operate_Failed, str);
            }
            catch
            { return OperateResult(false, Lang.Msg_Operate_Failed, null); }
        }

        public override ActionResult Find(JqGridRequest request, string condition)
        {
            ActionResult r = base.Find(request, condition);
            foreach (Purchase p in ((JqGridData<Purchase>)((JsonResult)r).Data).rows)
            {
                if (p.PurchaseContracts == null || p.PurchaseContracts.Count == 0)
                    continue;
                p.PurchaseContract_Name = p.PurchaseContracts[0].PurchaseContract_Name;
                p.PurchaseContract_SupplyTel = p.PurchaseContracts[0].PurchaseContract_SupplyTel;
                p.PurchaseContract_Supply = p.PurchaseContracts[0].PurchaseContract_Supply;
                p.PurchaseContract_SupplyTel1 = p.PurchaseContracts[0].PurchaseContract_SupplyTel1;
                p.PurchaseContract_Supply1 = p.PurchaseContracts[0].PurchaseContract_Supply1;
                p.PurchaseContract_Date = p.PurchaseContracts[0].PurchaseContract_Date;
                p.PurchaseContract_StartDate = p.PurchaseContracts[0].PurchaseContract_StartDate;
                p.PurchaseContract_EndDate = p.PurchaseContracts[0].PurchaseContract_EndDate;

            }
            return r;
        }
        /// <summary>
        /// 获取最新电话
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetLastTel(string name)
        {
            try
            {
                return OperateResult(true, Lang.Msg_Operate_Success, this.service.Purchase.GetLastTel(name));
            }
            catch
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, null);
            }
        }
        /// <summary>
        /// 获取最新价格
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetLastPrice(string id)
        {
            IList<Purchase> list = this.m_ServiceBase.Find("goodsid='" + id + "'", 0, 0, "BuildTime", "DESC");
            decimal num=0.00m;
            if (list == null || list.Count == 0)
                num = 0.00m;
            else
                num = list[0].Purchase_Price;
            return OperateResult(true, Lang.Msg_Operate_Success, num);
        }

        /// <summary>
        /// 获取用户姓名和电话
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetNameAndTel()
        {
            try
            {
                return OperateResult(true, Lang.Msg_Operate_Success, this.service.Purchase.GetNameAndTel(User.UserID()));
            }
            catch
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, null);
            }
        }
        /// <summary>
        /// 新增子项
        /// </summary>
        /// <param name="ParaArrt"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddPurchase(string[] ParaArrt)
        {
            try
            {
                string msg = this.service.Purchase.AddPurchase(ParaArrt);
                if (string.IsNullOrEmpty(msg))
                    return OperateResult(true, Lang.Msg_Operate_Success, null);
                else
                    return OperateResult(false, Lang.Msg_Operate_Failed + ":" + msg, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ":" + e.Message, null);
            }
        }
        /// <summary>
        /// 提交子项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitPurchase(int id)
        {
            try
            {
                string msg = this.service.Purchase.SubmitPurchase(id);

                if (string.IsNullOrEmpty(msg))
                    return OperateResult(true, Lang.Msg_Operate_Success, null);
                else
                    return OperateResult(false, Lang.Msg_Operate_Failed + ":" + msg, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ":" + e.Message, null);
            }
        }

        [HttpPost]
        public ActionResult GetObj(int id)
        {
            try
            {
                string[] arr = this.service.Purchase.GetObj(id);

                if (arr[0]=="-1")
                    return OperateResult(false, Lang.Msg_Operate_Failed + ":找不到子表信息", null);
                else
                    return OperateResult(true, null, arr);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ":" + e.Message, null);
            }
        }
        /// <summary>
        /// 修改子项
        /// </summary>
        /// <param name="ParaArrt"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdatePurchase(string[] ParaArrt)
        {
            try
            {
                string msg = this.service.Purchase.UpdatePurchase(ParaArrt);
                if (string.IsNullOrEmpty(msg))
                    return OperateResult(true, Lang.Msg_Operate_Success, null);
                else
                    return OperateResult(false, Lang.Msg_Operate_Failed + ":" + msg, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ":" + e.Message, null);
            }
        }
    }
}
