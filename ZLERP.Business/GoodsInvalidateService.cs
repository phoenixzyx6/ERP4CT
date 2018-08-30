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
    public class GoodsInvalidateService : ServiceBase<GoodsInvalidate>
    {
        internal GoodsInvalidateService(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// 增加物资报废记录时修改物资总量、库存、报废数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override GoodsInvalidate Add(GoodsInvalidate entity)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    IRepositoryBase<GoodsInfo> GoodsInfoResp = this.m_UnitOfWork.GetRepositoryBase<GoodsInfo>();
                    GoodsInfo goodsInfo = GoodsInfoResp.Get(entity.GoodsID);

                    goodsInfo.InvalidateNum += entity.InvalidateNum;
                    goodsInfo.ContentNum -= entity.InvalidateNum;
                    goodsInfo.TotalNum -= entity.InvalidateNum;
                    goodsInfo.tPrice = goodsInfo.uPrice * goodsInfo.ContentNum;

                    GoodsInfoResp.Update(goodsInfo, null);
                    GoodsInvalidate obj = base.Add(entity);
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
        /// 删除物资报废记录时修改物资总量、库存、报废数量
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(GoodsInvalidate entity)
        {
           
                try
                {
                    IRepositoryBase<GoodsInfo> GoodsInfoResp = this.m_UnitOfWork.GetRepositoryBase<GoodsInfo>();
                    GoodsInfo goodsInfo = GoodsInfoResp.Get(entity.GoodsID);

                    goodsInfo.InvalidateNum -= entity.InvalidateNum;
                    goodsInfo.ContentNum += entity.InvalidateNum;
                    goodsInfo.TotalNum+= entity.InvalidateNum;
                    goodsInfo.tPrice = goodsInfo.uPrice * goodsInfo.ContentNum;

                    GoodsInfoResp.Update(goodsInfo, null);

                    base.Delete(entity);

                  
                }
                catch (Exception ex)
                {
                 
                    logger.Error(ex.Message);
                    throw;
                }
            
        }
    }
}
