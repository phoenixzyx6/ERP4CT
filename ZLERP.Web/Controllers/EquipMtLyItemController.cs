
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
    public class EquipMtLyItemController : BaseController<EquipMtLyItem, int?>
    {

        public override ActionResult Add(EquipMtLyItem EquipMtLyItem)
        {
            //ServiceBase<PartInfo> partInfoService = this.service.GetGenericService<PartInfo>();
            //PartInfo partInfo = partInfoService.Get(EquipMtLyItem.PartID);
            //partInfo.Inventory -= (int)EquipMtLyItem.Amount;
            //if (partInfo.Inventory < 0)
            //{
            //    string str = String.Format("领用数量{0}大于当前库存量", EquipMtLyItem.Amount);
            //    return OperateResult(false, str, false);
            //}
            //else
            //{
            //    partInfoService.Update(partInfo, null);
            //    return base.Add(EquipMtLyItem);
            //}
            return base.Add(EquipMtLyItem);
        }

        public override ActionResult Update(EquipMtLyItem EquipMtLyItem)
        {
            //EquipMtLyItem OldEquipMtLyItem = this.service.GetGenericService<EquipMtLyItem>().All("EquipMtLyItemID='" + EquipMtLyItem.ID + "'", "ID", true)[0];
            //ServiceBase<PartInfo> partInfoService = this.service.GetGenericService<PartInfo>();
            //PartInfo partInfo = partInfoService.Get(OldEquipMtLyItem.PartID);
            //if (EquipMtLyItem.Amount != OldEquipMtLyItem.Amount )
            //{
            //    int count = OldEquipMtLyItem.Amount - EquipMtLyItem.Amount;
            //    partInfo.Inventory += count;
            //    if (partInfo.Inventory < 0)
            //    {
            //        string str = String.Format("领用数量{0}大于当前库存量", EquipMtLyItem.Amount);
            //        return OperateResult(false, str, false);
            //    }
            //}
            //partInfoService.Update(partInfo, null);
            return base.Update(EquipMtLyItem);
        }


        public override ActionResult Delete(int?[] id)
        {


           

            //foreach (object item in id)
            //{
            //    EquipMtLyItem entity = m_ServiceBase.Get(item);
            //    if (entity != null && entity.MntZlItemID==null)
            //    {

            //        ServiceBase<PartInfo> partInfoService = this.service.GetGenericService<PartInfo>();
            //        PartInfo partInfo = partInfoService.Get(entity.PartID);
            //        partInfo.Inventory += (int)entity.Amount;
            //        partInfoService.Update(partInfo, null);
            //    }
            //}

            return base.Delete(id);
        }

  
   }
}