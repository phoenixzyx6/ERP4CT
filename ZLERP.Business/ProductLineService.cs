using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model.Enums;

namespace ZLERP.Business
{
    public class ProductLineService : ServiceBase<ProductLine>
    {
        internal ProductLineService(IUnitOfWork uow) : base(uow) { }


        /// <summary>
        /// type:1表示未分配的筒仓，type:2表示全部筒仓
        /// </summary>
        /// <param name="pid">机组ID</param>
        /// <param name="type">分类</param>
        /// <returns></returns>
        public virtual IList<Silo> FindSilo(string pid,int? type)
        {
            ProductLine p = this.Get(pid);
            IList<SiloProductLine> siloProductLineList = null;
            switch (type)
            {
                case 1:
                    siloProductLineList = this.m_UnitOfWork.GetRepositoryBase<SiloProductLine>().All();
                    break;
                case 2:
                    siloProductLineList = new List<SiloProductLine>();
                    break;
                default:
                    siloProductLineList = p.SiloProductLines;
                    break;
            }
            IList<Silo> allSiloList = this.m_UnitOfWork.GetRepositoryBase<Silo>().All();
            //IList<Silo> queryList = allSiloList.Where(a => !siloProductLineList.Select(s => s.SiloID).Contains(a.ID)
            //    && a.StuffInfo != null && a.StuffInfo.StuffType != null && a.StuffInfo.StuffType.TypeID != StuffTypeEnum.Oil.ToString()).ToList();
            IList<Silo> queryList = allSiloList.Where(a => !siloProductLineList.Select(s => s.SiloID).Contains(a.ID)
               && a.StuffInfo != null && a.StuffInfo.StuffType != null).ToList();
            return queryList;
        }

        public virtual void AssignSilo(string pid, string sid)
        {
            ProductLine productline = this.Get(pid);
            IList<SiloProductLine> list = productline.SiloProductLines;
            if (list.Select(s => s.SiloID).Contains(sid)) {
                throw new Exception("该筒仓已经分配给了" + productline.ProductLineName + " , 一个筒仓在同一条生产线不能分配多次！");
            }
            //获取最大的orderNum
            int maxOrderNum = list.Count == 0 ? 0 : list.Max(m => m.OrderNum);
            SiloProductLine sp = new SiloProductLine();
            sp.SiloID = sid;
            sp.ProductLineID = pid;
            sp.OrderNum = maxOrderNum + 1;
            sp.Rate = 1;
            this.m_UnitOfWork.GetRepositoryBase<SiloProductLine>().Add(sp);
        }




    }
}
