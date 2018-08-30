
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class EquipmentItemController : BaseController<EquipmentItem, string>
    {
        public override System.Web.Mvc.ActionResult Add(EquipmentItem EquipmentItem)
        {
            var equipmentItemList = this.service.GetGenericService<EquipmentItem>().All("EquipmentID='" + EquipmentItem.EquipmentID + "'  and MaintenanceID='" + EquipmentItem.MaintenanceID + "'", "ID", true);

            if (equipmentItemList.Count > 0) {
                return OperateResult(false, "该设备下已存在该保养项目", null);
            
            }
            return base.Add(EquipmentItem);
        }

        public override System.Web.Mvc.ActionResult Update(EquipmentItem EquipmentItem)
        {

            var equipmentItemList = this.service.GetGenericService<EquipmentItem>().All("EquipmentID='" + EquipmentItem.EquipmentID + "'  and MaintenanceID='" + EquipmentItem.MaintenanceID  + "'  and EquipmentItemID !='" + EquipmentItem.ID + "'", "ID", true);

            if (equipmentItemList.Count > 0)
            {
                return OperateResult(false, "该设备下已存在该保养项目", null);

            }
            return base.Update(EquipmentItem);
        }
    }    
}
