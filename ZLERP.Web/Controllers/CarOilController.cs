
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Linq;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using System.Web.Mvc;
using ZLERP.Model.Enums;
using ZLERP.Resources;
using ZLERP.Model.ViewModels;
using System.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class CarOilController : BaseController<CarOil, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            var silos = this.service.Silo.Query()
                                 .Where(s => s.StuffInfo != null && s.StuffInfo.StuffType != null && s.StuffInfo.StuffType.TypeID == StuffTypeEnum.Oil.ToString())
                                 .ToList();
            ViewBag.Silos = new SelectList(silos, "ID", "SiloName");
            var carList = this.service.Car.GetCarSelectList(null).OrderBy(c => c.CarTypeID + c.ID);
            ViewBag.CarList = new SelectList(carList, "ID", "CarNo");

            //获取油价表设定的当前时间的油价
            CarOilPriceSetting cops = this.service.CarOil.getCarOilPrice(DateTime.Now.ToString());
            //CarOilPriceSetting cops = this.service.CarOil.getCarOilPrice("2016-03-10");
            ViewBag.OilPrice = cops.OilPrice;

            //获取油料密度(油料换算率)
            SysConfig config = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.OilDensity);
            if (config != null)
            {
                ViewBag.Rate = config.ConfigValue;
            }
            return base.Index();
        }

        /// <summary>
        /// 获取上一次加油的里程表信息
        /// </summary>
        /// <param name="carid"></param>
        /// <returns></returns>
        public ActionResult GetKiloMeter(string carid)
        {
            decimal value = this.service.CarOil.GetKiloMeter(carid);
            return OperateResult(true, Lang.Msg_Operate_Success, value);
        }
        /// <summary>
        /// 修改油价
        /// </summary>
        /// <param name="CarOilPrice"></param>
        /// <returns></returns>
        public ActionResult UpdateOilPrice(CarOilPrice CarOilPrice)
        {
            try
            {
                IList<CarOil> list = this.service.CarOil
                    .Query()
                    .Where(p => p.AddDate >= CarOilPrice.BeginTime && p.AddDate <= CarOilPrice.EndTime).ToList();
                //var list = this.service.CarOil
                //    .Query()
                //    .Where(p => p.AddDate >= CarOilPrice.BeginTime && p.AddDate <= CarOilPrice.EndTime);
                if (list.Count > 0)
                {
                    foreach (var a in list)
                    {
                        a.UnitPrice = CarOilPrice.Price;
                        a.TotalPrice = CarOilPrice.Price * a.Amount;
                        this.service.CarOil.Update(a, Request.Unvalidated().Form);
                    }
                }
                
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                log.Error(ex.Message, ex);
                return HandleExceptionResult(ex);

            }
        }
        /// <summary>
        /// 确认油价
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public ActionResult ConfirmOil0(decimal rate)
        {
            try
            {
                IList<CarOil> list = this.service.CarOil.Query().Where(p => p.Status == 0 || p.Status == null).ToList();
                var list2 = list.GroupBy(p=>p.StuffID).Select(g=>(new {StuffID = g.Key,Number = g.Sum(p=>p.Amount)})).ToList();
                foreach (var i in list2) {
                    StuffInfo stuffInfo = this.service.StuffInfo.Get(i.StuffID);
                    stuffInfo.Inventory = stuffInfo.Inventory - i.Number * rate;//减库存
                    this.service.StuffInfo.Update(stuffInfo, Request.Unvalidated().Form);
                }
                foreach (CarOil i in list)
                {
                    i.Status = 1;
                    this.service.CarOil.Update(i);
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                log.Error(ex.Message, ex);
                return HandleExceptionResult(ex);

            }
        }
        /// <summary>
        /// 确认加油记录（状态改为已确认）
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public ActionResult ConfirmOil(decimal rate)
        {
            try
            {
                IList<CarOil> list = this.service.CarOil.Query().Where(p => p.Status == 0 || p.Status == null).ToList();
                foreach (CarOil i in list)
                {
                    i.Status = 1;
                    this.service.CarOil.Update(i);
                }
                if(list.Count==0)
                {
                    return OperateResult(false, "没有加油记录需要确认！", null);
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
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
