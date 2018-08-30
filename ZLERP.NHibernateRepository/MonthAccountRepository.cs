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
using System.Diagnostics;
namespace ZLERP.NHibernateRepository
{
    public class MonthAccountRepository : NHRepositoryBase<MonthAccount>, IMonthAccountRepository
    {
        public MonthAccountRepository(ISession session)
            : base(session)
        {

        }

        /// <summary>
        /// 月结操作
        /// </summary>
        /// <param name="month">月结月份</param>
        /// <returns></returns>
        public bool MonthAccountOper(string month)
        {
            string sp = "exec sp_MonthAccount @month=:month";
            var query = this._session.CreateSQLQuery(sp);
            query.SetString("month", month);
            object ret = query.UniqueResult();
            if (ret == null)
                return false;
            return true;

        }
     
    }
}
