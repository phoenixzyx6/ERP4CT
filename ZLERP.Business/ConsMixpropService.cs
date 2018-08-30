using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Model;
using ZLERP.Model.Enums;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Business.ControlSystem;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Configuration;

namespace ZLERP.Business
{
    public class ConsMixpropService: ServiceBase<ConsMixprop>
    {
        ControlSystemHelper controlSystem;
        internal ConsMixpropService(IUnitOfWork uow) : base(uow) {
            controlSystem = new ControlSystemHelper();
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="consMixprop"></param>
        public void Audit(ConsMixprop consMixprop)
        {
            try
            {
                ConsMixprop cons = this.Get(consMixprop.ID);
                int? oldMixtime = cons.MixingTime;
                string auditor = AuthorizationService.CurrentUserID;
                cons.AuditStatus = consMixprop.AuditStatus;
                cons.AuditInfo = consMixprop.AuditInfo;
                cons.Auditor = auditor;
                cons.AuditTime = DateTime.Now;
                if (oldMixtime != consMixprop.MixingTime) {
                    cons.SynStatus = 0;//如果搅拌时间发生改变，那么配比得重新发送，将修改状态设置为修改中
                }
                cons.MixingTime = consMixprop.MixingTime;
                base.Update(cons, null);
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }
        }
        //public override void Update(ConsMixprop entity, System.Collections.Specialized.NameValueCollection form)
        //{
        //    using (var tx = this.m_UnitOfWork.BeginTransaction())
        //    {
        //        try
        //        {
        //            base.Update(entity, form);
        //            entity = this.Get(entity.ID);
        //            if (!entity.IsSlurry)
        //            {
        //                SysConfigService configService = new SysConfigService(this.m_UnitOfWork);
        //                SysConfig configFormulaRZMax = configService.GetSysConfig(SysConfigEnum.FormulaRZMax);
        //                SysConfig configFormulaRZMin = configService.GetSysConfig(SysConfigEnum.FormulaRZMin);
        //                decimal dFormulaRZMax = decimal.Parse(configFormulaRZMax.ConfigValue);
        //                decimal dFormulaRZMin = decimal.Parse(configFormulaRZMin.ConfigValue);
        //                decimal weight = entity.Weight ?? 0;
        //                if (weight > dFormulaRZMax || weight < dFormulaRZMin)
        //                {
        //                    throw new Exception("混凝土配比超出容重范围");
        //                }
        //            }
        //            if (entity.SynStatus == 1 )      //发送配比
        //            {
        //                /*//直连:通知控制系统修改配比
        //                by:Sky 2013/3/17
        //                *****************************************************************************/
        //                IList<ConsMixpropItem> cmItems = this.m_UnitOfWork.GetRepositoryBase<ConsMixpropItem>().All("ConsMixpropID='" + entity.ID + "'", "ID", true);
        //                ResultInfo result = controlSystem.UpdateConsMixprop(entity, cmItems);
        //                if (!result.Result)       //配比没有发送成功
        //                {
        //                    throw new Exception(result.Message);
        //                } 
        //            }
        //            tx.Commit();
        //        }
        //        catch (Exception e)
        //        {
        //            tx.Rollback();
        //            throw e;
        //        }
        //    }
        //}
        public void UpdateAuditStatus(ConsMixprop entity)
        {
            base.Update(entity, null);
        }
        
        /// 取消审核
        /// </summary>
        /// <param name="consMixID"></param>
        /// <param name="auditStatus"></param>
        public void UnAudit(string consMixID, int auditStatus)
        {
            try
            {
                ConsMixprop consmixprop = this.Get(consMixID);
                consmixprop.AuditStatus = auditStatus;
                consmixprop.Auditor = AuthorizationService.CurrentUserID;
                consmixprop.AuditTime = DateTime.Now;
                this.m_UnitOfWork.GetRepositoryBase<ConsMixprop>().Update(consmixprop, null);
                this.m_UnitOfWork.Flush();
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }
        }
        /// <summary>
        /// 检查施工配比是否超出计量范围
        /// </summary> 
        /// <param name="taskId"></param>
        /// <param name="productLineId"></param>
        /// <param name="rz">罐容比</param>
        /// <param name="checkConcrete">是否检查混凝土配比</param>
        /// <param name="checkSlurry">是否检查砂浆配比</param>
        /// <returns>未超出返回null, 否则返回超秤的错误信息</returns>
        public string CheckMesureScale(string taskId, string productLineId,  decimal concreteRZ, decimal slurryRZ, bool checkConcrete, bool checkSlurry)
        {
            return this.m_UnitOfWork.ConsMixpropRepository.CheckMesureScale(taskId, productLineId, concreteRZ, slurryRZ, checkConcrete, checkSlurry);
        }

        /// <summary>
        /// 条件查询，不返回记录总数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public override IList<ConsMixprop> Find(JqGridRequest request, string condition,out int total)
        {
            IList<ConsMixprop> ConsMixpropList = base.Find(request, condition,out total);
            IList<ConsMixprop> ConsMixpropCount = base.All("ConsMixpropID in (select key_value from SynMonitor where key_name = 'ConsMixpropID' union select ConsMixPropID from ConsMixpropItems where ConsMixpropItemsID in (select key_value from SynMonitor where key_name = 'ConsMixpropItemsID'))", "ID", true);
            foreach(ConsMixprop cm in ConsMixpropList)
            {
                if (ConsMixpropCount.Count(c=>c.ID == cm.ID) > 0)
                {
                    cm.CompletedSyn = false;
                }
                else
                {
                    cm.CompletedSyn = true;
                }
            }
            return ConsMixpropList;

        }

        public void SendModifiedPBToKZ(ConsMixprop entity, string[] DirtyDataArr)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    if (DirtyDataArr != null)
                    {
                        ////先保存子表数据,只用到编号与数量
                        string is2012="";
                        Synmonitor synTable = null;
                        PublicService op=null;

                        foreach (string DirtyData in DirtyDataArr)
                        {
                            string[] dirty = DirtyData.Split(',');
                            ConsMixpropItemService consMixpropItemservice = new ConsMixpropItemService(this.m_UnitOfWork);
                            ConsMixpropItem item = consMixpropItemservice.Get(Convert.ToInt32(dirty[0]));
                            item.Amount = Convert.ToDecimal(dirty[1]);
                            consMixpropItemservice.Update(item, null);

                            is2012 = ConfigurationManager.AppSettings["Is2012"];
                            if (is2012 == "true")
                            { 
                                //是2012工控则插入同步表
                                synTable = new Synmonitor();
                                synTable.tb_name = "view_ConsMixpropItems";
                                synTable.tb_action = "UPDATE";
                                synTable.key_name = "ConsMixpropID";
                                synTable.key_value = dirty[0];
                                synTable.key_type = "0";
                                synTable.action_time = DateTime.Now;
                                synTable.ProductLineID = item.ConsMixprop.ProductLineID;
                                if (op == null)
                                    op = new PublicService();
                                op.Synmonitor.Add(synTable);
                            }
                        }
                    }
                    IConsMixpropItemRepository IConItems = this.m_UnitOfWork.ConsMixpropItemRepository;
                    IList<ConsMixpropItem> citems = IConItems.Query().Where(m => m.ConsMixpropID == entity.ID).ToList();
                    decimal totolWeight = 0;
                    foreach (ConsMixpropItem citem in citems) {
                        totolWeight = totolWeight + citem.Amount;
                    }
                    ConsMixprop cons = this.Get(entity.ID);
                    cons.SynStatus = entity.SynStatus;
                    base.Update(cons, null);
                    entity = this.Get(entity.ID);
                    SysConfigService configService = new SysConfigService(this.m_UnitOfWork);
                    SysConfig config = configService.GetSysConfig(SysConfigEnum.IsAllowConsMixpropLimit);
                    if (!entity.IsSlurry)
                    {
                        SysConfig configFormulaRZMax = configService.GetSysConfig(SysConfigEnum.FormulaRZMax);
                        SysConfig configFormulaRZMin = configService.GetSysConfig(SysConfigEnum.FormulaRZMin);
                        decimal dFormulaRZMax = decimal.Parse(configFormulaRZMax.ConfigValue);
                        decimal dFormulaRZMin = decimal.Parse(configFormulaRZMin.ConfigValue);
                        decimal weight = entity.Weight ?? 0;
                        if (config != null && bool.Parse(config.ConfigValue) && entity.ProduceTask.CementType == Model.Enums.CementType.CommonCement)
                        {
                            if (totolWeight > dFormulaRZMax || totolWeight < dFormulaRZMin)
                            {
                                throw new Exception("混凝土配比超出容重范围");
                            }
                            string measureError = CheckMesureScale(entity.TaskID, entity.ProductLineID, 0, 0, true, false);
                            if (!string.IsNullOrEmpty(measureError))
                            {
                                throw new Exception(measureError);
                            }
                        }
                    }
                    else
                    {
                        if (config != null && bool.Parse(config.ConfigValue) && entity.ProduceTask.CementType == Model.Enums.CementType.CommonCement)
                        {
                            string measureError = CheckMesureScale(entity.TaskID, entity.ProductLineID, 0, 0, false, true);
                            if (!string.IsNullOrEmpty(measureError))
                            {
                                throw new Exception(measureError);
                            }
                        }
                    }
                    if (entity.SynStatus == 1)      //发送配比
                    {
                        /*//直连:通知控制系统修改配比
                        by:Sky 2013/3/17
                        *****************************************************************************/
                        IList<ConsMixpropItem> cmItems = this.m_UnitOfWork.GetRepositoryBase<ConsMixpropItem>().All("ConsMixpropID='" + entity.ID + "'", "ID", true);
                        ResultInfo result = controlSystem.UpdateConsMixprop(entity, cmItems);
                        if (!result.Result)       //配比没有发送成功
                        {
                            throw new Exception(result.Message);
                        }
                    }
                    tx.Commit();
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw e;
                }
            }
        }

        public dynamic GetDynamicColByPid(string pid) {
            IList<SiloProductLine> spllist = this.m_UnitOfWork.GetRepositoryBase<SiloProductLine>().Query().Where(m => m.ProductLineID == pid).OrderBy(m => m.OrderNum).ToList();
            IList<object> ColMs = new List<object>();
            foreach (SiloProductLine spl in spllist) {
                var colm = new
                {
                    label = spl.SiloName + "</br>" + spl.StuffName,
                    name = "S" + spl.OrderNum + "_wet",
                    index = "S" + spl.OrderNum + "_wet",
                    //width = 50,
                    align = "right",
                    editable = true,
                    //editrules = "{ number: true }",
                    sortable = false

                };
                ColMs.Add(colm);
            }
            return new
            {
                Result = true
                ,
                Message = string.Empty
                ,
                ColumnData = ColMs
            };
        }

        /// <summary>
        /// 保存横向与纵向配比
        /// </summary>
        /// <param name="cm"></param>
        /// <param name="updateKeys"></param>
        public IList<string> UpdatePrimaryAndItems(ConsMixprop cm, NameValueCollection updateKeys) {
            
            using (var tx = this.m_UnitOfWork.BeginTransaction()) {
                try
                {
                    IList<string> errorList = new List<string>();
                    string _ConsMixpropID = cm.ID;
                    //先更新主表，此时synstatus为0表示修改状态
                    this.Update(cm, updateKeys);
                    //获取需要更新的子表
                    cm = this.Get(cm.ID);
                    IList<SiloProductLine> siloProductLines = this.m_UnitOfWork.GetRepositoryBase<SiloProductLine>().Query().Where(m => m.ProductLineID == cm.ProductLineID).ToList();
                    var updateKeysOrderList = updateKeys.AllKeys.ToList();
                    Type tp = cm.GetType();
                    Dictionary<int, decimal> orderList = new Dictionary<int, decimal>();
                    string startWith = "S";
                    string endWith = "_wet";
                    foreach (string item in updateKeysOrderList) {
                        if (item.TrimStart().StartsWith(startWith) && item.TrimEnd().EndsWith(endWith))
                        {
                            Dictionary<int, decimal> orderAndValDic = new Dictionary<int, decimal>();
                            //取得orderNum
                            Regex rg = new Regex("(?<=(" + startWith + "))[.\\s\\S]*?(?=(" + endWith + "))",RegexOptions.Multiline | RegexOptions.Singleline);
                            int order = Convert.ToInt32(rg.Match(item).Value);
                            //取得orderNum对应的用量
                            var val = tp.GetProperty(string.Format("S{0}_wet", order).ToString()).GetValue(cm, null);
                            orderList.Add(order, Convert.ToDecimal(val));
                            //orderList.Add(orderAndValDic);
                        }
                        
                    }
                    //根据orderNum取得需要更新的siloID
                    Dictionary<string, decimal> needToUpdateSiloIdList = new Dictionary<string, decimal>();
                    foreach (SiloProductLine sp in siloProductLines) {
                        if (orderList.ContainsKey(sp.OrderNum)) {
                            string siloId = sp.SiloID;
                            needToUpdateSiloIdList.Add(siloId, orderList[sp.OrderNum]);
                        }
                    }

                    //开始更新子表
                    foreach (KeyValuePair<string, decimal> kvp in needToUpdateSiloIdList) {
                        ConsMixpropItem cmi = this.m_UnitOfWork.ConsMixpropItemRepository.Query().Where(c => c.ConsMixpropID == _ConsMixpropID && c.SiloID == kvp.Key).FirstOrDefault();
                        cmi.Amount = kvp.Value;
                        this.m_UnitOfWork.ConsMixpropItemRepository.Update(cmi, null);
                    }
                    //检查容重与水泥、水的用量
                    IConsMixpropItemRepository IConItems = this.m_UnitOfWork.ConsMixpropItemRepository;
                    IList<ConsMixpropItem> citems = IConItems.Query().Where(m => m.ConsMixpropID == cm.ID).ToList();
                    decimal totolWeight = 0;
                    foreach (ConsMixpropItem citem in citems)
                    {
                        totolWeight = totolWeight + citem.Amount;
                    }
                    SysConfigService configService = new SysConfigService(this.m_UnitOfWork);
                    SysConfig config = configService.GetSysConfig(SysConfigEnum.IsAllowConsMixpropLimit);
                    if (!cm.IsSlurry)
                    {
                        SysConfig configFormulaRZMax = configService.GetSysConfig(SysConfigEnum.FormulaRZMax);
                        SysConfig configFormulaRZMin = configService.GetSysConfig(SysConfigEnum.FormulaRZMin);
                        decimal dFormulaRZMax = decimal.Parse(configFormulaRZMax.ConfigValue);
                        decimal dFormulaRZMin = decimal.Parse(configFormulaRZMin.ConfigValue);
                        decimal weight = cm.Weight ?? 0;
                        if (config != null && bool.Parse(config.ConfigValue) && cm.ProduceTask.CementType == Model.Enums.CementType.CommonCement)
                        {
                            if (totolWeight > dFormulaRZMax || totolWeight < dFormulaRZMin)
                            {
                                errorList.Add("施工配比号[" + cm.ID + "]" + "&nbsp;&nbsp;超出混凝土配比容重范围");
                                tx.Rollback();
                                return errorList;
                                //throw new Exception("混凝土配比超出容重范围");
                            }
                            string measureError = CheckMesureScale(cm.TaskID, cm.ProductLineID, 0, 0, true, false);
                            if (!string.IsNullOrEmpty(measureError))
                            {
                                errorList.Add(measureError);
                                tx.Rollback();
                                return errorList;
                                //throw new Exception(measureError);
                            }
                        }
                    }
                    else
                    {
                        if (config != null && bool.Parse(config.ConfigValue) && cm.ProduceTask.CementType == Model.Enums.CementType.CommonCement)
                        {
                            string measureError = CheckMesureScale(cm.TaskID, cm.ProductLineID, 0, 0, false, true);
                            if (!string.IsNullOrEmpty(measureError))
                            {
                                errorList.Add(measureError);
                                tx.Rollback();
                                return errorList;
                            }
                        }
                    }
                    cm.SynStatus = 1;//主表与子表修改完毕后将SynStatus设为1，准备同步与发送搅拌楼
                    base.Update(cm, null);
                    if (cm.SynStatus == 1)      //发送配比至搅拌楼（12版楼站）
                    {
                        /*//直连:通知控制系统修改配比
                        by:Sky 2013/3/17
                        *****************************************************************************/
                        IList<ConsMixpropItem> cmItems = this.m_UnitOfWork.GetRepositoryBase<ConsMixpropItem>().All("ConsMixpropID='" + cm.ID + "'", "ID", true);
                        ResultInfo result = controlSystem.UpdateConsMixprop(cm, cmItems);
                        if (!result.Result)       //配比没有发送成功
                        {
                            errorList.Add(result.Message);
                            tx.Rollback();
                            return errorList;
                            //throw new Exception(result.Message);
                        }
                    }
                    tx.Commit();
                    return errorList;

                }
                catch(Exception e) {
                    tx.Rollback();
                    throw e;
                }
            }
        }
        
    }
}
