using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;

namespace ZLERP.Business
{
    public class ContractOtherPriceService : ServiceBase<ContractOtherPrice>
    {
        internal ContractOtherPriceService(IUnitOfWork uow) :base(uow){ 
            
        }
    }
}
