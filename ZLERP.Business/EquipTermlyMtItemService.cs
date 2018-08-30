using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;

namespace ZLERP.Business
{
    public class EquipTermlyMtItemService : ServiceBase<EquipTermlyMtItem>
    {
        internal EquipTermlyMtItemService(IUnitOfWork uow) : base(uow) { }

        //public override void Update(EquipTermlyMtItem EquipTermlyMtItem, System.Collections.Specialized.NameValueCollection form)

        //{
        //    EquipTermlyMtItem etm = this.Get(EquipTermlyMtItem.ID);
        //    base.Update(EquipTermlyMtItem, null);
        //}



       
        public override void Delete(EquipTermlyMtItem entity)
        {

            try
            {
                IRepositoryBase<PartInfo> partInfoResp = this.m_UnitOfWork.GetRepositoryBase<PartInfo>();
                PartInfo partInfo = partInfoResp.Get(entity.PartID);

                if (entity.Amount != null)
                {
                    partInfo.Inventory += (int)entity.Amount;
                }
                partInfoResp.Update(partInfo, null);
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
