
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web.Mvc;

namespace ZLERP.Web.Controllers
{
    public class PartBorrowItemController : BaseController<PartBorrowItem, int>
    {
        public override ActionResult Add(PartBorrowItem PartBorrowItem)
        {

            PartInfo part = this.service.GetGenericService<PartInfo>().Get(PartBorrowItem.PartID);
            if (PartBorrowItem.BorrowNum > part.Inventory)
            {
                string str = String.Format("借用数量{0}大于当前库存量{1}", PartBorrowItem.BorrowNum, part.Inventory);
                return OperateResult(false, str, false);
            }
            else
                return base.Add(PartBorrowItem);
        }
        public override ActionResult Update(PartBorrowItem PartBorrowItem)
        {
            return base.Update(PartBorrowItem);
        }

        public override JsonResult ListData(string textField, string valueField, string orderBy = "ID", bool ascending = false, string condition = "")
        {
            return base.ListData(textField, valueField, orderBy, ascending, condition);
        }
    }
}
