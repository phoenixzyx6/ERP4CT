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
    public class GoodsInService : ServiceBase<GoodsIn>
    {
        internal GoodsInService(IUnitOfWork uow)
            : base(uow)
        {
        }
        private PublicService s;

        public GoodsInfo GetGoodsInfo(string id)
        {
            return this.m_UnitOfWork.GetRepositoryBase<GoodsInfo>().Get(id);
        }

        /// <summary>
        /// 增加物资入库记录时修改物资总量、库存、入库数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override GoodsIn Add(GoodsIn entity)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    IRepositoryBase<GoodsInfo> GoodsInfoResp = this.m_UnitOfWork.GetRepositoryBase<GoodsInfo>();

                    GoodsInfo goodsInfo = GoodsInfoResp.Get(entity.GoodsID);
                    goodsInfo.InNum += entity.InNum;
                    goodsInfo.ContentNum += entity.InNum;
                    goodsInfo.TotalNum += entity.InNum;

                    if (goodsInfo.tPrice == null)
                        goodsInfo.tPrice = (entity.InNum * entity.InPrice);
                    else
                        goodsInfo.tPrice += (entity.InNum * entity.InPrice);
                    goodsInfo.uPrice = (decimal)goodsInfo.tPrice / (decimal)goodsInfo.ContentNum;

                    GoodsInfoResp.Update(goodsInfo, null);
                    GoodsIn obj = base.Add(entity);

                    //添加新的历史记录
                    if (s == null)
                        s = new PublicService();
                    GoodsInHistory history = new GoodsInHistory();
                    history.Builder = obj.Builder;
                    history.BuildTime = obj.BuildTime;
                    history.GoodsID = obj.GoodsID;
                    history.GoodsInIDHistory = obj.ID;
                    history.InNum = obj.InNum;
                    history.InPrice = obj.InPrice;
                    history.InTime = obj.InTime;
                    history.Lifecycle = obj.Lifecycle;
                    history.Modifier = obj.Modifier;
                    history.ModifyTime = obj.ModifyTime;
                    history.Operator = obj.Operator;
                    history.Remark = obj.Remark;
                    history.SupplyName = obj.SupplyName;
                    history.TransportName = obj.TransportName;
                    history.action_u = "新增";
                    history.GoodsName = goodsInfo.GoodsName;
                    s.GoodsInHistory.Add(history);

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
        /// 增加物资入库记录时修改物资总量、库存、入库数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public GoodsIn AddByPurchase(GoodsIn entity)
        {
                try
                {
                    IRepositoryBase<GoodsInfo> GoodsInfoResp = this.m_UnitOfWork.GetRepositoryBase<GoodsInfo>();

                    GoodsInfo goodsInfo = GoodsInfoResp.Get(entity.GoodsID);

                    goodsInfo.InNum += entity.InNum;
                    goodsInfo.ContentNum += entity.InNum;
                    goodsInfo.TotalNum += entity.InNum;

                    if (goodsInfo.tPrice == null)
                        goodsInfo.tPrice = (entity.InNum * entity.InPrice);
                    else
                        goodsInfo.tPrice += (entity.InNum * entity.InPrice);
                    goodsInfo.uPrice = (decimal)goodsInfo.tPrice / (decimal)goodsInfo.ContentNum;

                    GoodsInfoResp.Update(goodsInfo, null);
                    GoodsIn obj = base.Add(entity);

                    //添加新的历史记录
                    if (s == null)
                        s = new PublicService();
                    GoodsInHistory history = new GoodsInHistory();
                    history.Builder = obj.Builder;
                    history.BuildTime = obj.BuildTime;
                    history.GoodsID = obj.GoodsID;
                    history.GoodsInIDHistory = obj.ID;
                    history.InNum = obj.InNum;
                    history.InPrice = obj.InPrice;
                    history.InTime = obj.InTime;
                    history.Lifecycle = obj.Lifecycle;
                    history.Modifier = obj.Modifier;
                    history.ModifyTime = obj.ModifyTime;
                    history.Operator = obj.Operator;
                    history.Remark = obj.Remark;
                    history.SupplyName = obj.SupplyName;
                    history.TransportName = obj.TransportName;
                    history.action_u = "新增";
                    history.GoodsName = goodsInfo.GoodsName;
                    s.GoodsInHistory.Add(history);

                    return obj;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    throw;
                
            }
        }

        /// <summary>
        /// 删除物资入库记录时修改物资总量、库存、入库数量
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(GoodsIn entity)
        {
            try
            {
                IRepositoryBase<GoodsInfo> GoodsInfoResp = this.m_UnitOfWork.GetRepositoryBase<GoodsInfo>();
                GoodsInfo goodsInfo = GoodsInfoResp.Get(entity.GoodsID);

                goodsInfo.InNum -= entity.InNum;
                goodsInfo.ContentNum -= entity.InNum;
                goodsInfo.TotalNum -= entity.InNum;

                goodsInfo.tPrice -= (entity.InNum * entity.InPrice);

                goodsInfo.uPrice = (decimal)goodsInfo.ContentNum > 0 ? ((decimal)goodsInfo.tPrice / (decimal)goodsInfo.ContentNum) : 0;

                GoodsInfoResp.Update(goodsInfo, null);
                base.Delete(entity);

                //添加新的历史记录
                if (s == null)
                    s = new PublicService();
                GoodsInHistory history = new GoodsInHistory();
                history.Builder = entity.Builder;
                history.BuildTime = entity.BuildTime;
                history.GoodsID = entity.GoodsID;
                history.GoodsInIDHistory = entity.ID;
                history.InNum = entity.InNum;
                history.InPrice = entity.InPrice;
                history.InTime = entity.InTime;
                history.Lifecycle = entity.Lifecycle;
                history.Modifier = entity.Modifier;
                history.ModifyTime = entity.ModifyTime;
                history.Operator = entity.Operator;
                history.Remark = entity.Remark;
                history.SupplyName = entity.SupplyName;
                history.TransportName = entity.TransportName;
                history.action_u = "删除";
                history.GoodsName = goodsInfo.GoodsName;
                s.GoodsInHistory.Add(history);
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                throw;
            }
        }


        public string GetName(string GoodsID)
        {
            IRepositoryBase<GoodsInfo> GoodsInfoResp = this.m_UnitOfWork.GetRepositoryBase<GoodsInfo>();
            GoodsInfo goodsInfo = GoodsInfoResp.Get(GoodsID);
            return goodsInfo == null ? "" : goodsInfo.GoodsName;
        }

    }
}
