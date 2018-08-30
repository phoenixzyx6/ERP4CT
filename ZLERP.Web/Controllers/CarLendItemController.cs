
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Resources;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class CarLendItemController : BaseController<CarLendItem, int>
    {
        public override System.Web.Mvc.ActionResult Add(CarLendItem CarLendItem)
        {
            var CarLendItemList = this.service.GetGenericService<CarLendItem>().All("(BackTime is null or CarLendID='" + CarLendItem.CarLendID + "') and CarID='" + CarLendItem.CarID + "'", "ID", true);

            if (CarLendItemList.Count > 0)
            {
                return OperateResult(false, "此出租列表已包含该车辆信息或者其它出租列表中该车辆未回厂", null);
            }
            else
            {
                Car obj = this.service.Car.Get(CarLendItem.CarID);
                obj.CarStatus = CarStatus.RentCar;
                this.service.Car.Update(obj);
                return base.Add(CarLendItem);
            }
        }

        public override System.Web.Mvc.ActionResult Update(CarLendItem CarLendItem)
        {
            CarLendItem oldCarLendItem = this.service.GetGenericService<CarLendItem>().Get(CarLendItem.ID);            
            Car obj = this.service.Car.Get(CarLendItem.CarID);
            if (obj.CarStatus == CarStatus.RentCar && CarLendItem.BackTime != null && oldCarLendItem.BackTime == null)
            {
                this.service.Car.ChangeCarStatus(CarLendItem.CarID, CarStatus.AllowShipCar, CarLendItem.BackTime);
            }

            if (CarLendItem.BackTime == null && oldCarLendItem.BackTime != null)
            {
                return OperateResult(false, "该记录已经回厂", null);
            }

            return base.Update(CarLendItem);
        }

        public ActionResult AddAllCars(string id)
        {
            try
            {
                this.service.CarLendItem.AddAllCars(id);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
    }
}
