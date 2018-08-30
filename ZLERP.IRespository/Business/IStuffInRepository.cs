using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;

namespace ZLERP.IRepository
{
    public interface IStuffInRepository : IRepositoryBase<StuffIn>
    {
        /// <summary>
        /// 进货单结算操作
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="modifier"></param>
        void ExecutePrice(string ids, string modifierl);
    }
}
