using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    public class ContractOtherPriceController : BaseController<ContractOtherPrice, int>
    {
        public override ActionResult Add(ContractOtherPrice ContractOtherPrice)
        {
            return base.Add(ContractOtherPrice);
        }

        public ActionResult getOtherPriceChoose(string ContractID)
        {
             var ContractOtherPriceList = 
                    this.service.ContractOtherPrice.Query().Where(p => (p.ContractID == ContractID && p.IsAll == false)).ToList();

            foreach (ContractOtherPrice Cop in ContractOtherPriceList)
            {
                Cop.PriceType = string.Format("{0} -- {1}",Cop.PriceType,Cop.CalcType);
            }
            
            return Json(new SelectList(ContractOtherPriceList,  "ID", "PriceType"));
             
        }
    }    
}
