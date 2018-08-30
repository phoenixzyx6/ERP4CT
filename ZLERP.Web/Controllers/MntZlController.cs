
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ZLERP.Web.Controllers.Attributes;
using ZLERP.Model;
using System.Linq;
using ZLERP.Web.Helpers;
using ZLERP.Resources;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    public class MntZlController : BaseController<MntZl, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.PartType = Request.QueryString["p1"];
            ViewBag.Users = HelperExtensions.SelectListData<User>("TrueName", "ID", "", "ID", true, "");
            ViewBag.Departments = HelperExtensions.SelectListData<Department>("DepartmentName", "ID", "", "ID", true, "");
            ViewBag.ClassBPart = HelperExtensions.SelectListData<ClassB>("ClassBName", "ID", "ClassID ='" + Request.QueryString["p1"] + "'", "ID", true, "");
            ViewBag.ClassMPartInit = HelperExtensions.SelectListData<ClassM>("ClassMName", "ID", "ClassID ='" + Request.QueryString["p1"] + "'", "ID", true, "");
            ViewBag.ClassSPartInit = HelperExtensions.SelectListData<Classs>("ClassSName", "ID", "ClassID ='" + Request.QueryString["p1"] + "'", "ID", true, "");
            ViewBag.PartInfo = HelperExtensions.SelectListData<PartInfo>("PartName","ID","","ID",true,"");
            ViewBag.ClassB = HelperExtensions.SelectListData<ClassB>("ClassBName", "ID", "ClassID ='EquipType'", "ID", true, "");
            return base.Index();
        }
        public override ActionResult Add(MntZl MntZl)
        {
            return base.Add(MntZl);
        }

        public override ActionResult Update(MntZl MntZl)
        {
            if (ModelState.IsValid)
            {
                return base.Update(MntZl);
            }
            else {
                var m = ModelState.Values.Where(p => p.Errors.Count > 0)
                    .Select(c => string.Join(",", c.Errors.Select(p => p.ErrorMessage).ToList())).ToList();

                return OperateResult(false, string.Join("<br/>", m), null);
            }
        }

        //审核
        public ActionResult Audit(MntZl MntZl) {
            try
            {
                this.service.MntZl.Audit(MntZl);
                
                this.service.SysLog.Log(Model.Enums.SysLogType.Audit, MntZl.ID, null, "设备支领审核");
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }


        //经理审核
        public ActionResult ManageAudit(MntZl MntZl)
        {
            try
            {
                this.service.MntZl.ManageAudit(MntZl);
                if (MntZl.ReAuditStatus == 1)
                {
                    IList<MntZlItem> mntZlItemList = this.service.GetGenericService<MntZlItem>().All("MntZlID= '" + MntZl.ID + "'", "MntZlID", true);
                    if (mntZlItemList != null)
                    {
                        ServiceBase<PartInfo> partInfoService = this.service.GetGenericService<PartInfo>();
                        foreach (MntZlItem mntZlItem in mntZlItemList)
                        {
                            PartInfo partInfo = partInfoService.Get(mntZlItem.PartID);
                            if (mntZlItem.amount != null)
                            {
                                partInfo.Inventory -= (int)mntZlItem.amount;

                                if (partInfo.Inventory < 0)
                                {
                                    string str = String.Format("领用数量{0}大于当前库存量，审核不通过！", mntZlItem.amount);
                                    return OperateResult(false, str, false);
                                }
                            }
                            partInfoService.Update(partInfo, null);
                        }
                    }
                }
                this.service.SysLog.Log(Model.Enums.SysLogType.Audit, MntZl.ID, null, "设备支领审核");
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        //批次审核
        public override ActionResult BatchAudit(string[] ids) {
            try
            {
                this.service.MntZl.BatchAudit(ids);
                this.service.SysLog.Log(Model.Enums.SysLogType.Audit, ids.ToString(), null, "设备支领批次审核");
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        /*
         * 批次审核页面
         */
        public virtual ActionResult AuditBatch() {
            return View();
        }
    }    
}
