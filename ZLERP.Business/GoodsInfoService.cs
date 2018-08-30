using System;
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
    public class GoodsInfoService : ServiceBase<GoodsInfo>
    {
        internal GoodsInfoService(IUnitOfWork uow)
            : base(uow)
        {

        }

        public void SetM(string goodsid)
        {
            GoodsInfo obj = this.Get(goodsid);
            if (obj == null)
                return;
            if (obj.MaxWarmContent == null)
                obj.MaxWarmContent = 0.00m;
            if (obj.MinWarmContent == null)
                obj.MinWarmContent = 0.00m;

            

            this.Update(obj);
        }
    }
}
