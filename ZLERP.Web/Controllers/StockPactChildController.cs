using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class StockPactChildController : BaseController<StockPactChild, int>
    {
        public override ActionResult Add(StockPactChild stockPactChild)
        {
            return base.Add(stockPactChild);
        }
        public ActionResult Pay(StockPact sp)
        {
            StockPactChild spc = sp.StockPactChild;
            spc.StockPactID = sp.ID;
            spc.StuffID = sp.StuffID;
            try
            {
                StockPactChild saveEntity = m_ServiceBase.Add(spc);
                return OperateResult(true, Lang.Msg_Operate_Success, saveEntity.GetId());
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                log.Error(ex.Message, ex);
                return HandleExceptionResult(ex);

            }
        }


        public ActionResult PayUpdate(string[] sp)
        {
            try
            {
                StockPactChild spc = m_ServiceBase.Get(Convert.ToInt32(sp[4]));
                spc.ID =Convert.ToInt32(sp[4]);
                spc.PayTime =Convert.ToDateTime(sp[1]);
                spc.PayMoney =Convert.ToDecimal(sp[2]);
                if (!string.IsNullOrEmpty(sp[3]))
                    spc.StuffID = sp[3];
                spc.StockPactID = sp[0];
                m_ServiceBase.Update(spc);
                return OperateResult(true, Lang.Msg_Operate_Success, spc.ID);
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
