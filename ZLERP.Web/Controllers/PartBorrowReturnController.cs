
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
    public class PartBorrowReturnController : BaseController<PartBorrowReturn, int>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            var partInfo = this.service.GetGenericService<PartInfo>().All("", "ID", true);
            IList<PartInfo> PartInfoList = new List<PartInfo>();
            PartInfo tmp=new PartInfo();
            tmp.ID="请选择";
            tmp.PartName = "请选择";
            PartInfoList.Add(tmp);
            foreach(PartInfo pi in partInfo )
            {
               // if (pi.ClassB.ClassID == "EquipType")
               // {
                    PartInfoList.Add(pi);
                     
              //  }
            }

            ViewBag.partInfoList = new SelectList(PartInfoList, "ID", "PartName"); ;
            return base.Index();
        }

        public override ActionResult Add(PartBorrowReturn entity)
        {
            try
            {
                this.service.PartBorrowReturn.Add(entity);
                return OperateResult(true, Lang.Msg_Operate_Success, entity.ID);
            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message, entity.ID);
            }
        }

      

        public override ActionResult Delete(int[] id)
        {
            try
            {

                this.service.PartBorrowReturn.Deletes(id);
                return OperateResult(true, Lang.Msg_Operate_Success, 0);
            }
            catch
            {
                return OperateResult(true, Lang.Msg_Operate_Failed, 0);
            }
        }

    }    
}
