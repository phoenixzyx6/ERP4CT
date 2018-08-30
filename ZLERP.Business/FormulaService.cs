using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.Model.Enums;

namespace ZLERP.Business
{
    public class FormulaService : ServiceBase<Formula>
    {
        internal FormulaService(IUnitOfWork uow)
            : base(uow) 
        {
 
        }

        /// <summary>
        /// 配比复制功能
        /// 暂时只实现理论配比到施工配比的复制
        /// </summary>
        /// <param name="op">操作</param>
        /// <param name="sid">源ID</param>
        /// <returns></returns>
        public bool StartCopy(string op, string sid,string did)
        {
            bool result = false;
            switch (op)
            {
                case "FO2CO":
                    result = FO2CO(sid,did);
                    break;
                case "FO2CU":
                    result = FO2CU(sid, did);
                    break;
                case "CO2FO":
                    break;
                case "CO2CU":
                    break;
                case "CU2CO":
                    result = CU2CO(sid, did);
                    break;
                case "CU2FO":
                    result = CU2FO(sid, did);
                    break;
            }
            return result;
        }

        /// <summary>
        /// Formula表数据导入到ConsMixprop表
        /// </summary>
        /// <param name="sid">Formula表主键ID</param>
        /// <returns></returns>
        public bool FO2CO(string sid, string did)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    Formula obj = this.Get(sid);
                    IConsMixpropRepository m_cons = this.m_UnitOfWork.ConsMixpropRepository;
                    IList<ProductLine> plList = this.m_UnitOfWork.GetRepositoryBase<ProductLine>()
                            .Query()
                            .Where(m => m.IsUsed)
                            .ToList();
                    ConsMixprop newobj = new ConsMixprop();
                    foreach (ProductLine pl in plList)
                    {
                        ConsMixprop tempobj = new ConsMixprop();
                        if (did.Length > 0)
                            tempobj.ID = did;
                        tempobj.ConStrength = obj.ConStrength;
                        tempobj.FlexStrength = obj.FlexStrength;
                        tempobj.FormulaID = obj.ID;
                        tempobj.IsSlurry = obj.FormulaType == "混凝土" ? false : true;
                        tempobj.ImpGrade = obj.ImpGrade;
                        tempobj.MixingTime = obj.MixingTime;
                        tempobj.Remark = obj.Remark;
                        tempobj.SCRate = obj.SCRate;
                        tempobj.SeasonType = obj.SeasonType;
                        tempobj.ProductLineID = pl.ID;

                        newobj = m_cons.Add(tempobj);

                        IList<FormulaItem> list = obj.FormulaItems;
                        foreach (FormulaItem item in list)
                        {
                            foreach (SiloProductLine sp in pl.SiloProductLines)
                            {
                                ConsMixpropItem temp = new ConsMixpropItem();
                                temp.ConsMixprop = tempobj;
                                temp.ConsMixpropID = tempobj.ID;
                                if (sp.Silo.StuffInfo.StuffType.ID == item.StuffType.ID)
                                {
                                    temp.Amount = item.StuffAmount * (decimal)sp.Rate;
                                    temp.Silo = sp.Silo;
                                    temp.SiloID = sp.Silo.ID;
                                    this.m_UnitOfWork.GetRepositoryBase<ConsMixpropItem>().Add(temp);
                                }
                            }
                        }
                    }
                    tx.Commit();
                    return true;
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
        /// Formula表数据导入到CustMixprop表
        /// </summary>
        /// <param name="sid">Formula表主键ID</param>
        /// <returns></returns>
        public bool FO2CU(string sid, string did)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    Formula obj = this.Get(sid);
                    CustMixprop newobj = new CustMixprop();
                    if (did.Length > 0)
                        newobj.ID = did;
                    newobj.CarpRadii = obj.CarpRadii;
                    newobj.CementType = obj.CementType;
                    newobj.ConStrength = obj.ConStrength;
                    newobj.ImpGrade = obj.ImpGrade;
                    newobj.Mesh = obj.Mesh;
                    newobj.MixpropCode = obj.FormulaName;
                    newobj.SCRate = obj.SCRate;
                    newobj.Slump = obj.Slump;
                    newobj.WCRate = obj.WCRate;
                    newobj.Weight = obj.Weight;

