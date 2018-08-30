using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class StuffInPriceAdjustController : BaseController<StuffInPriceAdjust, string>
    {
        /// <summary>
        /// 入库单价调整（批量）存错过程
        /// </summary>
        /// <param name="CarOilPrice"></param>
        /// <returns></returns>
        public ActionResult StuffInPriceAdjust(string beginDate, string endDate)
        {
            try
            {
                //IList<MonthAccount> mlist = this.service.GetGenericService<MonthAccount>().All("Month='" + Month + "'", "Month", true);
                //if (mlist.Count > 0)
                //{
                //    return OperateResult(false, Month + "月份已经月结，不能再进行月结！", null);

                //}

                //IList<MonthAccount> mlist2 = this.service.GetGenericService<MonthAccount>().All("1=1", "Month", true);
                //var m = mlist2.FirstOrDefault();
                //if (m != null && Convert.ToInt64(m.Month) >= Convert.ToInt64(Month))
                //{
                //    return OperateResult(false, "不能比已经月结过的月份小，请选择大于最后月结的月份！", null);
                //}

                //if (mlist2.Count != 0)
                //{
                //    TimeSpan ts = DateTime.Now - Convert.ToDateTime(m.Buildtime);
                //    if (ts.Days < 30)
                //    {
                //        return OperateResult(false, "与上一次的月结间隔不能少于30天！", null);
                //    }
                //}

                this.service.StuffInPriceAdjust.StuffInPriceAdjustOper(beginDate, endDate);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                log.Error(ex.Message, ex);
                return HandleExceptionResult(ex);

            }
        }

    }
}
