using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;
using ZLERP.NHibernateRepository;
using Lib.Web.Mvc.JQuery.JqGrid;
using System.Collections;

namespace ZLERP.Web.Controllers
{
    public class MonthAccountController : BaseController<MonthAccount, string>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override ActionResult Index()
        {           
            return base.Index();
        }

        /// <summary>
        /// 取月结月份列表（每月份取一行）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMonthAccount(JqGridRequest request)
        {
            int total = 0;
            MonthAccount a = new Model.MonthAccount();
            a.Clone();
            
            //IList<MonthAccount> mlist = this.service.GetGenericService<MonthAccount>().Find(request, "1=1", out total);
               
            //IList<MonthAccount> mlist1 = this.service.GetGenericService<MonthAccount>().Query().ToList();
            IList<string> columns = new List<string>();
            columns.Add("Month");
            var mlist = this.service.MonthAccount.All(columns, "", "", false).Distinct().OrderBy(p => p.Month).ToList();//按“月份”过滤重复行
            JqGridData<MonthAccount> data = new JqGridData<MonthAccount>()
            {
                page =request.PageIndex,
                records = total,
                pageSize = request.RecordsCount,
                rows = mlist
            };
            return Json(data);
        }

        /// <summary>
        /// 月结处理
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public ActionResult MonthAccount(string Month)
        {
            try
            {
                IList<MonthAccount> mlist = this.service.GetGenericService<MonthAccount>().All("Month='" + Month + "'", "Month", true);               
                if (mlist.Count > 0)
                {
                    return OperateResult(false, Month + "月份已经月结，不能再进行月结！", null);

                }

                IList<MonthAccount> mlist2 = this.service.GetGenericService<MonthAccount>().All("1=1", "Month", true);
                var m = mlist2.FirstOrDefault();
                if (m!=null&&Convert.ToInt64(m.Month)>=Convert.ToInt64(Month))
                {
                    return OperateResult(false, "不能比已经月结过的月份小，请选择大于最后月结的月份！", null);
                }

                if (mlist2.Count != 0)
                {
                    TimeSpan ts = DateTime.Now - Convert.ToDateTime(m.Buildtime);
                    if (ts.Days < 30)
                    {
                        return OperateResult(false, "与上一次的月结间隔不能少于30天！", null);
                    }
                }

                //插入当前材料的月结
                //IList<Silo> list = this.service.Silo.Query().ToList();
                IList<Silo> list = this.service.GetGenericService<Silo>().All("SiloID IN (SELECT SiloID from SiloProductLine)", "ID", false);
                foreach (var i in list)
                {
                    MonthAccount monthAccount =new MonthAccount();
                    monthAccount.Month = Month;//
                    monthAccount.Siloid = i.ID;
                    monthAccount.Stuffid = i.StuffID;
                    monthAccount.Begindate = null;
                    monthAccount.Enddate = null;
                    monthAccount.Currentcount = i.Content/1000;
                    monthAccount.Currentamount = 0;
                    monthAccount.Builder = "";
                    monthAccount.Buildtime = DateTime.Now;
                    this.service.MonthAccount.Add(monthAccount);
                }

                //更新入库记录状态为：已月结
                IList<StuffIn> stuffinlist = this.service.StuffIn.Query().Where(p=>p.IsMonth==false).ToList();
                foreach (var sl in stuffinlist)
                {
                    sl.IsMonth = true;
                    this.service.StuffIn.Update(sl,null);
                }

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
        /// <summary>
        /// 月结处理（存储过程）
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public ActionResult MonthAccountOper(string Month)
        {
            try
            {
                IList<MonthAccount> mlist = this.service.GetGenericService<MonthAccount>().All("Month='" + Month + "'", "Month", true);
                if (mlist.Count > 0)
                {
                    return OperateResult(false, Month + "月份已经月结，不能再进行月结！", null);

                }

                IList<MonthAccount> mlist2 = this.service.GetGenericService<MonthAccount>().All("1=1", "Month", true);
                var m = mlist2.FirstOrDefault();
                if (m != null && Convert.ToInt64(m.Month) >= Convert.ToInt64(Month))
                {
                    return OperateResult(false, "不能比已经月结过的月份小，请选择大于最后月结的月份！", null);
                }

                if (mlist2.Count != 0)
                {
                    TimeSpan ts = DateTime.Now - Convert.ToDateTime(m.Buildtime);
                    if (ts.Days < 30)
                    {
                        return OperateResult(false, "与上一次的月结间隔不能少于30天！", null);
                    }
                }

                this.service.MonthAccount.MonthAccountOper(Month);
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
