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
    public class ConsMixpropItemRepository : NHRepositoryBase<ConsMixpropItem>, IConsMixpropItemRepository
    {
        public ConsMixpropItemRepository(ISession session)
            : base(session)
        {

        }

        /// <summary>
        /// 更换桶仓
        /// </summary>
        /// <param name="S_SiloID">源桶仓编号</param>
        /// <param name="D_SiloID">目标桶仓编号</param>
        /// <param name="ids">施工配比编号</param>
        /// <returns></returns>
        public bool ChangeSilo(string S_SiloID, string D_SiloID, string[] ids)
        {
            foreach (string id in ids)
            {
                string sp = "exec sp_ChangeSilo @s_siloid=:s_siloid,@d_siloid=:d_siloid,@cid=:cid";
                var query = this._session.CreateSQLQuery(sp);
                query.SetString("s_siloid", S_SiloID);
                query.SetString("d_siloid", D_SiloID);
                query.SetString("cid", id);
                object ret = query.UniqueResult();
                if (ret == null)
                    return false;

            }
            return true;

        }
    }
}
