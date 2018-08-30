using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.ViewModels;
using NHibernate;
using ZLERP.IRepository;
using ZLERP.Model;

namespace ZLERP.NHibernateRepository
{
    /// <summary>
    /// 入库记录单价调整
    /// </summary>
    public class StuffInPriceAdjustRepository : NHRepositoryBase<StuffInPriceAdjust>, IStuffInPriceAdjustRepository
    {
        public StuffInPriceAdjustRepository(ISession session)
            : base(session)
        {

        }

        /// <summary>
        /// 入库记录单价调整
        /// </summary>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public bool StuffInPriceAdjustOper(string beginDate, string endDate)
        {
            string sp = "exec sp_StuffInPriceAdjust  @beginDate=:beginDate,@endDate=:endDate";
            var query = this._session.CreateSQLQuery(sp);
            query.SetString("beginDate", beginDate);
            query.SetString("endDate", endDate);
            object ret = query.UniqueResult();
            if (ret == null)
                return false;
            return true;

        }

    }
}
