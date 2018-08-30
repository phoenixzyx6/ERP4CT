using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;

namespace ZLERP.Business
{
    public class MonthAccountService : ServiceBase<MonthAccount>
    {
        internal MonthAccountService(IUnitOfWork uow): base(uow)
        {

        }
        /// <summary>
        /// 月结
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool MonthAccountOper(string month)
        {
            bool issuccess = this.m_UnitOfWork.MonthAccountRepository.MonthAccountOper(month);
            return issuccess;
        }
    }
}
