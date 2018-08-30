
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Model.ViewModels;
using ZLERP.Business;
using ZLERP.Resources;
using ZLERP.NHibernateRepository;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class CarRepairController : BaseController<CarRepair, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.UserList = HelperExtensions.SelectListData<User>("TrueName", "ID", "IsUsed=1", "TrueName", true, "");
            var carList = this.service.Car.GetCarSelectList(null).OrderBy(c => c.CarTypeID + c.ID);
            ViewBag.CarList = new SelectList(carList, "ID", "CarNo");
            return base.Index();
        }

        public override ActionResult Add(CarRepair entity)
        {
            //entity.CarMan = AuthorizationService.CurrentUserID;
            return base.Add(entity);
        }

        public ActionResult submit(string id)
        {
            CarRepair e = this.m_ServiceBase.Get(id);
            e.mtlystate = 1;
            base.Update(e);

            PurchasePlanByEquip obj = new PurchasePlanByEquip();
            obj.PurchasePlan_NeedDate = e.RepairTime;
            obj.GoodsID = e.CarID;
            obj.PurchasePlan_reason = e.RepairReason;
            obj.PurchasePlan_planstate = 0;
            obj.PurchasePlan_state = 0;
            obj.PurchasePlan_claimer = AuthorizationService.CurrentUserID;
            obj.planmoney = 0.00m;
            obj._type = 1;
            obj.planmoney = e.summoney;
            obj.EquipMtLyID = e.ID;

            this.service.PurchasePlanByEquip.Add(obj);

            return OperateResult(true, Lang.Msg_Operate_Success, null);
        }
    }    
}
