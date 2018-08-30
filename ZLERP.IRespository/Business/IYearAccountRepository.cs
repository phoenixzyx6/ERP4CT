using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;

namespace ZLERP.IRepository
{
    public interface IYearAccountRepository : IRepositoryBase<YearAccount>
    {
        void ExecuteCreate(string path,string name,string s_path);
        void Run(YearAccount entity);
    }
}
