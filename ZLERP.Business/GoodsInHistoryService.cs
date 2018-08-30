using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using System.Collections.Specialized;
using ZLERP.Model.Enums;
using System.Web;
using ZLERP.Resources;　

namespace ZLERP.Business
{
    public class GoodsInHistoryService : ServiceBase<GoodsInHistory>
    {
        internal GoodsInHistoryService(IUnitOfWork uow)
            : base(uow)
        {
        }

    }
}
