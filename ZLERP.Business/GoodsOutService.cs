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
    public class GoodsOutService : ServiceBase<GoodsOut>
    {
        internal GoodsOutService(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// 增加物资领用记录时修改物资总量、库存、领用数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override GoodsOut Add(GoodsOut entity)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    IRepositoryBase<GoodsInfo> GoodsInfoResp = this.m_UnitOfWork.GetRepositoryBase<GoodsInfo>();
                    GoodsInfo goodsInfo = GoodsInfoResp.Get(entity.GoodsID);

                    goodsInfo.OutNum += entity.OutNum;
                    goodsInfo.ContentNum -= entity.OutNum;
                    goodsInfo.TotalNum -= entity.OutNum;
                    goodsInfo.tPrice = goodsInfo.uPrice * goodsInfo.ContentNum;

                    GoodsInfoResp.Update(goodsInfo, null);
                    GoodsOut obj = base.Add(entity);
                    tx.Commit();
                    return obj;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    throw;
                }
            }
        }

        /// <summary>
        /// 删除物资领用记录时修改物资总量、库存、领用数量
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(GoodsOut entity)
        {
            
                try
                {
                    IRepositoryBase<GoodsInfo> GoodsInfoResp = this.m_UnitOfWork.GetRepositoryBase<GoodsInfo>();
                    GoodsInfo goodsInfo = GoodsInfoResp.Get(entity.GoodsID);

                    goodsInfo.OutNum -= entity.OutNum;
                    goodsInfo.ContentNum += entity.OutNum;
                    goodsInfo.tPrice = goodsInfo.uPrice * goodsInfo.ContentNum;
                    goodsInfo.TotalNum += entity.OutNum;

                    GoodsInfoResp.Update(goodsInfo, null);

                    base.Delete(entity);

                  
                }
                catch (Exception ex)
                {
                   
                    logger.Error(ex.Message);
                    throw;
                }
            
        }

        PublicService ps;
        public decimal getNumPrice(string id, string price)
        {
            if (ps == null)
                ps = new PublicService();
            //入库
            decimal sumIn = ps.GoodsIn.Query().Where(m => m.GoodsID == id && m.InPrice == Convert.ToDecimal(price)).Sum(p=>p.InNum);

            //出库
            //decimal sumOut = this.Query().Where(m => m.GoodsID == id && m.price == Convert.ToDecimal(price)).Sum(p => p.OutNum);
            List<GoodsOut> list = this.Query().Where(m => m.GoodsID == id && m.price == Convert.ToDecimal(price)).ToList();
            decimal sumOut = 0.00m;
            foreach (GoodsOut g in list)
            {
                sumOut += g.OutNum;
            }

            return sumIn - sumOut;
        }
    }
}
