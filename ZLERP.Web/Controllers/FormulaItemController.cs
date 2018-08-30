using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class FormulaItemController : BaseController<FormulaItem,int>
    {
        

        public override ActionResult Add(FormulaItem ForItem)
        {
            return base.Add(ForItem);
        }


        public ActionResult IsEnableEdit(int id, decimal StuffAmount)
        {
            try
            {
                bool result = this.service.FormulaItem.IsEnableEdit(id, StuffAmount);
                return OperateResult(result, Lang.Msg_Operate_Success, result);

            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message, false);
            }
        }
    }
}
