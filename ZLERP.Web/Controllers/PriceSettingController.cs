using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class PriceSettingController : BaseController<PriceSetting, int>
    {

        public override ActionResult Add(PriceSetting PriceSetting)
        {
            ContractItem ci = this.service.GetGenericService<ContractItem>().Get(PriceSetting.ContractItemsID);
            string constring = ci.ConStrength;
            string cid = ci.ContractID;
            List<ContractItem> ls = this.service.GetGenericService<ContractItem>().All().Where(p => p.ContractID == cid && p.ConStrength.Length>3).ToList();
            if (constring.Length == 3) {
                ls = ls.Where(p =>p.ConStrength.Substring(0, 3) == constring).ToList();
                foreach(var a in ls){
                    PriceSetting ps = new PriceSetting();
                    ps.ContractItemsID = a.ID;
                    ps.UnPumpPrice=a.UnPumpPrice-ci.UnPumpPrice+PriceSetting.UnPumpPrice;
                    ps.FatherID = constring;
                    ps.ChangeTime = PriceSetting.ChangeTime;
                    base.Add(ps);
                }
            }
            return base.Add(PriceSetting);
        }

    }
}
