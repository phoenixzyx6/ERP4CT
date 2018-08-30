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
    public class TyreChangeService : ServiceBase<TyreChange>
    {
        internal TyreChangeService(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// 增加轮胎更换
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TyreChange AddChange(TyreChange entity, string OldTyreAction)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                 
                    TyreInfo _oldTyre = this.m_UnitOfWork.GetRepositoryBase<TyreInfo>().Get(entity.OldTyreID);
                    TyreInfo _newTyre = this.m_UnitOfWork.GetRepositoryBase<TyreInfo>().Get(entity.NewTyreID);
                 
                    //新增轮胎更换记录
                    entity.TyreType = _newTyre.TyreType;
                    entity.InstallPlace = _oldTyre.InstallPlace;
                    TyreChange tc = this.m_UnitOfWork.GetRepositoryBase<TyreChange>().Add(entity);

                    //处理旧轮胎
                    _oldTyre.CarID = null;
                    _oldTyre.InstallPlace = null;
                    if (OldTyreAction != TyreStatus.Repair)     
                    {
                        _oldTyre.CurrentStatus = OldTyreAction;
                    }
                    
                    this.m_UnitOfWork.GetRepositoryBase<TyreInfo>().Update(_oldTyre,null);

                    if (OldTyreAction == TyreStatus.Repair)         //若旧胎处理为维修，则在新增一条维修记录
                    {
                        TyreRepair tr = new TyreRepair();
                        tr.ReceiveDate = DateTime.Now;
                        tr.RepairDate = DateTime.Now;
                        tr.Remark = entity.ChangeReason;
                        tr.RepairType = entity.ChangeReason;
                        tr.TyreID = _oldTyre.ID;
                       
                        new TyreRepairService(this.m_UnitOfWork).Add_noTransaction(tr);
                        
                     
                    }

                    //处理新轮胎
                    _newTyre.CarID = entity.CarID;
                    _newTyre.InstallPlace = entity.InstallPlace;
                    _newTyre.CurrentStatus = TyreStatus.Using;
                    this.m_UnitOfWork.GetRepositoryBase<TyreInfo>().Update(_newTyre, null);

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


        public override void Delete(TyreChange entity)
        {
            
                try
                {
                    string tyreId = entity.NewTyreID;
                    TyreInfo ti = this.m_UnitOfWork.GetRepositoryBase<TyreInfo>().Get(tyreId);
                    ti.CurrentStatus = TyreStatus.UsAble;
                    ti.CarID = null;
                    ti.InstallPlace = null;
                    this.m_UnitOfWork.GetRepositoryBase<TyreInfo>().Update(ti, null);
                    base.Delete(entity);
                   
                }
                catch (Exception ex)
                {
                  
                    logger.Error(ex.Message, ex);
                    throw ex;
                }
            
        }



    }
}

