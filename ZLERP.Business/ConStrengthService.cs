using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;

namespace ZLERP.Business
{
    public class ConStrengthService : ServiceBase<ConStrength>
    {
        internal ConStrengthService(IUnitOfWork uow) : base(uow) { }
    }
}
