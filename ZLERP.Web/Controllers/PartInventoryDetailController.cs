
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class PartInventoryDetailController : BaseController<PartInventoryDetail, int>
    {
        public override System.Web.Mvc.ActionResult Add(PartInventoryDetail PartInventoryDetail)
        {
            //盘点审核后才更新库存
            //PartInfo partInfo = this.service.GetGenericService<PartInfo>().Get(PartInventoryDetail.PartID);
            //partInfo.Inventory = PartInventoryDetail.ActualValue;
            return base.Add(PartInventoryDetail);
        }
        public override System.Web.Mvc.ActionResult Update(PartInventoryDetail PartInventoryDetail)
        {
          
            return base.Update(PartInventoryDetail);
        }
    }    
}
