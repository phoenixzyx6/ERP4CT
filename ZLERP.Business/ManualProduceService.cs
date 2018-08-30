using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using ZLERP.Model.Enums;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;

namespace ZLERP.Business
{
    public class ManualProduceService : ServiceBase<ManualProduce>
    {
        internal ManualProduceService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public override ManualProduce Add(ManualProduce entity)
        {
            ProductLine line = base.m_UnitOfWork.GetRepositoryBase<ProductLine>().Get(entity.ProductLineID);
            if (line != null)
            {
                entity.ProductLineName = line.ProductLineName;
            }
            return base.Add(entity);
        }


        public object getDispatchInfoByID(string id)
        {
            string shipDocID = string.Empty;
            int? nullable = 0;
            DispatchList list = base.m_UnitOfWork.GetRepositoryBase<DispatchList>().Get(id);
            if (list != null)
            {
                shipDocID = list.ShipDocID;
                nullable = list.BTotalPot + list.STotalPot;
            }
            else
            {
                DispatchListHistory history = base.m_UnitOfWork.GetRepositoryBase<DispatchListHistory>().Find("DispatchID = '" + id + "'", 1, 10, "ASC", "AutoID").FirstOrDefault<DispatchListHistory>();
                if (history != null)
                {
                    shipDocID = history.ShipDocID;
                    nullable = history.BTotalPot + history.STotalPot;
                }
            }
            ShippingDocument document = base.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Get(shipDocID);
            return new { Result = true, Message = string.Empty, TaskID = (document == null) ? string.Empty : document.TaskID, ShipDocID = (document == null) ? string.Empty : document.ID, ProjectName = (document == null) ? string.Empty : document.ProjectName, ConStrength = (document == null) ? string.Empty : document.ConStrength, ProductLineID = (document == null) ? string.Empty : document.ProductLineID, CarID = (document == null) ? string.Empty : document.CarID, SendCube = (document == null) ? 0M : document.SendCube, SendPot = nullable, Operator = (document == null) ? string.Empty : document.Operator };
        }

        public override void Update(ManualProduce entity, NameValueCollection form)
        {
            ProductLine line = base.m_UnitOfWork.GetRepositoryBase<ProductLine>().Get(entity.ProductLineID);
            if (line != null)
            {
                entity.ProductLineName = line.ProductLineName;
            }
            base.Update(entity, form);
        }

 


    }
}
