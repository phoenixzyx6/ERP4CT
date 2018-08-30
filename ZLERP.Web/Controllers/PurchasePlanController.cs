using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Resources;
using ZLERP.Model.Enums;
using ZLERP.Web.Controllers.Attributes;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    public class PurchasePlanController : BaseController<PurchasePlan, int>
    {
        //
        // GET: /PurchasePlan/

        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Add(PurchasePlan entity)
        {
            entity.PurchasePlan_claimer = AuthorizationService.CurrentUserID;

            //单价
            //Purchase obj = this.service.Purchase.Query().Where(m => m.GoodsID == entity.GoodsID).OrderByDescending(m => m.BuildTime).FirstOrDefault();
            //decimal num = 0.00m;
            //if (obj == null)
            //    num = 0.00m;
            //else
            //    num = obj.Purchase_Price;

            //entity.planprice = num;
            //entity.planmoney = num * entity.PurchasePlan_num;

            return base.Add(entity);
        }

        public override ActionResult Update(PurchasePlan entity)
        {
            //单价
            //Purchase obj = this.service.Purchase.Query().Where(m => m.GoodsID == entity.GoodsID).OrderByDescending(m => m.BuildTime).FirstOrDefault();
            //decimal num = 0.00m;
            //if (obj == null)
            //    num = 0.00m;
            //else
            //    num = obj.Purchase_Price;

            //entity.planprice = num;
            //entity.planmoney = num * entity.PurchasePlan_num;

            return base.Update(entity);
        }

        public override ActionResult Find(JqGridRequest request, string condition)
        {
            //有审核按钮的是领导都可以看见
            //没有审核按钮权限的只能看到自己的
            IList<SysFunc> FuncList = this.service.User.GetUserFuncs(AuthorizationService.CurrentUserID);
            SysFunc sf = FuncList.Where(p => p.ID == "232005" || p.ID == "232006").FirstOrDefault();
            if (sf != null)              
            {
                //有权限
            }
            else
            {
                if (!string.IsNullOrEmpty(condition))
                {
                    condition = " AND ";
                }
                condition += " PurchasePlan_claimer='" + AuthorizationService.CurrentUserID + "'";
            }

            //return base.Find(request, condition);
            ActionResult r = base.Find(request, condition);
            GoodsInfo g = null;
            foreach (PurchasePlan p in ((ZLERP.Model.JqGridData<PurchasePlan>)((JsonResult)r).Data).rows)
            {
                if (string.IsNullOrEmpty(p.GoodsID))
                    continue;
                g = this.service.GoodsInfo.Get(p.GoodsID);
                if (g != null)
                {
                    p.GoodsNum = g.ContentNum;
                }
            }
            return r;
        }

        /// <summary>
        /// 提交采购计划
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SubmitInfo(int id)
        {
            PurchasePlan obj = this.service.PurchasePlan.Get(id);
            if(obj==null)
                return OperateResult(true, Lang.Msg_Operate_Failed, null);
            obj.PurchasePlan_state = 1;//已提交
            obj.PurchasePlan_planstate = 1;//进入计划阶段
            return base.Update(obj);
        }


        /// <summary>
        /// 物资员审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AuditingByGoods(PurchasePlan entity)
        {
            int? id = entity.ID;
            PurchasePlan obj = this.service.PurchasePlan.Get(id);
            if(obj==null)
                return OperateResult(true, Lang.Msg_Operate_Failed, null);
            obj.Remark = entity.Remark;
            obj.PurchasePlan_state = entity.PurchasePlan_state;
            //更新阶段
            if(string.IsNullOrEmpty(obj.PurchasePlan_state.ToString())||(obj.PurchasePlan_state!=4&&obj.PurchasePlan_state!=5))
                return OperateResult(true, Lang.Msg_Operate_Failed, null);
            if (obj.PurchasePlan_state == 4)
            {
                //通过
            }
            else
            { 
                //不通过
                obj.PurchasePlan_planstate = 0;
            }
            obj.goodsername = AuthorizationService.CurrentUserID;
            obj.goodsertime = DateTime.Now;
            obj.planmoney = entity.planmoney;
            obj.planprice = entity.planprice;
            return base.Update(obj);
        }


        /// <summary>
        /// 领导审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Auditing1(PurchasePlan entity)
        {
            int? id = entity.ID;
            PurchasePlan obj = this.service.PurchasePlan.Get(id);
            if (obj == null)
                return OperateResult(true, Lang.Msg_Operate_Failed, null);
            obj.Remark = entity.Remark;
            obj.PurchasePlan_state = entity.PurchasePlan_state;
            //更新阶段
            if (string.IsNullOrEmpty(obj.PurchasePlan_state.ToString()) || (obj.PurchasePlan_state != 2 && obj.PurchasePlan_state != 3))
                return OperateResult(true, Lang.Msg_Operate_Failed, null);
            if (obj.PurchasePlan_state == 2)
            {
                //通过
                obj.PurchasePlan_planstate = 2;                
            }
            else
            {
                //不通过
                obj.PurchasePlan_planstate = 0;
            }
            obj.PurchasePlan_audit_date = DateTime.Now;
            obj.PurchasePlan_audit_opinion = entity.PurchasePlan_audit_opinion;
            obj.PurchasePlan_auditor = AuthorizationService.CurrentUserID;

            return base.Update(obj);
        }

        public ActionResult AuditingALL(string[] ids)
        {
            if(ids==null||ids.Length==0)
                return OperateResult(true, Lang.Msg_Operate_Failed, null);
            for (int i = 0; i < ids.Length; i++)
            {
                int? id = Convert.ToInt32(ids[i]);
                PurchasePlan obj = this.service.PurchasePlan.Get(id);
                if (obj == null)
                    return OperateResult(true, Lang.Msg_Operate_Failed, null);
                obj.Remark = "批量审核";
                obj.PurchasePlan_state = 2;
                obj.PurchasePlan_planstate = 2;
                obj.PurchasePlan_audit_date = DateTime.Now;
                obj.PurchasePlan_audit_opinion = "批量审核";
                obj.PurchasePlan_auditor = AuthorizationService.CurrentUserID;

                base.Update(obj);
            }
            return OperateResult(true, Lang.Msg_Operate_Success, null);
        }
    }
}
