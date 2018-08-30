using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Resources;

namespace ZLERP.Business
{
    public class ContractService : ServiceBase<Contract>
    {
        internal ContractService(IUnitOfWork uow) : base(uow) { }

        public override Contract Add(Contract entity)
        {
            base.CheckIsAutoAudit(entity);
            //此处针对之前未在数据库作合同名唯一索引的版本做出限制
            int ret = this.Query()
                .Where(p => p.CustomerID == entity.CustomerID && p.ContractName == entity.ContractName)
                .Count();
            if (ret > 0)
            {
                throw new ApplicationException(Lang.Contract_ContractName_Exists);
            }
            return base.Add(entity);

        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="consMixprop"></param>
        public void Audit(Contract Contract)
        {
            try
            {
                Contract contract = this.Get(Contract.ID);
                string auditor = AuthorizationService.CurrentUserID;
                contract.AuditStatus = Contract.AuditStatus;
                contract.AuditInfo = Contract.AuditInfo;
                contract.Auditor = auditor;
                contract.AuditTime = DateTime.Now;
                base.Update(contract, null);
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }
        }
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="auditStatus"></param>
        public void UnAudit(string contractID, int auditStatus)
        {
            try
            {
                Contract contract = this.Get(contractID);
                contract.AuditStatus = 0;
                contract.Auditor = AuthorizationService.CurrentUserID;
                contract.AuditTime = DateTime.Now;
                this.m_UnitOfWork.GetRepositoryBase<Contract>().Update(contract, null);
                this.m_UnitOfWork.Flush();
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }
        }

        /// <summary>
        /// 快速解除限制
        /// </summary>
        /// <param name="contractID">合同编号</param>
        /// <param name="contractStatus">合同状态：强制更改为进行中</param>
        /// <param name="contractLimitType">合同限制条件：强制更改为不受限制</param>
        public void QuickUnfreeze(string contractID, string contractStatus, string contractLimitType)
        {
            try
            {
                Contract contract = this.Get(contractID);
                contract.ContractStatus = contractStatus;
                contract.ContractLimitType = contractLimitType;
                contract.IsLimited = false;
                this.m_UnitOfWork.GetRepositoryBase<Contract>().Update(contract, null);
                this.m_UnitOfWork.Flush();
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }

        }
        /// <summary>
        /// 快速锁定合同
        /// </summary>
        /// <param name="contractID"></param>
        public void QuickLock(string contractID, string remark)
        {
            try
            {
                Contract contract = this.Get(contractID);
                contract.Remark = remark;
                contract.IsLimited = true;
                this.m_UnitOfWork.GetRepositoryBase<Contract>().Update(contract, null);
                this.m_UnitOfWork.Flush();
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }
        }

        public bool SetComplete(string contractID)
        {
            Contract contract = this.Get(contractID);
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {

                try
                {
                    contract.ContractStatus = "3";
                    this.Update(contract, null);
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    return false;
                    throw;
                }
            }
        }

        public bool SetUnComplete(string contractID)
        {
            Contract contract = this.Get(contractID);
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {

                try
                {
                    contract.ContractStatus = "2";
                    this.Update(contract, null);
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    return false;
                    throw;
                }
            }
        }


        /// <summary>
        /// 获取特性价格
        /// </summary>
        /// <returns></returns>
        public dynamic getIdentityPrice(string identityName, string identityType)
        {

            Identity identity = this.m_UnitOfWork.GetRepositoryBase<Identity>().Query().Where(p => (p.IdentityName == identityName && p.IdentityType == identityType)).FirstOrDefault();

            dynamic identityPriceInfo = new
               {
                   Result = true
                   ,
                   IdentityPrice = identity.IdentityPrice
               };
            return identityPriceInfo;
        }

        /// <summary>
        /// 运输单结算
        /// </summary>
        /// <param name="sd"></param>
        public void JS(ShippingDocument sd)
        {
            sd.IsJS = true;
            decimal price = 0;
            ContractItem ci = this.m_UnitOfWork.GetRepositoryBase<ContractItem>().Get(this.m_UnitOfWork.GetRepositoryBase<ProduceTask>().Get(sd.TaskID).ContractItemsID);
            List<PriceSetting> list = this.m_UnitOfWork.GetRepositoryBase<PriceSetting>().All().Where(p => p.ContractItemsID == ci.ID).OrderBy(p => p.ChangeTime).ToList();
            if (list.Count > 0) 
            {
                int i = 0;
                while (list[i].ChangeTime<sd.BuildTime)
                {
                    i++;
                }
                if (i == 0) {
                    price=(ci.UnPumpPrice==null?0:(Decimal)ci.UnPumpPrice)+(ci.PumpPrice==null?0:(Decimal)ci.PumpPrice);
                }
                else {
                    price = (list[i - 1].UnPumpPrice == null ? 0 : (Decimal)list[i - 1].UnPumpPrice) + (ci.PumpPrice == null ? 0 : (Decimal)ci.PumpPrice);
                }

            }
            else
            {
                price=(ci.UnPumpPrice==null?0:(Decimal)ci.UnPumpPrice)+(ci.PumpPrice==null?0:(Decimal)ci.PumpPrice);
            }
            sd.JSPrice = sd.SignInCube * price;
            this.m_UnitOfWork.ShippingDocumentRepository.Update(sd, null);
            this.m_UnitOfWork.Flush();
            
        }

        public bool Import(string contractID, string[] conStrength)
        {
            if (conStrength == null)
            {
                throw new Exception(":没有合同明细需要导入");
            }
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    IRepositoryBase<ContractItem> itemResp = this.m_UnitOfWork.GetRepositoryBase<ContractItem>();
                    foreach (string item in conStrength)
                    {
                        ContractItem temp = new ContractItem();
                        temp.ContractID = contractID;
                        temp.ConStrength = item;
                        itemResp.Add(temp);
                    }
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}
