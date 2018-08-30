using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    public class CustMixpropController : BaseController<CustMixprop,string>
    {
        public override ActionResult Index()
        {
            ViewBag.ConStrength = Helpers.HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "ConStrengthCode", true);
            ViewBag.StuffID = Helpers.HelperExtensions.SelectListData<StuffInfo>("StuffName", "ID", "IsUsed=1", "ID", true, null);
            
            return base.Index();
        }
        public override ActionResult Add(CustMixprop cmp)
        {
            return base.Add(cmp);
        }

        public override ActionResult Update(CustMixprop cmp)
        {
            return base.Update(cmp);
        }

        public ActionResult GetItemsAmountById(string id)
        {
            try
            {
                IList<CustMixpropItem> list = m_ServiceBase.Get(id).CustMixpropItems;
                StuffAmount yl = new StuffAmount();
                foreach (CustMixpropItem item in list)
                {
                    switch (item.StuffInfo.StuffTypeID)
                    {
                        case"WA":
                            yl.WAAmount = item.Amount;
                            break;
                        case "CE":
                            yl.CEAmount = item.Amount;
                            break;
                        case "CA":
                            yl.CAAmount = item.Amount;
                            break;
                        case "FA":
                            yl.FAAmount = item.Amount;
                            break;
                        case "AIR1":
                            yl.AIR1Amount = item.Amount;
                            break;
                        case "AIR2":
                            yl.AIR2Amount = item.Amount;
                            break;
                        case "ADM1":
                            yl.ADM1Amount = item.Amount;
                            break;
                        case "ADM2":
                            yl.ADM2Amount = item.Amount;
                            break;
                        case "ADM3":
                            yl.ADM3Amount = item.Amount;
                            break;
                    }

                }
                return OperateResult(true, Lang.Msg_Operate_Success, yl);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return OperateResult(false, ex.Message, null);
            }
        }

        /// <summary>
        /// 导入材料信息，自动生成客户配比
        /// </summary>
        /// <param name="formulaid"></param>
        /// <returns></returns>
        public ActionResult ExportStuff(string formulaid)
        {
            CustMixpropService service = this.service.CustMixprop;
            bool result = service.ExportStuff(formulaid);
            if (result)
            {
                return OperateResult(result, Lang.Msg_Operate_Success, result);
            }
            else
            {
                return OperateResult(result, Lang.Msg_Operate_Failed, result);
            }
        }
    }
}
