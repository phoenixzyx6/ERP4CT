using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Business;
using ZLERP.Resources;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class ReportDatumController : BaseController<ReportDatum, int>
    {
        public ActionResult SaveReport(ReportDatum entity) {
            //查找该资料打印时是否保存过，如果已保存则更新
            ReportDatum rd = this.service.GetGenericService<ReportDatum>().Query().Where(m => m.Stencil == entity.Stencil && m.ReportType == entity.ReportType && m.ObjectID == entity.ObjectID).ToList().FirstOrDefault();
            if (rd != null)
            {
                rd.ReportDataContent = entity.ReportDataContent;
                rd.Modifier = AuthorizationService.CurrentUserID;
                rd.ModifyTime = DateTime.Now;
                return base.Update(rd);
            }
            else
            {
                return base.Add(entity);
            }
        }
    }    
}
