using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class GPS_IconLayerController : BaseController<GPS_IconLayer, int>
    {
        public override System.Web.Mvc.ActionResult Add(GPS_IconLayer IconLayer)
        {
            return base.Add(IconLayer);
        }


        public override System.Web.Mvc.ActionResult Update(GPS_IconLayer IconLayer)
        {
            return base.Update(IconLayer);
        }

        public ActionResult IconLayerUseStatus(string[] ids, bool status)
        {
            try
            {

                foreach (string id in ids)
                {
                    GPS_IconLayer IconLayer = m_ServiceBase.Get(int.Parse(id));
                    IconLayer.IsUsed = status;
                    base.Update(IconLayer);
                }

                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ex.Message, null);
            }
        }
 
    }
}
