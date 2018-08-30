using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;

namespace ZLERP.Business
{
    public class PartBorrowReturnService : ServiceBase<PartBorrowReturn>
    {
        internal PartBorrowReturnService(IUnitOfWork uow) : base(uow) { }
        public override PartBorrowReturn Add(PartBorrowReturn entity)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    IList<PartBorrowItem> pbiList = this.m_UnitOfWork.GetRepositoryBase<PartBorrowItem>().Query().Where(p => p.BorrowID == entity.BorrowID && p.PartID == entity.PartID).ToList();
                    if (pbiList.Count > 0)
                    {
                        PartBorrowItem pbi = pbiList[0];
                        if (pbi.BorrowNum < entity.ReturnNum)
                        {
                            string str = String.Format("归还数量{0}大于当前借用数量{1}，添加失败！", entity.ReturnNum, pbi.BorrowNum);
                            throw new Exception(str);
                        }
                        if (pbi.ReturnNum == null) { pbi.ReturnNum = 0; }
                        pbi.ReturnNum += entity.ReturnNum;
                        this.m_UnitOfWork.GetRepositoryBase<PartBorrowItem>().Update(pbi, null);
                    }

                    PartInfo part = this.m_UnitOfWork.GetRepositoryBase<PartInfo>().Get(entity.PartID);
                    part.Inventory += entity.ReturnNum;
                    this.m_UnitOfWork.GetRepositoryBase<PartInfo>().Update(part, null);
                    tx.Commit();
                    return base.Add(entity);
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    throw;
                }
            }
        }
        public override void Update(PartBorrowReturn entity, System.Collections.Specialized.NameValueCollection form)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    IList<PartBorrowReturn> pbrList = this.m_UnitOfWork.GetRepositoryBase<PartBorrowReturn>().Query().Where(p => p.ID == entity.ID).ToList();
                    PartBorrowReturn pbr = pbrList[0];
                    if (pbr.ReturnNum != entity.ReturnNum)
                    {
                        decimal count = entity.ReturnNum - pbr.ReturnNum;
                        IList<PartBorrowItem> pbiList = this.m_UnitOfWork.GetRepositoryBase<PartBorrowItem>().Query().Where(p => p.BorrowID == pbr.BorrowID && p.PartID == pbr.PartID).ToList();
                        if (pbiList.Count > 0)
                        {
                            PartBorrowItem pbi = pbiList[0];
                            pbi.ReturnNum = pbi.ReturnNum + count;
                            this.m_UnitOfWork.GetRepositoryBase<PartBorrowItem>().Update(pbi, null);
                        }



                        PartInfo part = this.m_UnitOfWork.GetRepositoryBase<PartInfo>().Get(pbr.PartID);
                        part.Inventory += count;
                        this.m_UnitOfWork.GetRepositoryBase<PartInfo>().Update(part, null);
                    }



                   

                    tx.Commit();
                    base.Update(entity, form);
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    throw;
                }
            }
        }


        public void Deletes(int[] ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {

                    foreach (int id in ids)
                    {
                        PartBorrowReturn pbr = this.m_UnitOfWork.GetRepositoryBase<PartBorrowReturn>().Get(id);
                        IList<PartBorrowItem> pbiList = this.m_UnitOfWork.GetRepositoryBase<PartBorrowItem>().Query().Where(p => p.BorrowID == pbr.BorrowID && p.PartID == pbr.PartID).ToList();
                        if (pbiList.Count > 0)
                        {
                            PartBorrowItem pbi = pbiList[0];
                            pbi.ReturnNum = pbi.ReturnNum - pbr.ReturnNum;
                            this.m_UnitOfWork.GetRepositoryBase<PartBorrowItem>().Update(pbi, null);
                        }

                        PartInfo part = this.m_UnitOfWork.GetRepositoryBase<PartInfo>().Get(pbr.PartID);
                        part.Inventory -= pbr.ReturnNum;
                        this.m_UnitOfWork.GetRepositoryBase<PartInfo>().Update(part, null);
                        tx.Commit();
                        base.Delete(pbr);
                    }
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
