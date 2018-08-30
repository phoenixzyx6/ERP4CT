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
    public class GoodsCheckService : ServiceBase<GoodsCheck>
    {
        internal GoodsCheckService(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// 增加物资盘点记录时修改物资总量、库存数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override GoodsCheck Add(GoodsCheck entity)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    IRepositoryBase<GoodsInfo> GoodsInfoResp = this.m_UnitOfWork.GetRepositoryBase<GoodsInfo>();
                    GoodsInfo goodsInfo = GoodsInfoResp.Get(entity.GoodsID);

                    goodsInfo.TotalNum = goodsInfo.TotalNum - (goodsInfo.ContentNum - entity.CheckNum);
                    goodsInfo.ContentNum = entity.CheckNum;

                    goodsInfo.tPrice = goodsInfo.uPrice * goodsInfo.ContentNum;
                    
                    GoodsInfoResp.Update(goodsInfo, null);
                    GoodsCheck obj = base.Add(entity);
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
    }
}
