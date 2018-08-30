using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
namespace ZLERP.IRepository
{
    public interface IMonthAccountRepository : IRepositoryBase<MonthAccount>
    { 
        /// <summary>
        /// 月结操作
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        bool MonthAccountOper(string month);
    }
}
