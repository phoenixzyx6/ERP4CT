using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;

namespace ZLERP.IRepository
{
    public interface IConsMixpropItemRepository : IRepositoryBase<ConsMixpropItem>
    {
        //更换桶仓
        bool ChangeSilo(string S_SiloID, string D_SiloID, string[] ids);
    }
}
