using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.NHibernateRepository;

namespace ZLERP.Web.Controllers
{
    public class ContractPumpController : BaseController<ContractPump,int>
    {
        public ActionResult ImportEntitys(string contractID, string[] ids) {

            try
            {
                this.service.ContractPumpService.ImportPumpType(contractID, ids);
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e) {
                if (e.InnerException != null)
                    e = e.InnerException;
                log.Error(e.Message, e);
                return OperateResult(false, Lang.Msg_Operate_Failed + ":" + (e.Message.Contains("UNIQUE KEY") == true ? Lang.IsExistRecord : e.Message), null);
            }
            
        }

        public ActionResult PumpTypes() { 
            Dictionary<string,string> pumptypes = new Dictionary<string,string>();
            pumptypes.Add("PT1", "固定泵");
            pumptypes.Add("PT2", "移动泵");
            return PartialView("select", pumptypes);
        }

    }
}
