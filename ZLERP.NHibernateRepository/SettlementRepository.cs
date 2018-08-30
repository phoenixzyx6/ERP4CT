using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.Model;
using NHibernate.Criterion;
using NHibernate.Transform;
using NHibernate.Linq;
using NHibernate;
namespace ZLERP.NHibernateRepository
{
    public class SettlementRepository : NHRepositoryBase<Settlement>, ISettlementRepository
    {
        public SettlementRepository(ISession session)
        :base(session)
        { 
        
        }


        #region ISettlementRepository 成员

        public void ExecuteSettlement(string id, string builder)
        {
            string sp = "exec sp_f_execute_settlement @SettlementId=:SettlementId, @Builder=:Builder, @BuildTime=:BuildTime";
            var settlement = this.Get(id);
            if (settlement != null)
            {
                var query = this._session.CreateSQLQuery(sp);
                query.SetString("SettlementId", id);
                query.SetString("Builder", builder);
                query.SetDateTime("BuildTime", DateTime.Now);

                query.ExecuteUpdate();
                settlement.IsClosed = true;
                this.Update(settlement);
                this._session.Flush();


            }
        }

        #endregion
    }
}
