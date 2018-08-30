
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class CarClassItemController : BaseController<CarClassItem, int>
    {
        public override System.Web.Mvc.ActionResult Add(CarClassItem CarClassItem)
        {
            IList<CarClassItem> CarClassItemList = this.service.GetGenericService<CarClassItem>().All("CarClassID='"+ CarClassItem.CarClassID +"' and TyrPlace='"+ CarClassItem.TyrPlace +"'","ID",true);
            if (CarClassItemList.Count > 0)
            {

                return OperateResult(false, Lang.IsExistRecord, null); 
            }
            return base.Add(CarClassItem);
        }

        public override System.Web.Mvc.ActionResult Update(CarClassItem CarClassItem)
        {
            CarClassItem FindCarClassItem = this.service.GetGenericService<CarClassItem>().Get(CarClassItem.ID);
            string CarClassID = FindCarClassItem.CarClassID; 
            IList<CarClassItem> CarClassItemList = this.service.GetGenericService<CarClassItem>().All("CarClassID='" + CarClassID + "' and TyrPlace='" + CarClassItem.TyrPlace + "' and CarClassItemID <> " + CarClassItem.ID, "ID", true);
            if (CarClassItemList.Count > 0)
            {
                return OperateResult(false, Lang.IsExistRecord, null);
            }
            return base.Update(CarClassItem);
        }


        public JsonResult TyrePlaceListData(string textField, string valueField, string orderBy = "ID", bool ascending = false,
          string foreignValue = "")
        {
            Car c = this.service.Car.Get(foreignValue);

            return base.ListData(textField, valueField, orderBy, ascending, "CarClassID='"+c.CarClassID+"'");
           
        }
    }    
}
