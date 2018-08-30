using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;

namespace ZLERP.Business
{
    public class CustMixpropItemService : ServiceBase<CustMixpropItem>
    {
        internal CustMixpropItemService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public override CustMixpropItem Add(CustMixpropItem entity)
        {
            List<CustMixpropItem> list = this.m_UnitOfWork.GetRepositoryBase<CustMixpropItem>()
                .Query().Where(m => m.CustMixpropID == entity.CustMixpropID && m.StuffID == entity.StuffID).ToList();
            if (list.Count > 0)
            {
                throw new Exception("该配比库已存在该材料的用量信息");
            }
            else
            {
                return base.Add(entity);
            }
        }
    }
}
