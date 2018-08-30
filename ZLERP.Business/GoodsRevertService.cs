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
    public class GoodsRevertService : ServiceBase<GoodsRevert>
    {
        internal GoodsRevertService(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// 增加物资归还记录时修改物资库存、归还数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override GoodsRevert Add(GoodsRevert entity)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    IRepositoryBase<GoodsInfo> GoodsInfoResp = this.m_UnitOfWork.GetRepositoryBase<GoodsInfo>();
                    GoodsInfo goodsInfo = GoodsInfoResp.Get(entity.GoodsID);

                    goodsInfo.RevertNum += entity.RevertNum;
                    goodsInfo.ContentNum += entity.RevertNum;

                    GoodsInfoResp.Update(goodsInfo, null);
                    GoodsRevert obj = base.Add(entity);
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
        /// 删除物资归还记录时修改物资库存、借用、归还数量
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(GoodsRevert entity)
        {
            try
            {
                IRepositoryBase<GoodsInfo> GoodsInfoResp = this.m_UnitOfWork.GetRepositoryBase<GoodsInfo>();
                GoodsInfo goodsInfo = GoodsInfoResp.Get(entity.GoodsID);

                goodsInfo.RevertNum -= entity.RevertNum;
                goodsInfo.ContentNum -= entity.RevertNum;

                GoodsInfoResp.Update(goodsInfo, null);

                base.Delete(entity);
            }catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }

        }
    }
}
