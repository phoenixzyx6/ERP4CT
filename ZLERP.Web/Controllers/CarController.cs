using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class CarController : BaseController<Car,string>
    {
        public override ActionResult Index()
        {
            ViewBag.DepartmentList = HelperExtensions.SelectListData<Department>("DepartmentName", "ID", "", "DepartmentName", true, "");
            //ViewBag.UserList = HelperExtensions.SelectListData<User>("TrueName", "ID", "IsUsed=1 AND (UserType ='" + UserType.Driver + "' OR UserType ='" + UserType.MixerDriver + "')", "TrueName", true, "");
            ViewBag.CarClassList = HelperExtensions.SelectListData<CarClass>("CarClassName", "ID", "", "CarClassName", true, "");

            var parentCompany = this.service.GetGenericService<Company>().All("", "ID", true);
            ViewBag.CompanyDics = new SelectList(parentCompany, "ID", "CompName");
            //ViewBag.CompanyList = HelperExtensions.SelectListData<Company>("CompName", "ID", "", "CompName", true, "");
            return base.Index();
        }

        [HttpPost]
        public override ActionResult Add(Car car)
        {            
            ActionResult result = base.Add(car);
            AddCarHistory(car, result);
            return result;
        }
        [HttpPost]
        public override ActionResult Update(Car car)
        {
            ActionResult result = base.Update(car);
            AddCarHistory(car, result);
            return result;
        }

        [HttpPost]
        public override ActionResult Delete(string[] id)
        {
            ActionResult result = base.Delete(id);
            DeleCarHistory(id, result);
            return result;
        }

        /// <summary>
        /// 添加历史记录
        /// </summary>
        /// <param name="car"></param>
        /// <param name="result"></param>
         private void AddCarHistory(Car car,ActionResult result)
         {
             if (!string.IsNullOrEmpty(car.ID) && (((ResultInfo)((JsonResult)result).Data).Result))
             {
                 //获得最后一条的历史数据
                 CarHistory carHistory = this.service.CarHistory.GetLastData(car.ID);
                 if (carHistory != null)
                 {
                     if (carHistory.CarWeight == car.CarWeight)
                     {
                         //未修改皮重 不增加历史记录
                         return;
                     }
                 }

                 CarHistory _CarHistory = new CarHistory();
                 _CarHistory.BelongTo = car.BelongTo;
                 _CarHistory.Builder = car.Builder;
                 _CarHistory.BuildTime = car.BuildTime;
                 _CarHistory.BuyDate = car.BuyDate;
                 _CarHistory.CarClass = car.CarClass;
                 _CarHistory.CarClassID = car.CarClassID;
                 //_CarHistory.CarClassName = car.CarClassName;
                 _CarHistory.CarID = car.ID;
                 _CarHistory.CarNo = car.CarNo;
                 _CarHistory.CarStatus = car.CarStatus;
                 _CarHistory.CarTypeID = car.CarTypeID;
                 _CarHistory.CarWeight = car.CarWeight;
                 _CarHistory.ChassisID = car.ChassisID;
                 _CarHistory.DriverForCars = car.DriverForCars;
                 _CarHistory.EngineID = car.EngineID;
                 _CarHistory.FacInnerID = car.FacInnerID;
                 _CarHistory.Factory = car.Factory;
                 _CarHistory.GPSStatus = car.GPSStatus;
                 _CarHistory.IsUsed = car.IsUsed;
                 _CarHistory.LastBackTime = car.LastBackTime;
                 _CarHistory.LeaveFacDate = car.LeaveFacDate;
                 _CarHistory.Lifecycle = car.Lifecycle;
                 _CarHistory.MaxCube = car.MaxCube;
                 _CarHistory.Modifier = car.Modifier;
                 _CarHistory.ModifyTime = DateTime.Now;
                 _CarHistory.OilConsume = car.OilConsume;
                 _CarHistory.OrderNum = car.OrderNum;
                 _CarHistory.Owner = car.Owner;
                 _CarHistory.PumpTypeID = car.PumpTypeID;
                 _CarHistory.Remark = car.Remark;
                 _CarHistory.RentCarName = car.RentCarName;
                 _CarHistory.SIM = car.SIM;
                 _CarHistory.StuffWeight = car.StuffWeight;
                 _CarHistory.TerminalID = car.TerminalID;
                 _CarHistory.Version = car.Version;
                 _CarHistory.CompanyID = car.CompanyID;

                 this.service.CarHistory.Add(_CarHistory);
             }
         }
        /// <summary>
         /// 删除历史记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="result"></param>
         private void DeleCarHistory(string[] id, ActionResult result)
         {
             if (id == null) return;
             if (((ResultInfo)((JsonResult)result).Data).Result)
             {
                 for (int i = 0; i < id.Length; i++)
                 {
                     //查找此车下属的历史信息
                     string[] ids = this.service.CarHistory.GetHistoryIDByCarID(id[i]);
                     if (ids == null || ids.Length == 0)
                         continue;
                     this.service.CarHistory.Delete(ids);
                 }
             }
         }

        /// <summary>
        /// 根据car获取司机、容重等。调度使用。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetInfo(string id)
        {
            dynamic car = this.service.Car.GetInfo(id);
            return this.Json(car);
        }


        /// <summary>
        /// 获取车辆信息，根据状态进行分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetMixerCarStatus()
        {
            dynamic car = this.service.Car.GetMixerCarStatus();
            return this.Json(car);
        }

        /// <summary>
        /// 修改车辆状态，调度界面使用。
        /// </summary>
        /// <param name="operType"></param>
        /// <param name="carId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ActionResult ShiftMixerCarStatus(string operType, string carId, string status) 
        {
            this.service.Car.ShiftMixerCarStatus(operType, carId, status);
            return OperateResult(true, Lang.Msg_Operate_Success, null);
        }

        /// <summary>
        /// 拖放搅拌车操作
        /// </summary>
        /// <param name="carid"></param>
        /// <param name="orders"></param>
        /// <returns></returns>
        public ActionResult DragMixerCar(string carid, string[] orders)
        {
            try
            {
                this.service.Car.DragMixerCar(carid, orders);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, null);
            }
        }

        /// <summary>
        /// 明天可用车辆
        /// </summary>
        /// <param name="availableCar">可用车辆</param>
        /// <param name="carNum">车辆总数</param>
        /// <returns></returns>
        public ActionResult AddAvailableCar(string availableCar, string carNum)
        {
            try
            {
                this.service.Car.AddAvailableCar(availableCar, carNum);
                return OperateResult(true, Lang.Msg_Operate_Success, null);

            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        /// <summary>
        /// GPS车辆状态修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult GPSChangeCarStatus(string id, string code)
        {
            try
            {
                this.service.Car.GPSChangeCarStatus(id, code);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

    }
}
