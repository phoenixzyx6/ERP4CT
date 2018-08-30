using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class ProduceTaskOtherPriceController : BaseController<ProduceTaskOtherPrice, int>
    {
        public override ActionResult Add(ProduceTaskOtherPrice entity)
        {
           IList<ProduceTaskOtherPrice> OtherPriceList = this.service.GetGenericService<ProduceTaskOtherPrice>().Query().Where(p=>(p.OtherPriceID==entity.OtherPriceID && p.ProduceTaskID == entity.ProduceTaskID)).ToList();
           if (OtherPriceList.Count > 0)
           {
               return OperateResult(false, "已经存在该项目！", entity);
           }
           else
           {
               return base.Add(entity);
           }
        }

    }
}
