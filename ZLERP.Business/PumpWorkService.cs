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
    public class PumpWorkService : ServiceBase<PumpWork>
    {
        internal PumpWorkService(IUnitOfWork uow) : base(uow) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void CompareShipAndPump(PumpWork entity) {

            ShippingDocumentService sdserv = new ShippingDocumentService(this.m_UnitOfWork);

            /*
             * 先找出发货单中该任务单与输入泵送日期相符的出票方量总计值，如果当天无发货，则提示错误信息
             * 若有，则与泵送作业表中该任务单已泵混凝土与砂浆总计值比较，如果超出则提示超出数值
             */
            //发货单上的数量
            decimal? shippSum = this.getShipSum(entity.TaskID, entity.PumpDate);

            //已有的混凝土泵送数量
            decimal? pumpBSum = this.Query()
                .Where(p => p.TaskID == entity.TaskID)
                .Where(p => p.PumpDate == Convert.ToDateTime(entity.PumpDate))
                .Where(p => p.ID != entity.ID)
                .Select(p => p.CustNum)
                .Cast<decimal?>()
                .Sum();

            //已有的砂浆泵送数量
            decimal? pumpSSum = this.Query()
                .Where(p => p.TaskID == entity.TaskID)
                .Where(p => p.PumpDate == Convert.ToDateTime(entity.PumpDate))
                .Where(p => p.ID != entity.ID)
                .Select(p => p.SlurryCustNum)
                .Cast<decimal?>()
                .Sum();

            //未泵数量
            shippSum = shippSum == null ? 0 : shippSum;
            pumpBSum = pumpBSum == null ? 0 : pumpBSum;
            pumpSSum = pumpSSum == null ? 0 : pumpSSum;

            decimal? notPunp = shippSum - (pumpBSum + pumpSSum);
            if (entity.CustNum + entity.SlurryCustNum > notPunp) { //超出了数量
                throw new Exception("累计泵送数量超过发货数量，发货数量：" + shippSum + "，已泵数量：" + (pumpBSum + pumpSSum) + "");
            }
            
            
        }

        /// <summary>
        /// 发货单上的数量
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="pumpDate"></param>
        /// <returns></returns>
        public decimal? getShipSum(string taskId, DateTime? pumpDate) {
            ShippingDocumentService sdserv = new ShippingDocumentService(this.m_UnitOfWork);
            return  sdserv.Query()
                .Where(s => s.TaskID == taskId&&s.IsEffective==true)
                .Where(s => s.ProduceDate > Convert.ToDateTime(pumpDate)
                    && s.ProduceDate < Convert.ToDateTime(pumpDate).AddDays(1))
                .Select(s=>s.ParCube)
                .Cast<decimal?>()
                .Sum();
        }

        public dynamic getPumpTypeAndPumpCost(string taskId, DateTime? pumpDate){
            ProduceTask task = this.m_UnitOfWork.GetRepositoryBase<ProduceTask>().Get(taskId);
            //合同编号
            string contractId = task.ContractID;
            //合同明细编号
            int contractItemId = task.ContractItemsID;
            ContractItem contractItem = task.ContractItem;
            //泵车类型
            string pumpType = task.PumpType;

            //发货单数量
            decimal? shipSum = this.getShipSum(taskId, pumpDate);
            return new { 
                Result = true,
                PumpType = pumpType,
                PumpCost = contractItem ==null ? 0 : contractItem.PumpCost,
                ShipSum = shipSum == null ? 0 : shipSum
            };
        }
    }
}
