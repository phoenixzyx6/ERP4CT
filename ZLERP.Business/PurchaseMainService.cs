﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using System.Collections.Specialized;
using ZLERP.Model.Enums;

namespace ZLERP.Business
{
    public class PurchaseMainService : ServiceBase<PurchaseMain>
    {
        internal PurchaseMainService(IUnitOfWork uow)
            : base(uow)
        {
        }
    }
}
