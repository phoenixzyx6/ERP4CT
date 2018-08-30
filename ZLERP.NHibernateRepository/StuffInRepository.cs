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
    public class StuffInRepository : NHRepositoryBase<StuffIn>, IStuffInRepository
    {
        public StuffInRepository(ISession session)
        :base(session)
        { 
        
        }
         
        #region IStuffInRepository 成员

        public void ExecutePrice(string ids, string modifier)
        {
             string sp = "exec sp_stuffin_execute_price @ids=:ids,@Modifier=:Modifier,@ModifyTime=:ModifyTime"; 
                var query = this._session.CreateSQLQuery(sp);
                query.SetString("ids", ids);
                query.SetString("Modifier", modifier);
                query.SetDateTime("ModifyTime", DateTime.Now);
                query.ExecuteUpdate();   
        }

        #endregion

        
    }
}
