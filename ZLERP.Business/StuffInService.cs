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
    public class StuffInService : ServiceBase<StuffIn>
    {

        internal StuffInService(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="id"></param>
        public void ExecutePrice(string ids)
        {
            this.m_UnitOfWork.StuffInRepository.ExecutePrice(ids, AuthorizationService.CurrentUserID);
        }

        public override void Delete(StuffIn entity)
        {

            try
            {
                StuffIn _si = this.Get(entity.ID);
                if (entity.Lifecycle == 1)
                {
                    //Silo silo = this.m_UnitOfWork.GetRepositoryBase<Silo>().Get(entity.SiloID);
                    //silo.Content -= entity.InNum;
                    //StuffInfo stuff = this.m_UnitOfWork.GetRepositoryBase<StuffInfo>().Get(entity.StuffID);
                    //stuff.Inventory -= entity.InNum;
                    //this.m_UnitOfWork.GetRepositoryBase<Silo>().Update(silo, null);
                    //this.m_UnitOfWork.GetRepositoryBase<StuffInfo>().Update(stuff, null);
                    //this.m_UnitOfWork.Flush();
                    throw new Exception("进货单[" + entity.ID + "]已审核，无法删除");
                }
                else
                {
                    entity.Lifecycle = -1;
                    base.Update(entity, null);
                }

            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                throw;
            }

        }

        public override void Update(StuffIn entity, NameValueCollection form)
        {

            try
            {
                StuffIn _si = this.Get(entity.ID);
                if (_si.Lifecycle == 1 || _si.Lifecycle == -1)
                {
                    //Silo silo = this.m_UnitOfWork.GetRepositoryBase<Silo>().Get(entity.SiloID);
                    //silo.Content -= entity.InNum;
                    //StuffInfo stuff = this.m_UnitOfWork.GetRepositoryBase<StuffInfo>().Get(entity.StuffID);
                    //stuff.Inventory -= entity.InNum;
                    //this.m_UnitOfWork.GetRepositoryBase<Silo>().Update(silo, null);
                    //this.m_UnitOfWork.GetRepositoryBase<StuffInfo>().Update(stuff, null);
                    //this.m_UnitOfWork.Flush();
                    throw new Exception("进货单[" + entity.ID + "]，无法修改");
                }

                // base.Update(entity, null);
                IRepositoryBase<StuffIn> StuffRespository = this.m_UnitOfWork.GetRepositoryBase<StuffIn>();
                StuffRespository.Update(entity, form);
                this.m_UnitOfWork.Flush();
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                throw;
            }

        }

        public void Audit(string id)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    StuffIn obj = this.Get(id);
                    if (obj.Lifecycle == 0)
                    {
                        //Silo silo = this.m_UnitOfWork.GetRepositoryBase<Silo>().Get(obj.SiloID);
                        //decimal baseStore = silo.Content;
                        //silo.Content += obj.InNum;
                        //this.m_UnitOfWork.GetRepositoryBase<Silo>().Update(silo,null);
                        //StuffInfo stuff = this.m_UnitOfWork.GetRepositoryBase<StuffInfo>().Get(obj.StuffID);
                        //stuff.Inventory += obj.InNum;
                        //this.m_UnitOfWork.GetRepositoryBase<StuffInfo>().Update(stuff, null);
                        //this.m_UnitOfWork.Flush();
                        obj.TotalPrice = obj.SupplyNum * obj.UnitPrice/1000;
                        obj.Lifecycle = 1;
                        base.Update(obj, null);

                        //添加入库记录
                        //SiloLedgerContent_InAndCheck slc = new SiloLedgerContent_InAndCheck();
                        //slc.SiloID = obj.SiloID;
                        //slc.StuffID = obj.StuffID;
                        //slc.Action = "手动入库";
                        //slc.BaseStore = baseStore;
                        //slc.Num = obj.InNum;
                        //slc.Remaining = silo.Content;
                        //slc.ActionTime = DateTime.Now;
                        //slc.UserName = AuthorizationService.CurrentUserName;
                        //this.m_UnitOfWork.GetRepositoryBase<SiloLedgerContent_InAndCheck>().Add(slc);
                    }

                    tx.Commit();

                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    throw;
                }
            }
        }

        public override StuffIn Add(StuffIn entity)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    entity.AH = "Handle";

                    IRepositoryBase<StuffInfo> stuffinfoResp = this.m_UnitOfWork.GetRepositoryBase<StuffInfo>();
                    StuffInfo stuff = stuffinfoResp.Get(entity.StuffID);

                    IRepositoryBase<Silo> siloResp = this.m_UnitOfWork.GetRepositoryBase<Silo>();
                    Silo silo = siloResp.Get(entity.SiloID);

                    //首先判断是进货还是退货？   
                    //if (!entity.IsBack) //进货则增加原材料库存、筒仓库存
                    //{

                    //    //stuff.Inventory += entity.InNum;

                    //    //silo.Content += entity.InNum;


                    //}
                    //else//退货则减少原材料库存、筒仓库存
                    //{
                    //    stuff.Inventory -= entity.InNum;

                    //    silo.Content -= entity.InNum;
                    //}

                    if (entity.IsExam)
                    {
                        switch (stuffinfoResp.Get(entity.StuffID).StuffTypeID)
                        {
                            case "CE":
                                CEExam exam = new CEExam();
                                exam.DeputyNum = entity.InNum;
                                exam.StuffID = entity.StuffID;
                                exam.ReportTime = DateTime.Now;
                                this.m_UnitOfWork.GetRepositoryBase<CEExam>().Add(exam);
                                break;
                            case "FA":
                                FAExam fa = new FAExam();
                                fa.DeputyNum = entity.InNum;
                                fa.StuffID = entity.StuffID;
                                fa.ReportTime = DateTime.Now;
                                this.m_UnitOfWork.GetRepositoryBase<FAExam>().Add(fa);
                                break;
                            case "CA":
                                CAExam ca = new CAExam();
                                ca.DeputyNum = entity.InNum;
                                ca.StuffID = entity.StuffID;
                                ca.ReportTime = DateTime.Now;
                                this.m_UnitOfWork.GetRepositoryBase<CAExam>().Add(ca);
                                break;
                            case "AIR1":
                                AIR1Exam air1 = new AIR1Exam();
                                air1.DeputyNum = entity.InNum;
                                air1.StuffID = entity.StuffID;
                                air1.ReportTime = DateTime.Now;
                                this.m_UnitOfWork.GetRepositoryBase<AIR1Exam>().Add(air1);
                                break;
                            case "AIR2":
                                AIR2Exam air2 = new AIR2Exam();
                                air2.DeputyNum = entity.InNum;
                                air2.StuffID = entity.StuffID;
                                air2.ReportTime = DateTime.Now;
                                this.m_UnitOfWork.GetRepositoryBase<AIR2Exam>().Add(air2);
                                break;
                            case "ADM1":
                                ADMExam adm = new ADMExam();
                                adm.DeputyNum = entity.InNum;
                                adm.StuffID = entity.StuffID;
                                adm.ReportTime = DateTime.Now;
                                this.m_UnitOfWork.GetRepositoryBase<ADMExam>().Add(adm);
                                break;
                            case "ADM2":
                                ADM2Exam adm2 = new ADM2Exam();
                                adm2.DeputyNum = entity.InNum;
                                adm2.StuffID = entity.StuffID;
                                adm2.ReportTime = DateTime.Now;
                                this.m_UnitOfWork.GetRepositoryBase<ADM2Exam>().Add(adm2);
                                break;
                        }
                    }
                    //stuffinfoResp.Update(stuff, null);
                    //siloResp.Update(silo, null);
                    StuffIn obj = base.Add(entity);
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
        /// 审核
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sp"></param>
        /// <param name="price"></param>
        /// <param name="Guige"></param>
        /// <param name="supplyNum"></param>
        /// <param name="stuffid"></param>
        public void Audit(string id, StockPact sp, decimal price, string Guige, decimal supplyNum, string stuffid)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {

                    StuffIn obj = this.Get(id);
                    if (obj.Lifecycle == 0)
                    {
                        //if (obj.StuffID == sp.StuffID && obj.SupplyID == sp.SupplyID)
                        if (obj.StuffID == stuffid && obj.SupplyID == sp.SupplyID)
                        {
                            if (Guige != "")
                            {
                                obj.Guige = Guige;
                            }
                            obj.Proportion = (obj.Proportion == 0 ? 1 : obj.Proportion);
                            obj.FootNum = obj.InNum / obj.Proportion;
                            obj.SupplyNum = supplyNum * 1000;
                            decimal bc = obj.SupplyNum - Convert.ToDecimal(obj.FootNum);
                            obj.Bangcha = bc;
                            obj.FinalFootNum = obj.FootNum;
                            if (sp.BangchaRate > 0)
                            {
                                if (sp.BangchaRate * obj.FootNum>=bc)
                                {
                                    obj.FinalFootNum = obj.SupplyNum;
                                }
                            }
                            obj.UnitPrice = price;
                            obj.TotalPrice = price * obj.FinalFootNum / 1000;
                            obj.StockPactID = sp.ID;
                            obj.Lifecycle = 1;
                            base.Update(obj, null);
                        }
                    }

                    tx.Commit();

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
        /// 取消审核
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="auditStatus"></param>
        public void UnAudit(string contractID, int auditStatus)
        {
            try
            {
                StuffIn contract = this.Get(contractID);
                contract.Lifecycle = 0;
                contract.Modifier = AuthorizationService.CurrentUserID;
                contract.ModifyTime = DateTime.Now;
                this.m_UnitOfWork.GetRepositoryBase<StuffIn>().Update(contract, null);
                this.m_UnitOfWork.Flush();
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }
        }
    }
}

