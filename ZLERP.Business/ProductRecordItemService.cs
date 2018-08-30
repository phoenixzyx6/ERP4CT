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
    public class ProductRecordItemService : ServiceBase<ProductRecordItem>
    {
        internal ProductRecordItemService(IUnitOfWork uow) : base(uow) { }



        public override ProductRecordItem Add(ProductRecordItem entity)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var ProductRecord = this.m_UnitOfWork.GetRepositoryBase<ProductRecord>().Get(entity.ProductRecordID);
                    var consMixpropID = this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Get(ProductRecord.ShipDocID).ConsMixpropID;
                    var amount = this.m_UnitOfWork.GetRepositoryBase<ConsMixpropItem>().Query().
                       Where(a => a.ConsMixpropID == consMixpropID && a.SiloID == entity.SiloID).Select(a => a.Amount).FirstOrDefault();

                    entity.TheoreticalAmount = amount * ProductRecord.ProduceCube;
                    decimal tempActualAmount = entity.ActualAmount != null ? Convert.ToDecimal(entity.ActualAmount) : 0;
                    decimal tempTheoreticalAmount = entity.TheoreticalAmount != null ? Convert.ToDecimal(entity.TheoreticalAmount) : 0;
                    if (tempTheoreticalAmount != 0)
                    {
                        entity.ErrorValue = Math.Round(((tempActualAmount - tempTheoreticalAmount) / tempTheoreticalAmount * 100), 2);
                    }
                    else
                    {
                        entity.ErrorValue = 0;

                    }
                    IRepositoryBase<StuffInfo> stuffinfoRepository = this.m_UnitOfWork.GetRepositoryBase<StuffInfo>();
                    Silo silo = this.m_UnitOfWork.GetRepositoryBase<Silo>().Get(entity.SiloID);
                    StuffInfo stuffinfo = stuffinfoRepository.Get(entity.StuffID);
                    silo.Content -= tempActualAmount;
                    stuffinfo.Inventory -= tempActualAmount;
                    this.m_UnitOfWork.GetRepositoryBase<Silo>().Update(silo, null);
                    stuffinfoRepository.Update(stuffinfo, null);
                    //this.m_UnitOfWork.Flush();
                    ProductRecordItem obj = base.Add(entity);

                    ThreadID tid;
                    PublicService ps = new PublicService();
                    tid = new ThreadID();
                    tid.currentDate = DateTime.Now;
                    tid.typeID = entity.StuffID;//主材id
                    tid.typename = "0";//主材
                    ps.ThreadID.Add(tid);

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
