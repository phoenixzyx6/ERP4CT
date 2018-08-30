using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;

namespace ZLERP.Business
{
    public class EquipMtLyService : ServiceBase<EquipMtLy>
    {
        internal EquipMtLyService(IUnitOfWork uow) : base(uow) { }

        public void ImportMntZlItems(string equipmtlyid, string[] ids)
        {

            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    foreach (string mntzlitemid in ids)
                    {
                        //按支领明细ID查找支领明细数据
                        MntZlItem mntzlitem = this.m_UnitOfWork.GetRepositoryBase<MntZlItem>().Get(Convert.ToInt32(mntzlitemid));
                        EquipMtLyItem equipmtlyitem = new EquipMtLyItem();
                        equipmtlyitem.EquipMtLyID = equipmtlyid;
                        //equipmtlyitem.MaintenanceID = mntzlitem.MaintenanceID;
                        //equipmtlyitem.MntZlItemID =  Convert.ToInt32(mntzlitemid);
                        equipmtlyitem.DepartmentID = mntzlitem.MntZl.DepartmentID;
                        //equipmtlyitem.PartID = mntzlitem.PartID;
                        equipmtlyitem.Amount = mntzlitem.amount ?? 0;
                        //equipmtlyitem.PurposeMill = mntzlitem.PurposeMill;
                        equipmtlyitem.UserID = mntzlitem.MntZl.UserID;
                        //equipmtlyitem.IsAssess = mntzlitem.IsAssess;
                        equipmtlyitem.Remark = mntzlitem.Remark;

                        this.m_UnitOfWork.GetRepositoryBase<EquipMtLyItem>().Add(equipmtlyitem);
                        this.m_UnitOfWork.Flush();
                    }
                    tx.Commit();
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    logger.Error(e.Message, e);
                    throw e;
                }
            }
        }

    }
}
