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
    public class ProductRecordService : ServiceBase<ProductRecord>
    {
        internal ProductRecordService(IUnitOfWork uow) : base(uow) { }

        public bool Import(string id)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {

                    bool result = true;

                    ProductRecord obj = this.Get(id);
                    ShippingDocument shipdoc = this.m_UnitOfWork.ShippingDocumentRepository.Get(obj.ShipDocID);
                    ConsMixprop cm = this.m_UnitOfWork.ConsMixpropRepository.Get(shipdoc.ConsMixpropID);
                    List<ConsMixpropItem> list = this.m_UnitOfWork.ConsMixpropItemRepository.Query().Where(m => m.ConsMixpropID == cm.ID && m.Amount > 0).ToList();

                    ThreadID tid;
                    PublicService ps=new PublicService();
                    foreach (ConsMixpropItem c in list)
                    {
                        ProductRecordItem tmp = new ProductRecordItem();
                        tmp.ActualAmount = obj.ProduceCube * c.Amount;
                        tmp.TheoreticalAmount = obj.ProduceCube * c.Amount;
                        tmp.SiloID = c.SiloID;
                        tmp.StuffID = c.Silo.StuffInfo.ID;
                        tmp.ProductRecordID = id;
                        tmp.ErrorValue = 0;
                        tmp.ProductLineID = obj.ProductLineID;
                        this.m_UnitOfWork.GetRepositoryBase<ProductRecordItem>().Add(tmp);

                        tid = new ThreadID();
                        tid.currentDate = DateTime.Now;
                        tid.typeID = tmp.StuffID;//主材id
                        tid.typename = "0";//主材
                        ps.ThreadID.Add(tid);

                    }
                    tx.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    throw new Exception(ex.Message);
                }
            }
        }

        public override void Delete(object[] ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    ThreadID tid;
                    PublicService ps = new PublicService();

                    foreach (string id in ids)
                    {
                        IList<ProductRecordItem> items =  this.m_UnitOfWork.GetRepositoryBase<ProductRecordItem>().Query().Where(m => m.ProductRecordID == id).ToList();
                        foreach (ProductRecordItem iitem in items)
                        {
                            this.m_UnitOfWork.GetRepositoryBase<ProductRecordItem>().Delete(iitem);

                            tid = new ThreadID();
                            tid.currentDate = DateTime.Now;
                            tid.typeID = iitem.StuffID;//主材id
                            tid.typename = "0";//主材
                            ps.ThreadID.Add(tid);
                        }                       
                    }
                    
                    base.Delete(ids);
                    //tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    throw new Exception(ex.Message);
                }
            }
        }

    }
}
