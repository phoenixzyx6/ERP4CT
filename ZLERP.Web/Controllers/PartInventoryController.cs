
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using System.Web.Mvc;
using ZLERP.Resources;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    public class PartInventoryController : BaseController<PartInventory, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.PartInfoList = HelperExtensions.SelectListData<PartInfo>("PartName", "ID", "", "ID", true, "");
            return base.Index();
        }
        public override System.Web.Mvc.ActionResult Add(PartInventory PartInventory)
        {
            return base.Add(PartInventory);
        }


        public override ActionResult Update(PartInventory PartInventory)
        {
            return base.Update(PartInventory);
        }



        public virtual ActionResult Audit(string[] id)
        {
            try
            {
                IList<PartInventoryDetail> partInventoryDetailList = this.service.GetGenericService<PartInventoryDetail>().All("InventoryID = '" + id[0] + "'", "InventoryID", true);
                if (partInventoryDetailList != null)
                {
                    ServiceBase < PartInfo > partInfoventoryService = this.service.GetGenericService<PartInfo>();
                    foreach (PartInventoryDetail partInventoryDetai in partInventoryDetailList)
                    {
                        PartInfo partInfo = partInfoventoryService.Get(partInventoryDetai.PartID);
                        partInfo.Inventory = partInventoryDetai.ActualValue;
                        partInfoventoryService.Update(partInfo, null);

                    }
                }
                string userId = AuthorizationService.CurrentUserID;
                ServiceBase<PartInventory> partInventoryService = this.service.GetGenericService<PartInventory>();
                PartInventory partInventory = partInventoryService.Get(id[0]);
                partInventory.Auditor = userId;
                partInventory.AuditTime = DateTime.Now;
                partInventory.AuditStatus = 1;
                partInventoryService.Update(partInventory, null);

                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                log.Error(ex.Message, ex);
                return OperateResult(false, Lang.Msg_Operate_Failed, null);

            }
        }
    }    
}