                    newobj = this.m_UnitOfWork.GetRepositoryBase<CustMixprop>().Add(newobj);

                    IList<FormulaItem> list = obj.FormulaItems;
                    IList<StuffInfo> slist = this.m_UnitOfWork.GetRepositoryBase<StuffInfo>().Query().Where(m => m.IsUsed).ToList();
                    foreach (FormulaItem item in list)
                    {                        
                        foreach (StuffInfo stuff in slist)
                        {
                            if (stuff.StuffTypeID.ToString() == item.StuffTypeID.ToString())
                            {                                
                                CustMixpropItem temp = new CustMixpropItem();
                                temp.Amount = item.StuffAmount;
                                temp.StandardAmount = item.StandardAmount;
                                temp.CustMixprop = newobj;
                                temp.CustMixpropID = newobj.ID;
                                temp.StuffInfo = stuff;
                                temp.StuffID = stuff.ID;
                                this.m_UnitOfWork.GetRepositoryBase<CustMixpropItem>().Add(temp);
                            }
                        }
                    }
                    tx.Commit();
                    return true;
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
        /// CustMixprop表数据导入到Formula表
        /// </summary>
        /// <param name="sid">CustMixprop表主键ID</param>
        /// <returns></returns>
        public bool CU2FO(string sid, string did)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    CustMixprop obj = this.m_UnitOfWork.GetRepositoryBase<CustMixprop>().Get(sid);
                    Formula newobj = new Formula();
                    if (did.Length > 0)
                        newobj.ID = did;
                    newobj.CarpRadii = obj.CarpRadii;
                    newobj.CementType = obj.CementType;
                    newobj.ConStrength = obj.ConStrength;
                    newobj.ImpGrade = obj.ImpGrade;
                    newobj.Mesh = obj.Mesh;
                    newobj.FormulaName = obj.MixpropCode;
                    newobj.SCRate = obj.SCRate;
                    newobj.Slump = obj.Slump;
                    newobj.WCRate = obj.WCRate;
                    newobj.Weight = obj.Weight;
                    newobj = this.m_UnitOfWork.GetRepositoryBase<Formula>().Add(newobj);

