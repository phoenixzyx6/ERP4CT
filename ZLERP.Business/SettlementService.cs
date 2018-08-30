using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using System.Collections.Specialized;
using ZLERP.Model.Enums;
using System.Web;
using ZLERP.Resources;　
namespace ZLERP.Business
{
    public  class SettlementService : ServiceBase<Settlement>
    {

        internal SettlementService(IUnitOfWork uow)
            : base(uow) 
        { 
        }
        /// <summary>
        /// 查询是否有重复的结算单
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        bool IsSettlementExists(string contractId, string beginDate, string endDate) {
             
            string condition = string.Format("ContractID='{0}' and (BeginDate between '{1}' and '{2}' or EndDate between '{1}' and '{2}' or (BeginDate < '{1}' and EndDate>'{2}') or (BeginDate > '{1}' and EndDate<'{2}'))", contractId, beginDate, endDate);
 
            var data = this.All(condition, "ID", true);
            return data.Count > 0;
        }
        /// <summary>
        /// 查询合同指定时间段内的价格项
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="priceType"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public dynamic FindValueItems(string contractId, string beginDate, string endDate, string priceType, decimal? rate)
        {
            if (IsSettlementExists(contractId, beginDate, endDate))
            {
                throw new ApplicationException(Lang.Settlement_Error_DateRangeDuplicate);
            }
            DateTime dtBegin = Convert.ToDateTime(beginDate);
            DateTime dtEnd = Convert.ToDateTime(endDate);
            ContractService conSvr = new ContractService(this.m_UnitOfWork);
            ShippingDocumentService shipSvr = new ShippingDocumentService(this.m_UnitOfWork);

            ProduceTaskService pt = new ProduceTaskService(this.m_UnitOfWork);


            var contract = conSvr.Get(contractId);
            if (contract != null) {
                

                //查询指定范围生产过的合同明细id
             /*   var conStrengthList =  shipSvr.Query()
                    .Where(p => p.IsEffective == true)
                    .Where(p => p.ContractID == contractId 
                        && p.ProduceDate >= dtBegin
                        && p.ProduceDate <= dtEnd)
                    .Where(p => p.ProduceTask.ContractItem != null)
                    .Select(p => p.ProduceTask.ContractItem)
                    .Distinct() 
                    .ToList();
              */
                //提升发货单页面效率相应修改，发货单对象没有ProduceTask关联对象，此处采用替代方法
                //modify by: Sky
                //date: 2012-12-10
                       var produceTaskIds = shipSvr.Query()
                           .Where(p => p.IsEffective == true)
                           .Where(p => p.ContractID == contractId 
                               && p.ProduceDate >= dtBegin
                               && p.ProduceDate <= dtEnd)
                           .Select(p=>p.TaskID).ToList();

                      var conStrengthList = pt.Query()
                           .Where(p=>produceTaskIds.Contains(p.ID) && p.ContractItem !=null )
                           .Select(p => p.ContractItem)
                           .Distinct()
                           .ToList();
                            

              //信息价
               if (priceType == ContractValuationType.InformationValue)
               {
                   var informationPrice = this.m_UnitOfWork.GetRepositoryBase<ConPrice>().Query();
                   var query = from ci in conStrengthList
                               join ip in informationPrice
                               on ci.ConStrength equals ip.ConStrengthCode
                               select new SettlementItem
                               {
                                   ContractItemsID = ci.ID ?? 0,
                                   //强度
                                   TypeCode = ci.ConStrength,
                                   UnitPrice = ip.InfoPrice,
                                   PumpPrice = ip.PumpPrice,
                                   //特性
                                   PriceType = "",
                                   SlurryPrice = ip.SlurryPrice,
                                   IdentityPrice = 0
                               };
                   // .Join(conStrengthList,ip=>ip.ConStrengthCode, ci=>ci.ConStrength,  (ip,ci)=>ip.ConStrengthCode==ci.ConStrength)
                   // .Select(p => new { ConStrength = p., ConcretePrice = p.InfoPrice, PumpPrice = p.PumpPrice, SlurryPrice = p.SlurryPrice, IdentityPrice = 0 })
                   //.Where(p => conStrengthList.Select(i => i.ConStrength).Contains(p.ConStrength))
                   //.OrderBy(p => p.ConStrength)
                   //.ToList();

                   return new
                   {
                       OtherPrice = contract.OtherPrice.Select(o => new SettlementItem
                       {
                           //typecode
                           ContractItemsID = o.ID,
                           //加价项目
                           PriceType = o.PriceType,
                           //计算方式
                           TypeCode = o.CalcType,
                           UnitPrice = o.UnitPrice ?? 0
                       }),
                       PumpPrice = contract.ContractPumps.Select(o => new SettlementItem
                       {
                           ContractItemsID = 0,
                           //泵车类型
                           PriceType = o.PumpType,
                           //泵车类型
                           TypeCode = o.PumpType,
                           UnitPrice = o.PumpPrice ?? 0
                       }),
                       ItemPrice = query.ToList()
                   };
               }
               else
               {
                   return new
                    {
                        OtherPrice = contract.OtherPrice.Select(o => new SettlementItem
                        {
                            //typecode
                            ContractItemsID = o.ID,
                            //加价项目
                            PriceType = o.PriceType,
                            //计算方式
                            TypeCode = o.CalcType,
                            UnitPrice = o.UnitPrice ?? 0
                        }),
                        PumpPrice = contract.ContractPumps.Select(o => new SettlementItem
                        {
                            ContractItemsID = 0,
                            //泵车类型
                            PriceType = o.PumpType,
                            //泵车类型
                            TypeCode = o.PumpType,
                            UnitPrice = o.PumpPrice??0
                        }),
                        ItemPrice = contract.ContractItems
                            .Where(p => conStrengthList.Select(i => i.ConStrength).Contains(p.ConStrength))
                            .Select(p => new SettlementItem
                            {
                                ContractItemsID = p.ID ?? 0,
                                //强度
                                TypeCode = p.ConStrength,
                                UnitPrice = p.UnPumpPrice ?? 0,
                                PumpPrice = p.PumpCost ?? 0,
                                //特性
                                PriceType = string.Join(",", p.IdentitySettings.Select(t => t.IdentityName).ToArray()),
                                SlurryPrice = p.SlurryPrice ?? 0,
                                IdentityPrice = p.IdentitySettings.Sum(i => i.IdentityPrice)
                            })
                            .ToList()
                    };
               }

               
            }
            return null;
        }

