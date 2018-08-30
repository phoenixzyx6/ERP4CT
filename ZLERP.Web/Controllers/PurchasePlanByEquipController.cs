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
    public class PurchasePlanByEquipController : BaseController<PurchasePlanByEquip, int>
    {
        //
        // GET: /PurchasePlanByEquip/

        public override ActionResult Index()
        {
            return base.Index();
        }

        /// <summary>
        /// 领导审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Auditing1(PurchasePlanByEquip entity)
        {
            int? id = entity.ID;
            PurchasePlanByEquip obj = this.m_ServiceBase.Get(id);
            if (obj == null)
                return OperateResult(true, Lang.Msg_Operate_Failed, null);
            obj.PurchasePlan_state = entity.PurchasePlan_state;
            obj.PurchasePlan_audit_opinion = entity.PurchasePlan_audit_opinion;
            //更新阶段
            if (obj.PurchasePlan_state == 1)
            {
                //通过
                obj.PurchasePlan_planstate = 1;
                obj.PurchasePlan_audit_date = DateTime.Now;
                obj.PurchasePlan_audit_opinion = entity.PurchasePlan_audit_opinion;
                obj.PurchasePlan_auditor = AuthorizationService.CurrentUserID;

                return base.Update(obj);
            }
            else
            {
                //维修申请改成未提交
                if (obj._type == 0)
                {
                    string EquipMtLyID = obj.EquipMtLyID;
                    EquipMtLy op = this.service.EquipMtLyService.Get(EquipMtLyID);
                    op.mtlystate = 0;
                    this.service.EquipMtLyService.Update(op);
                }
                else
                {
                    string EquipMtLyID = obj.EquipMtLyID;
                    CarRepair op = this.service.CarRepair.Get(EquipMtLyID);
                    op.mtlystate = 0;
                    this.service.CarRepair.Update(op);
                }

                //删除申请记录
                return  this.Delete(new int[] { Convert.ToInt32(obj.ID) });
            }
        }

        /// <summary>
        /// 领导审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Auditing2(PurchasePlanByEquip entity)
        {
            int? id = entity.ID;
            PurchasePlanByEquip obj = this.m_ServiceBase.Get(id);
            if (obj == null)
                return OperateResult(true, Lang.Msg_Operate_Failed, null);
            obj.PurchasePlan_state = entity.PurchasePlan_state;
            obj.PurchasePlan_audit_opinion = entity.PurchasePlan_audit_opinion;
            //更新阶段
            if (obj.PurchasePlan_state == 3)
            {
                //通过
                obj.PurchasePlan_planstate = 2;
                obj.PurchasePlan_audit_date = DateTime.Now;
                obj.PurchasePlan_audit_opinion = entity.PurchasePlan_audit_opinion;
                obj.PurchasePlan_auditor = AuthorizationService.CurrentUserID;

                return base.Update(obj);
            }
            else
            {
                //维修申请改成未提交
                if (obj._type == 0)
                {
                    string EquipMtLyID = obj.EquipMtLyID;
                    EquipMtLy op = this.service.EquipMtLyService.Get(EquipMtLyID);
                    op.mtlystate = 0;
                    this.service.EquipMtLyService.Update(op);
                }
                else
                {
                    string EquipMtLyID = obj.EquipMtLyID;
                    CarRepair op = this.service.CarRepair.Get(EquipMtLyID);
                    op.mtlystate = 0;
                    this.service.CarRepair.Update(op);
                }

                //删除申请记录
                return this.Delete(new int[] { Convert.ToInt32(obj.ID) });
            }
        }
    }
}
