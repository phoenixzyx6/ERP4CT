
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    public class EquipTermlyMtItemController : BaseController<EquipTermlyMtItem, int>
    {
        public override System.Web.Mvc.ActionResult Add(EquipTermlyMtItem EquipTermlyMtItem)
        {
            ServiceBase<PartInfo> partInfoService = this.service.GetGenericService<PartInfo>();
            PartInfo partInfo = partInfoService.Get(EquipTermlyMtItem.PartID);
            if(EquipTermlyMtItem.Amount!=null){
                partInfo.Inventory -= (int)EquipTermlyMtItem.Amount;
            }
            partInfoService.Update(partInfo, null);
            return base.Add(EquipTermlyMtItem);
        }

        public override System.Web.Mvc.ActionResult Update(EquipTermlyMtItem EquipTermlyMtItem)
        {
            EquipTermlyMtItem OldEquipTermlyMtItem = this.service.GetGenericService<EquipTermlyMtItem>().All("EquipTermlyMtItemID='" + EquipTermlyMtItem.ID + "'", "ID", true)[0];
            ServiceBase<PartInfo> partInfoService = this.service.GetGenericService<PartInfo>();
            PartInfo partInfo = partInfoService.Get(OldEquipTermlyMtItem.PartID);
            if (EquipTermlyMtItem.Amount != OldEquipTermlyMtItem.Amount && EquipTermlyMtItem.Amount!=null)
            {
                int count = (OldEquipTermlyMtItem.Amount == null ? 0 : (int)OldEquipTermlyMtItem.Amount) - (EquipTermlyMtItem.Amount == null ? 0 : (int)EquipTermlyMtItem.Amount);
                partInfo.Inventory += count;
            }
            
            partInfoService.Update(partInfo,null);
            return base.Update(EquipTermlyMtItem);
        }
       /* public override System.Web.Mvc.ActionResult Delete(int[] id)
        {


           

            foreach (object item in id)
            {
                EquipTermlyMtItem entity = m_ServiceBase.Get(item);
                if (entity != null)
                {

                    ServiceBase<PartInfo> partInfoService = this.service.GetGenericService<PartInfo>();
                    PartInfo partInfo = partInfoService.Get(entity.PartID);
                    if (entity.Amount != null)
                    {
                        partInfo.Inventory += (int)entity.Amount;
                    }
                    partInfoService.Update(partInfo, null);
                }
            }

            return base.Delete(id);
        }*/
    }    
}
