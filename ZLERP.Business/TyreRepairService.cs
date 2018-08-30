using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using ZLERP.Model.Enums;
namespace ZLERP.Business
{
    public class TyreRepairService : ServiceBase<TyreRepair>
    {
        internal TyreRepairService(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// 增加轮胎更换
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override TyreRepair Add(TyreRepair entity)
        {
            
            using ( var  tx = this.m_UnitOfWork.BeginTransaction() )
            {
                try
                {
                    string tyreId = entity.TyreID;
                    TyreInfo ti = this.m_UnitOfWork.GetRepositoryBase<TyreInfo>().Get(tyreId);

                    if (ti.CurrentStatus == TyreStatus.Repair)
                    {
                        throw new Exception("该轮胎已经在维修中！请不要重复提交");
                    }
                    else if (ti.CurrentStatus == TyreStatus.Scrap)
                    {
                        throw new Exception("该轮胎已经在报废！");
                    }

                    ti.CurrentStatus = TyreStatus.Repair;
                    this.m_UnitOfWork.GetRepositoryBase<TyreInfo>().Update(ti, null);
                    TyreRepair tc = base.Add(entity);
                    tx.Commit();
                    return tc;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message, ex);
                    throw ex;
                }
            }
        }

        public void RepairResult(TyreRepair entity,bool Return)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    string tyreId = entity.TyreID;
                    TyreInfo ti = this.m_UnitOfWork.GetRepositoryBase<TyreInfo>().Get(tyreId);
                    
                    if (Return)  //是否将轮胎归还原车
                    {
                        ti.CurrentStatus = TyreStatus.Using;
                    }
                    else        
                    {
                       
                        if (Convert.ToBoolean(entity.RepairResult))
                        {
                            ti.CurrentStatus = TyreStatus.UsAble;
                        }
                        else
                        {
                            ti.CurrentStatus = TyreStatus.Scrap;
                        }
                        ti.CarID = null;
                        ti.InstallPlace = null;
                    }
                    this.m_UnitOfWork.GetRepositoryBase<TyreInfo>().Update(ti, null);
                    this.m_UnitOfWork.GetRepositoryBase<TyreRepair>().Update(entity, null);
                   
                    this.m_UnitOfWork.Flush();
                    tx.Commit();
                    
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message, ex);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 增加轮胎更换
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TyreRepair Add_noTransaction(TyreRepair entity)
        {

            try
            {
                string tyreId = entity.TyreID;
                TyreInfo ti = this.m_UnitOfWork.GetRepositoryBase<TyreInfo>().Get(tyreId);

                if (ti.CurrentStatus == TyreStatus.Repair)
                {
                    throw new Exception("该轮胎已经在维修中！请不要重复提交");
                }
                else if (ti.CurrentStatus == TyreStatus.Scrap)
                {
                    throw new Exception("该轮胎已经在报废！");
                }

                ti.CurrentStatus = TyreStatus.Repair;
                this.m_UnitOfWork.GetRepositoryBase<TyreInfo>().Update(ti, null);
                TyreRepair tc = base.Add(entity);

                return tc;
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        


    }
}

