using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;
using ZLERP.Web.Controllers.Attributes;

namespace ZLERP.Web.Controllers
{
    public class SettlementController : BaseController<Settlement, string>
    {
        /// <summary>
        /// 查询合同指定时间段内的价格项
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="priceType"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        [HandleAjaxError]
        public JsonResult FindValueItems(string contractId, string beginDate, string endDate, string priceType, decimal? rate)
        {

            var data = this.service.Settlement.FindValueItems(contractId, beginDate, endDate, priceType, rate);
            if (data != null)
            {
                return OperateResult(true, Lang.Msg_Operate_Success, data);
            }


            return OperateResult(false, Lang.Msg_Operate_Failed, null);
        }
        /// <summary>
        /// 保存结算单，主从表一次保存
        /// </summary>
        /// <param name="settlement"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddSettlement(Settlement settlement, IList<SettlementItem> items) {
            if (ModelState.IsValid)
            {
                this.service.Settlement.AddSettlement(settlement, items);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            else
            {
                var m = ModelState.Values.Where(p => p.Errors.Count > 0)
                    .Select(c => string.Join(",", c.Errors.Select(p => p.ErrorMessage).ToList())).ToList();
                return OperateResult(false, string.Join("<br/>", m), null);
            }
        }
        /// <summary>
        /// 修改结算单 
        /// </summary>
        /// <param name="settlement"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateSettlement(Settlement settlement, IList<SettlementItem> items)
        {
            this.service.Settlement.UpdateSettlement(settlement, items);
            return OperateResult(true, Lang.Msg_Operate_Success, null);
        }
        /// <summary>
        /// 查找结算单明细项
        /// </summary>
        /// <param name="settlementId"></param>
        /// <returns></returns>
        public JsonResult FindSettlementItems(string settlementId) {
            
            var settlement = this.service.Settlement.Get(settlementId);
            if (settlement != null)
            {
                var otherPriceList = settlement.Contract.OtherPrice;
                var data = new
                {
                    
                    OtherPrice = settlement.SettlementItems.Where(p => p.PriceType == "other")
                        .Select(p => new SettlementItem
                                   {
                                       ID = p.ID,
                                       ContractItemsID = p.ContractItemsID,
                                       //计算方式
                                       TypeCode = otherPriceList.Where(o=>o.ID.ToString() == p.TypeCode).Select(o=>o.CalcType).FirstOrDefault(),
                                       UnitPrice = p.UnitPrice,
                                       PumpPrice = p.PumpPrice,
                                       //加价项目
                                       PriceType = otherPriceList.Where(o => o.ID.ToString() == p.TypeCode).Select(o => o.PriceType).FirstOrDefault()
                                    
                                   }),
                    ItemPrice = settlement.SettlementItems.Where(p => p.PriceType == "citem")
                        .Select(p => new SettlementItem
                                   {
                                       ID = p.ID,
                                       ContractItemsID = p.ContractItemsID,
                                       //强度
                                       TypeCode = p.ContractItem.ConStrength,
                                       UnitPrice = p.UnitPrice,
                                       PumpPrice = p.PumpPrice,
                                       //特性
                                       PriceType = string.Join(",", p.ContractItem.IdentitySettings.Select(t => t.IdentityName).ToArray()),
                                       SlurryPrice = p.SlurryPrice,
                                       IdentityPrice = p.IdentityPrice
                                   }),
                    PumpPrice = settlement.SettlementItems.Where(p => p.PriceType == "pump")
                        
                };

                return OperateResult(true, Lang.Msg_Operate_Success, data);
            }
            else
                return OperateResult(false, Lang.Msg_Operate_Failed, null);
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost,HandleAjaxError]
        public JsonResult Execute(string id) {
            this.service.Settlement.ExecuteSettlement(id);
            return OperateResult(true, Lang.Msg_Operate_Success, null);
        }

    }
}
