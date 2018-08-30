
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class ProductRecordItemController : BaseController<ProductRecordItem, int>
    {

        public override System.Web.Mvc.ActionResult Add(ProductRecordItem ProductRecordItem)
        {

            try
            {
                ProductRecordItem temp = this.service.ProductRecordItemService.Add(ProductRecordItem);
                if (temp != null)
                    return OperateResult(true,  "  操作成功", null);
                else
                    return OperateResult(false, "  操作失败", null);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                return HandleExceptionResult(ex);
            }
        }

        public override ActionResult Delete(int[] id)
        {
            ProductRecordItem item;
            ThreadID tid;
            for (int i = 0; i < id.Length; i++)
            {
                 item= this.m_ServiceBase.Get(id[i]);
                 if (item != null)
                 {
                     tid = new ThreadID();
                     tid.currentDate = DateTime.Now;
                     tid.typeID = item.StuffID;//主材id
                     tid.typename = "0";//主材
                     this.service.ThreadID.Add(tid);
                 }
            }

            return base.Delete(id);
        }
    }
}
