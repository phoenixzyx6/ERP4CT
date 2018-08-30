using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
namespace ZLERP.Business
{

    public class StuffInfoService : ServiceBase<StuffInfo>
    {
        internal StuffInfoService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public void Inventory(StuffInfo entity) {
            base.Update(entity,null);
        }
        /// <summary>
        /// 是否骨料
        /// </summary>
        /// <param name="stuffid"></param>
        /// <returns></returns>
        public bool IsGuLiao(string stuffid) {
            StuffInfo stuffinfo = this.Find("StuffID = '" + stuffid + "' AND (StuffTypeId like 'SHI%' OR StuffTypeId like 'SHA%' OR StuffTypeId like 'CA%' OR StuffTypeId like 'FA%')", 1, 1, "StuffID", "DESC").FirstOrDefault();
            if (stuffinfo != null)
            {
                return true;
            }
            else {
                return false;
            }
        }
    }
}