                    IList<CustMixpropItem> list = obj.CustMixpropItems;
                    foreach (CustMixpropItem item in list)
                    {
                        IList<StuffType> slist = this.m_UnitOfWork.GetRepositoryBase<StuffType>().All();
                        foreach (StuffType stype in slist)
                        {
                            if (item.StuffInfo.StuffType.ID == stype.ID)
                            {
                                FormulaItem temp = new FormulaItem();
                                temp.StandardAmount = item.StandardAmount ?? 0;
                                temp.StuffAmount = item.Amount??0;
                                temp.StuffType = stype;
                                temp.StuffTypeID = stype.ID;
                                temp.Formula = newobj;
                                temp.FormulaID = newobj.ID;
                                this.m_UnitOfWork.GetRepositoryBase<FormulaItem>().Add(temp);
                            }
                        }
                    }
                    tx.Commit();
                    return true;
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
        /// CustMixprop表数据导入到ConsMixprop表
        /// </summary>
        /// <param name="sid">CustMixprop表主键ID</param>
        /// <returns></returns>
        public bool CU2CO(string sid, string did)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    CustMixprop obj = this.m_UnitOfWork.GetRepositoryBase<CustMixprop>().Get(sid);
                    IConsMixpropRepository m_cons = this.m_UnitOfWork.ConsMixpropRepository;
                    IList<ProductLine> plList = this.m_UnitOfWork.GetRepositoryBase<ProductLine>()
                            .Query()
                            .Where(m => m.IsUsed)
                            .ToList();
                    ConsMixprop newobj = new ConsMixprop();
                    foreach (ProductLine pl in plList)
                    {
                        ConsMixprop tempobj = new ConsMixprop();
                        if (did.Length > 0)
                            tempobj.ID = did;
                        tempobj.ConStrength = obj.ConStrength;
                        tempobj.FormulaID = obj.ID;
                        tempobj.IsSlurry = obj.CementType == "混凝土" ? false : true;
                        tempobj.ImpGrade = obj.ImpGrade;
                        tempobj.SCRate = obj.SCRate;
                        tempobj.ProductLineID = pl.ID;

                        newobj = m_cons.Add(tempobj);

                        IList<CustMixpropItem> list = obj.CustMixpropItems;
                        foreach (CustMixpropItem item in list)
                        {
                            foreach (SiloProductLine sp in pl.SiloProductLines)
                            {
                                ConsMixpropItem temp = new ConsMixpropItem();
                                temp.ConsMixprop = tempobj;
                                temp.ConsMixpropID = tempobj.ID;
                                if (sp.Silo.StuffInfo.StuffType.ID == item.StuffInfo.StuffType.ID)
                                {
                                    temp.Amount = (decimal)item.Amount * (decimal)sp.Rate;
                                    temp.Silo = sp.Silo;
                                    temp.SiloID = sp.Silo.ID;
                                    this.m_UnitOfWork.GetRepositoryBase<ConsMixpropItem>().Add(temp);
                                }
                            }
                        }
                    }
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    throw;
                }
            }
        }

        public bool ExportStuff(string formulaid)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    IRepositoryBase<FormulaItem> itemResp = this.m_UnitOfWork.GetRepositoryBase<FormulaItem>();
                    IRepositoryBase<StuffType> typeResp = this.m_UnitOfWork.GetRepositoryBase<StuffType>();
                    IList<StuffType> list = typeResp.Query().Where(m=>m.TypeID=="StuffType").ToList();

                    IList<FormulaItem> items = itemResp.Query()
                        .Where(m => m.FormulaID == formulaid)
                        .ToList();
                    foreach (FormulaItem item in items)
                    {
                        itemResp.Delete(item);
                    }
                    foreach (StuffType item in list)
                    {
                        FormulaItem temp = new FormulaItem();
                        temp.StuffTypeID = item.ID;
                        temp.StuffAmount = 0;
                        temp.StandardAmount = 0;
                        temp.FormulaID = formulaid;
                        itemResp.Add(temp);                        
                    }
                    tx.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    return false;
                }
            }

        }

        public override void Delete(Formula entity)
        {
           
                try
                {

                    this.m_UnitOfWork.GetRepositoryBase<Formula>().Update(entity, null);
                    base.Delete(entity);
                   
                }
                catch (Exception ex)
                {
                   
                    logger.Error(ex.Message, ex);
                    throw ex;
                }
            
        }

        public override Formula Add(Formula entity)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                Formula formula = null;
                try
                {
                    formula = base.Add(entity);
                    this.m_UnitOfWork.Flush();

                    IRepositoryBase<FormulaItem> itemResp = this.m_UnitOfWork.GetRepositoryBase<FormulaItem>();
                    IRepositoryBase<StuffType> typeResp = this.m_UnitOfWork.GetRepositoryBase<StuffType>();
                    IList<StuffType> list = typeResp.Query().Where(m => (m.TypeID == "StuffType" && m.IsUsed)).OrderBy(m => m.OrderNum).ToList();

                    foreach (StuffType item in list)
                    {
                        FormulaItem temp = new FormulaItem();
                        temp.StuffTypeID = item.ID;
                        temp.StuffAmount = 0;
                        temp.StandardAmount = 0;
                        temp.FormulaID = formula.ID;
                        itemResp.Add(temp);
                    }
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
                return formula;
            }
        }
    }
}
