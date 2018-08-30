using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;

namespace ZLERP.Business
{
    public class FormulaItemService : ServiceBase<FormulaItem>
    {
        internal FormulaItemService(IUnitOfWork uow)
            : base(uow) 
        { 
        }

        public bool IsEnableEdit(int id, decimal amount)
        {
            
            FormulaItem item = this.m_UnitOfWork.GetRepositoryBase<FormulaItem>().Get(id);
            decimal s_amount = (decimal)item.StandardAmount;
            if (s_amount > 0)
            {
                if ((Math.Abs(amount - s_amount) / s_amount) > (decimal)0.05)
                    throw new Exception("修改量超过了标准量的范围");
                else
                {
                    //符合情况的更新理论配比
                    item.StuffAmount = amount;
                    this.m_UnitOfWork.GetRepositoryBase<FormulaItem>().Update(item, null);
                    this.m_UnitOfWork.Flush();
                    return true;
                }
            }
            else
            {
                throw new Exception("该参考配比不使用该原料");
            }
        }

        public override FormulaItem Add(FormulaItem entity)
        {
            List<FormulaItem> list=this.m_UnitOfWork.GetRepositoryBase<FormulaItem>()
                .Query().Where(m=>m.FormulaID==entity.FormulaID && m.StuffTypeID==entity.StuffTypeID).ToList();
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
