using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using System.Collections.Specialized;
using ZLERP.Resources;
namespace ZLERP.Business
{
    public class ThreadIDService : ServiceBase<ThreadID>
    {
        internal ThreadIDService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public override ThreadID Add(ThreadID entity)
        {
            return base.Add(entity);
        }
    }
}

