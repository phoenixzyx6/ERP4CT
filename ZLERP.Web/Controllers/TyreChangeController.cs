
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Linq;

using System.Web.Mvc;
using ZLERP.Model.Enums;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class TyreChangeController : BaseController<TyreChange, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            var carList = this.service.Car.GetCarSelectList(null);
            carList.Insert(0, new Car());
            ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");

            IList<TyreInfo> tyrelist = this.service.GetGenericService<TyreInfo>().Query()
                .Where(t => t.CurrentStatus == TyreStatus.UsAble).ToList();
            tyrelist.Insert(0, new TyreInfo());
            ViewBag.TyreInfoDics = new SelectList(tyrelist, "ID", "ID");
            var userInfo = this.service.GetGenericService<User>().All("", "ID", true);
            ViewBag.UserInfoDics = new SelectList(userInfo, "ID", "TrueName");
            var carclasslist=this.service.GetGenericService<CarClass>().Query().ToList();
            ViewBag.CarClass = new SelectList(carclasslist, "ID", "CarClassName");

            return base.Index();
        }

        /// <summary>
        /// 获取上一次更换的轮胎
        /// </summary>
        /// <param name="carid"></param>
        /// <returns></returns>
        public ActionResult GetCarLastChange(string carid)
        {
            TyreChange tc = this.m_ServiceBase.Query().Where(t => t.CarID == carid)
                .OrderByDescending(t => t.ChangeDate).FirstOrDefault();
            return OperateResult(true, Lang.Msg_Operate_Success, tc);
        }

        /// <summary>
        /// 增加轮胎更换记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="OldTyreAction"></param>
        /// <returns></returns>
        public  ActionResult AddTyreChange(TyreChange entity, string OldTyreAction)
        {
            TyreInfo _oldTyre = this.service.GetGenericService<TyreInfo>().Get(entity.OldTyreID);
            TyreInfo _newTyre = this.service.GetGenericService<TyreInfo>().Get(entity.NewTyreID);
            #region 合法性逻辑校验
            if (_oldTyre.CarID != entity.CarID)
            {
                return OperateResult(false, "轮胎【" + _oldTyre.ID + "】没有安装在车辆" + entity.CarID + "号上",null);
            }
            if (_newTyre.CurrentStatus != TyreStatus.UsAble)
            {
                return OperateResult(false, "轮胎【" + _newTyre.ID + "】不在待用状态", null);
            }
            #endregion
            try
            {
                this.service.TyreChange.AddChange(entity, OldTyreAction);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message, null);
            }
        }

    }    
}
