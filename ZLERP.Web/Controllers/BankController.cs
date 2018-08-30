
using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Resources;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class BankController : BaseController<Bank, string>
    {
        public ActionResult FindBank(string CustomerID) {
            List<Bank> banks = new List<Bank>();
            //非担保银行
            Bank bnk = this.service.GetGenericService<Bank>().Query().Where(m => m.CustomerID == CustomerID && m.IsGuarantee == false && m.IsUsed == true).OrderByDescending(m => m.ID).FirstOrDefault();
            if(bnk != null)banks.Add(bnk);
            //担保
            Bank dbnk = this.service.GetGenericService<Bank>().Query().Where(m => m.CustomerID == CustomerID && m.IsGuarantee == true && m.IsUsed == true).OrderByDescending(m => m.ID).FirstOrDefault();
            if (dbnk != null) banks.Add(dbnk);
            if (banks.Count > 0)
            {
                return OperateResult(true, Lang.Msg_Operate_Success, banks);
            }
            else
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, null);
            }

        }
    }    
}
