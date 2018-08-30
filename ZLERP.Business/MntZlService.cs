using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;

namespace ZLERP.Business
{
    public class MntZlService:ServiceBase<MntZl>
    {
        internal MntZlService(IUnitOfWork uow) : base(uow) { }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="consMixprop"></param>
        public void Audit(MntZl MntZl)
        {
            try
            {
                MntZl mntzl = this.Get(MntZl.ID);
                string auditor = AuthorizationService.CurrentUserID;
                mntzl.AuditStatus = MntZl.AuditStatus;
                mntzl.AuditInfo = MntZl.AuditInfo;
                mntzl.Auditor = auditor;
                mntzl.AuditTime = DateTime.Now;
                base.Update(mntzl, null);
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }
        }


        /// <summary>
        /// 经理审核
        /// </summary>
        /// <param name="MntZl"></param>
        public void ManageAudit(MntZl MntZl)
        {
            try
            {
                MntZl mntzl = this.Get(MntZl.ID);
                string auditor = AuthorizationService.CurrentUserID;
                mntzl.ReAuditStatus = MntZl.ReAuditStatus;
                mntzl.ReAuditInfo = MntZl.ReAuditInfo;
                mntzl.ReAuditor = auditor;
                mntzl.ReAuditTime = DateTime.Now;
                base.Update(mntzl, null);
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }
        }
        /// <summary>
        /// 批次审核
        /// </summary>
        /// <param name="ids"></param>
        public void BatchAudit(string[] ids) {
            using (var tx = this.m_UnitOfWork.BeginTransaction()) {
                try
                {
                    foreach (var id in ids) {
                        MntZl mntzl = this.Get(id);
                        if (mntzl != null) {
                            mntzl.AuditStatus = 1;//设置为审核通过，若要根据系统设置来决定审核状态，可修改此处的值
                            mntzl.Auditor = AuthorizationService.CurrentUserID;
                            mntzl.AuditTime = DateTime.Now;
                            base.Update(mntzl, null);
                        }
                    }
                    tx.Commit();
                }
                catch (Exception e) {
                    tx.Rollback();
                    logger.Error(e.Message, e);
                    throw e;
                }
            }
        }
    }
}
