using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class ManualProduceController : BaseController<ManualProduce, string>
    {
        //
        // GET: /ManualProduce/

        public override ActionResult Index()
        {
            IList<ProductLine> items = base.service.ProductLine.All("IsUsed = 1", "ID", true);
            ((dynamic)base.ViewBag).ProductLineList = new SelectList(items, "ID", "ProductLineName");
            //材料列表
            ViewBag.StuffList = HelperExtensions.SelectListData<StuffInfo>("StuffName",
                "ID",
                "IsUsed=1",
                "StuffName",
                true, "");

            return base.Index();
        }
        /// <summary>
        /// 通过生产登记号获取生产详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetDispatchInfoByID(string id)
        {
            try
            {
                object obj2 = base.service.ManualProduce.getDispatchInfoByID(id);
                return (ActionResult)this.Json((dynamic)obj2);
            }
            catch (Exception)
            {
                return this.OperateResult(false, null, null);
            }
        }

 


 

    }
}
