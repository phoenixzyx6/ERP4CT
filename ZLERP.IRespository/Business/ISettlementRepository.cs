using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;

namespace ZLERP.IRepository
{
    public interface ISettlementRepository : IRepositoryBase<Settlement>
    {
        /// <summary>
        /// 结算单结算操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="builder"></param>
        void ExecuteSettlement(string id, string builder);
    }
}
