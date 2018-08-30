
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Business;
using System.Web.Mvc;

namespace ZLERP.Web.Controllers
{
    public class CheckItemController : BaseController<CheckItem, int>
    {
        public override System.Web.Mvc.ActionResult Add(CheckItem checkitem)
        {
            SiloService siloservice = this.service.Silo;
            Silo silo = siloservice.Get(checkitem.SiloID);
            if (!checkitem.IsAuditor)//无需审核情况直接更改
            {
                silo.Content = checkitem.FactValue;
            }
            siloservice.UpdateSiloContent(silo,checkitem.SystemValue);
            return base.Add(checkitem);
        }

        public override System.Web.Mvc.ActionResult Update(CheckItem checkitem)
        {
            return base.Update(checkitem);
        }

        /// <summary>
        /// 材料盘点审核功能
        /// </summary>
        /// <param name="checkitem">盘点项目</param>
        /// <returns></returns>
        public ActionResult AuditingCheckItem(CheckItem checkitem)
        {
            CheckItem temp = this.service.GetGenericService<CheckItem>().Get(checkitem.ID);
            SiloService siloservice = this.service.Silo;
            Silo silo = siloservice.Get(temp.SiloID);
            if (checkitem.AuditStatus == 1)
            {
                silo.Content = temp.FactValue;
            }
            siloservice.UpdateSiloContent(silo,checkitem.SystemValue);
            return base.Auditing((int)checkitem.ID, checkitem.AuditStatus, checkitem.AuditTime, checkitem.Auditor,"");
        }
    }
}
