using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Model.Enums;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class DutyPlanController : BaseController<DutyPlan, string>
    {
        //
        // GET: /DutyPlan/

        public override ActionResult Index()
        {
            ((dynamic)base.ViewBag).ChangeDay = base.service.SysConfig.GetSysConfig(SysConfigEnum.ChangeDay).ConfigValue;

            ViewBag.dispatchers = HelperExtensions.SelectListData<ZLERP.Model.User>("TrueName", "ID", " UserType = '02' AND IsUsed=1 ", "ID", true, "");
            return base.Index();
        }
        /// <summary>
        /// 增加值班计划
        /// </summary>
        /// <param name="beginDate"></param>
        /// <returns></returns>
        public ActionResult SignAdd(string beginDate,string sd,string md)
        {
            try
            {
                DateTime time = DateTime.Parse(beginDate);
                DutyPlan entity = new DutyPlan
                {
                    ID = time.ToString("yyyyMMdd"),
                    DutyDate = time,
                    SecondDispatcher=sd,
                    MainDispatcher=md
                };
                base.service.DutyPlan.Add(entity);
                return this.OperateResult(true, ZLERP.Resources.Lang.Msg_Operate_Success, null);
            }
            catch (Exception exception)
            {
                return this.OperateResult(false, ZLERP.Resources.Lang.Msg_Operate_Failed + exception.Message, null);
            }
        }
        /// <summary>
        /// 批量增加值班计划
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ActionResult MultADD(string beginDate, string endDate)
        {
            try
            {
                base.service.DutyPlan.MultAdd(beginDate, endDate);
                return this.OperateResult(true, ZLERP.Resources.Lang.Msg_Operate_Success, null);
            }
            catch (Exception exception)
            {
                return this.OperateResult(false, ZLERP.Resources.Lang.Msg_Operate_Failed + exception.Message, null);
            }
        }


    }
}
