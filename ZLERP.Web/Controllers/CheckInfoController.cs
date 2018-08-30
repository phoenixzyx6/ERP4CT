
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Business;
using ZLERP.Web.Helpers;
using System.Web.Mvc;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class CheckInfoController : BaseController<CheckInfo, string>
    {
        /// <summary>
        /// 增加盘点记录
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        public override System.Web.Mvc.ActionResult Add(CheckInfo check)
        {
            check.Checker = AuthorizationService.CurrentUserID;
            return base.Add(check);
        }

        public override System.Web.Mvc.ActionResult Index()
        {
         //   ViewBag.SiloInfo = HelperExtensions.SelectListData<ZLERP.Model.Silo>("SiloName", "ID", "","ID", true,"");
            ViewBag.ProductLineInfo = HelperExtensions.SelectListData<ZLERP.Model.ProductLine>("ProductLineName","ID","IsUsed = 1 ","ID",true,"");
            return base.Index();
        }

        public override System.Web.Mvc.ActionResult Update(CheckInfo check)
        {
            return base.Update(check);
        }

        /// <summary>
        /// 获得超过时间提示参数
        /// </summary>
        /// <param name="checkitem"></param>
        /// <returns></returns>
        public ActionResult ReadTimeLimit(string checkinfoID)
        {
            CheckInfo ci = this.service.GetGenericService<CheckInfo>().Get(checkinfoID);
            if (ci != null)
            {
                var config = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.CheckItemLimitTime);
                int configValue = 0;
                if (config != null && !string.IsNullOrEmpty(config.ConfigValue))
                    configValue = Convert.ToInt32(config.ConfigValue);
                TimeSpan ts = DateTime.Now - ci.BuildTime;
                return OperateResult(true, null, (ts.Days > configValue));
            }
            else
            {
                return OperateResult(false, "出现异常", null);
            }
        }
    }    
}
