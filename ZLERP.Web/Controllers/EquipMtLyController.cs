
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web;
using System.Web.Mvc;
using ZLERP.Web.Helpers;
using ZLERP.Resources;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    public class EquipMtLyController : BaseController<EquipMtLy, string>
    {
        public override ActionResult Index()
        {
            ViewBag.MntZlID = HelperExtensions.SelectListData<MntZl>("ID", "ID", "MntZlID IN (SELECT DISTINCT MntZlID FROM MntZlItem) AND ReAuditStatus = 1","ID",true,"");
            ViewBag.ClassBEquip = HelperExtensions.SelectListData<ClassB>("ClassBName", "ID", "ClassID ='" + Request.QueryString["p1"] + "'", "ID", true, "");
            ViewBag.Department = HelperExtensions.SelectListData<Department>("DepartmentName", "ID", "", "ID", true, "");
            ViewBag.User = HelperExtensions.SelectListData<User>("TrueName", "ID", "", "ID", true, "");
            ViewBag.PartInfo = HelperExtensions.SelectListData<PartInfo>("PartName", "ID", "", "ID", true, "");
            return base.Index();
        }

        public ActionResult Apply()
        {
            ViewBag.ClassBEquip = HelperExtensions.SelectListData<ClassB>("ClassBName", "ID", "ClassID ='EquipType'", "ID", true, "");
            ViewBag.Department = HelperExtensions.SelectListData<Department>("DepartmentName", "ID", "", "ID", true, "");
            ViewBag.User = HelperExtensions.SelectListData<User>("TrueName", "ID", "", "ID", true, "");
            InitCommonViewBag();
            return View();
        }
        public override ActionResult Add(EquipMtLy EquipMtLy)
        {
            EquipMtLy.mtlystate = 0;
            return base.Add(EquipMtLy);
        }

        public override ActionResult Update(EquipMtLy EquipMtLy)
        {
            EquipMtLy.mtlystate = 0;
            return base.Update(EquipMtLy);
        }

        public ActionResult ImportEntitys(string equipmtlyid, string[] ids) {
            try
            {
                this.service.EquipMtLyService.ImportMntZlItems(equipmtlyid, ids);
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    e = e.InnerException;
                log.Error(e.Message, e);
                return OperateResult(false, Lang.Msg_Operate_Failed + ":" + (e.Message.Contains("UNIQUE KEY") == true ? Lang.IsExistRecord : e.Message), null);
            }
        }

        public ActionResult submit(string id)
        {
            EquipMtLy e = this.m_ServiceBase.Get(id);
            e.mtlystate = 1;
            base.Update(e);

            PurchasePlanByEquip obj = new PurchasePlanByEquip();
            obj.PurchasePlan_NeedDate = e.ApplyTime;
            obj.GoodsID = e.EquipmentName;
            //obj.BID = e.ClassBID;
            //obj.MID = e.ClassMID;
            //obj.SID = e.ClassSID;
            obj.PurchasePlan_reason = e.TroubleDes;
            obj.PurchasePlan_planstate = 0;
            obj.PurchasePlan_state = 0;
            obj.PurchasePlan_claimer = AuthorizationService.CurrentUserID;
            obj.planmoney = 0.00m;
            obj._type = 0;
            obj.planmoney = e.summoney;
            obj.EquipMtLyID = id;

            this.service.PurchasePlanByEquip.Add(obj);

            return OperateResult(true, Lang.Msg_Operate_Success, null); 
        }
    }    
}
