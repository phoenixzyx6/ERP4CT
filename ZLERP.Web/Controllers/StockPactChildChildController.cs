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
    public class StockPactChildChildController : BaseController<StockPactChildChild, int>
    {
        public override ActionResult Add(StockPactChildChild stockPactChildChild)
        {
            return base.Add(stockPactChildChild);
        }
        public ActionResult AddFapiao(StockPact sp)
        {
            StockPactChildChild spcc = sp.StockPactChildChild;
            spcc.StockPactID = sp.ID;
            spcc.StuffID = sp.StuffID;
            try
            {
                StockPactChildChild saveEntity = m_ServiceBase.Add(spcc);
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


        public ActionResult UpdateFapiao(string[] sp)
        {
            StockPactChildChild spcc = m_ServiceBase.Get(Convert.ToInt32(sp[1]));
            if(spcc==null)
                return OperateResult(false, "找不到ID为"+sp[1]+"的发票与资源单数据", null);
            spcc.StockPactID = sp[0];
            spcc.PayTime = Convert.ToDateTime(sp[2]);
            spcc.Zyszmd = string.IsNullOrEmpty(sp[3]) ? 0 : Convert.ToInt32(sp[3]);
            spcc.FapiaoMoney = string.IsNullOrEmpty(sp[4]) ? 0.00m : Convert.ToDecimal(sp[4]);
            spcc.FapiaoNumber = sp[5];
            spcc.FapiaoCailiao = string.IsNullOrEmpty(sp[6]) ? 0.00m : Convert.ToDecimal(sp[6]);
            if (!string.IsNullOrEmpty(sp[7]))
            {
                spcc.StuffID = sp[7];
            }

            try
            {
                m_ServiceBase.Update(spcc);
                return OperateResult(true, Lang.Msg_Operate_Success, sp[1]);
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
