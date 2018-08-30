
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    public class PartInController : BaseController<PartIn, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {

            ViewBag.PartInfoList = HelperExtensions.SelectListData<PartInfo>("PartName", "ID", "", "ID", true, "");
            return base.Index();
        }
        public override System.Web.Mvc.ActionResult Add(PartIn PartIn)
        {
            return base.Add(PartIn);
        }

        public override System.Web.Mvc.ActionResult Update(PartIn PartIn)
        {
            return base.Update(PartIn);
        }
        public override System.Web.Mvc.ActionResult Auditing(string id, int auditstatus, DateTime? audittime, string auditor, string auditInfo)
        {
            ServiceBase<PartInItem> partInItemService =  this.service.GetGenericService<PartInItem>();
            IList<PartInItem>  partInItemList  = partInItemService.All("PartInID = '" + id + "'", "PartInID",true);
            if(partInItemList!=null){
                ServiceBase<PartInfo>  partInfoService = this.service.GetGenericService<PartInfo>();
                foreach (PartInItem partInItem in partInItemList) {
                    PartInfo partInfo = partInfoService.Get(partInItem.PartInfoID);
                    partInfo.Inventory += partInItem.InNum;
                    partInfoService.Update(partInfo, null);
                }
            }
            return base.Auditing(id, auditstatus, audittime, auditor, auditInfo);
        }
    }
}
