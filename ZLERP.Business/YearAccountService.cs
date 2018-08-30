using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using System.Configuration;
using System.IO;

namespace ZLERP.Business
{
    public class YearAccountService : ServiceBase<YearAccount>
    {
        internal YearAccountService(IUnitOfWork uow)
            : base(uow)
        {
        }


        public void BuildAccount(string path,string name,string s_path)
        {
            this.m_UnitOfWork.YearAccountRepository.ExecuteCreate(path,name,s_path);    

        }

        public void Run(YearAccount entity)
        {
            this.m_UnitOfWork.YearAccountRepository.Run(entity); 
        }
    }
}