        public void AddSettlement(Settlement settlement, IList<SettlementItem> items)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {

                    //var itemRepo = this.m_UnitOfWork.GetRepositoryBase<SettlementItem>();
                    IRepositoryBase<SettlementItem> SettlementItem = this.m_UnitOfWork.GetRepositoryBase<SettlementItem>();
                    //settlement.SettlementItems = items;
                    Settlement tmp = this.Add(settlement);
                    foreach (SettlementItem item in items)
                    {
                        item.SettlementId = tmp.ID;
                        SettlementItem.Add(item);
                        this.m_UnitOfWork.Flush();
                    }
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        public void UpdateSettlement(Settlement settlement, IList<SettlementItem> items)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                
                try
                {
                     
                    this.Update(settlement, HttpContext.Current.Request.Form);

                    if (settlement.SettlementItems == null || settlement.SettlementItems.Count == 0) {
                        settlement = this.Get(settlement.ID);
                    }
                    SettlementItem submitedItem;
                    //子项只更新价格字段
                    foreach (var item in settlement.SettlementItems)
                    {
                        submitedItem =  items.Where(i => i.ID == item.ID).FirstOrDefault();
                        if (submitedItem != null)
                        {
                            item.UnitPrice = submitedItem.UnitPrice;
                            item.PumpPrice = submitedItem.PumpPrice ?? 0;
                            item.SlurryPrice = submitedItem.SlurryPrice ?? 0;
                            item.IdentityPrice = submitedItem.IdentityPrice ?? 0;
                             
                        }
                    }
                    this.Update(settlement, null);
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="id"></param>
        public void ExecuteSettlement(string id)
        {
            this.m_UnitOfWork.SettlementRepository.ExecuteSettlement(id, AuthorizationService.CurrentUserID);
        }
    }
}

