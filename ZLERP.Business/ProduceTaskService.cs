using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using System.Reflection;
using Lib.Web.Mvc.JQuery.JqGrid;
using System.Configuration;　
namespace ZLERP.Business
{
    public  class ProduceTaskService : ServiceBase<ProduceTask>
    {
        internal ProduceTaskService(IUnitOfWork uow)
            : base(uow) 
        { 
        }
        public override ProduceTask Add(ProduceTask entity)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    base.CheckIsAutoAudit(entity);
                    var project = CreateProject(entity);
                    entity.ProjectID = project.ID;
                    ProduceTask tempTask = base.Add(entity);
                    tx.Commit();
                    SystemMsgService systemMsgService = new SystemMsgService(this.m_UnitOfWork);
                    systemMsgService.SendMsg("SendPb", "任务单号：" + tempTask.ID);
                    return tempTask;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw ex;
                }
            }
            
        }
        /// <summary>
        /// 创建工地明细，如存在则不会重复创建
        /// </summary>
        /// <param name="tempTask"></param>
        public Project CreateProject(ProduceTask tempTask) {

            ProjectService ps = new ProjectService(this.m_UnitOfWork);
            //检查同一合同下是否有同名的工程
            var oldProject = ps.Query()
                .Where(p => p.ContractID == tempTask.ContractID && p.ProjectName == tempTask.ProjectName)
                .FirstOrDefault();

            string projectID = "";
            Project pj = ps.Query().Where(p => p.ID.Contains("P" + DateTime.Now.ToString("yyyyMM"))).OrderByDescending(p => p.ID).FirstOrDefault();
            if (pj == null)
            {
                projectID = "P" + DateTime.Now.ToString("yyyyMM") + "001";
            }
            else
            {
                projectID = pj.ID.Substring(7, 3);
                int k = Convert.ToInt32(projectID);
                k++;
                projectID = "P" + DateTime.Now.ToString("yyyyMM") + (k.ToString().Length == 1 ? ("00" + k.ToString()) : (k.ToString().Length == 2 ? ("0" + k.ToString()) : k.ToString()));
            }


            if (oldProject == null)
            {
                //没有该工程，自动新建，以便重用，同时解决与GPS对接时每发一次调度会在GPS增加一个工地的问题
                Project project = new Project();
                project.ID = projectID;
                project.ProjectName = tempTask.ProjectName;
                project.ProjectAddr = tempTask.ProjectAddr;
                project.ConstructUnit = tempTask.ConstructUnit;
                project.BuildUnit = tempTask.BuildUnit;
                project.LinkMan = tempTask.LinkMan;
                project.Tel = tempTask.Tel;
                project.ContractID = tempTask.ContractID;
                return ps.Add(project);
            }
            else
                return oldProject;
        }
        
        public IList<ProduceTask> GetTodayTasks()
        {
            return base.Query()
                .Where(d => d.NeedDate >=DateTime.Today && d.NeedDate<=DateTime.Today.AddDays(1))
                .ToList();
        }


        /// <summary>
        /// 通过标准配比用量生成施工配比
        /// </summary>
        /// <param name="taskid">任务单编号</param>
        /// <param name="itemids">配比细表编号数组</param>
        /// <param name="amounts">用量信息数组</param>
        /// <param name="plist">生产线数组</param>
        /// <param name="formulaid">理论配比ID</param>
        /// <param name="isSlurry">是否为砂浆？</param>
        /// <returns>操作是否成功？</returns>
        /*public bool GenConsMixprop(string taskid, int[] itemids, decimal[] amounts, string[] plist, string formulaid, bool isSlurry, decimal siwrate, decimal riwrate, decimal riwrate1, decimal sirrate)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    SysConfig config =  new SysConfigService(this.m_UnitOfWork).GetSysConfig(Model.Enums.SysConfigEnum.IsAllowUpdateProducing);
                    if (config != null && config.ConfigValue == "false")//不允许在生产时修改配比
                    {
                        var DispatchLists = this.m_UnitOfWork.GetRepositoryBase<DispatchList>().Query().Where(p => (p.TaskID == taskid && p.IsRunning == true && p.IsCompleted == false)).ToList();

                        if (DispatchLists.Count > 0)
                        {
                            logger.Error("正在生产的任务单不允许修改配比");
                            throw new Exception("正在生产的任务单不允许修改配比");

                        }
                    }
                    ProduceTask taskObj = this.Get(taskid);

                    IConsMixpropRepository ICons = this.m_UnitOfWork.ConsMixpropRepository;
                    IRepositoryBase<Formula> formulaRepository = this.m_UnitOfWork.GetRepositoryBase<Formula>();
                    IConsMixpropItemRepository IConItems = this.m_UnitOfWork.ConsMixpropItemRepository;
                    Formula formulaObj = formulaRepository.Get(formulaid);
                    IList<FormulaItem> formulaItems = this.m_UnitOfWork.GetRepositoryBase<FormulaItem>().All(string.Format("FormulaID = '{0}'", formulaid), "ID", true);
                    IList<ProductLine> productLines = this.m_UnitOfWork.GetRepositoryBase<ProductLine>().Query().Where(m => m.IsUsed).ToList();
                    if (isSlurry)
                    {
                        taskObj.IsSlurry = true;
                        taskObj.SlurryFormulaInfo = formulaObj;
                    }
                    else
                    {
                        taskObj.BetonFormulaInfo = formulaObj;
                    }
                    taskObj.SIWRate = siwrate;
                    taskObj.SIRRate = sirrate;
                    taskObj.RIWRate = riwrate;
                    taskObj.RIWRate1 = riwrate1;
                    base.Update(taskObj, null);

                    siwrate /= 100;
                    sirrate /= 100;
                    riwrate /= 100;
                    riwrate1 /= 100;
                    foreach (string pl in plist)
                    {
                        ProductLine pline = productLines.FirstOrDefault(p => p.ID == pl);
                        IList<SiloProductLine> silos = pline.SiloProductLines;

                        //1. 该任务单对应的施工配比是否存在？，
                        IList<ConsMixprop> list = ICons.Query()
                            .Where(c => c.ProduceTask.ID == taskid && c.ProductLineID == pl && c.IsSlurry == isSlurry)
                            .ToList();

                        ConsMixprop cm = new ConsMixprop();
                        //2. 存在则找出更新
                        if (list.Count > 0)
                        {
                            //cm = list[0];
                            //foreach (ConsMixprop c in list)
                            //{
                            //    ICons.Delete(c);
                            //}
                            ConsMixprop temp = list[0];
                            temp.FormulaID = formulaid;
                            temp.TaskID = taskObj.ID;
                            temp.ConStrength = formulaObj.ConStrength;
                            temp.FlexStrength = taskObj.ImyGrade;
                            temp.ProductLineID = pl;
                            temp.SeasonType = formulaObj.SeasonType;
                            temp.SCRate = formulaObj.SCRate;
                            temp.FlexStrength = formulaObj.FlexStrength;
                            temp.IsSlurry = isSlurry;
                            temp.ImpGrade = formulaObj.ImpGrade;
                            temp.Remark = taskObj.Remark;
                            if (!base.GPSSwitch("IsAutoAuditForConsMixprop"))
                            {
                                temp.AuditStatus = 0;
                                temp.AuditTime = null;
                                temp.Auditor = "";
                            }
                            temp.MixingTime = formulaObj.MixingTime;
                            ICons.Update(temp, null);


                            IList<ConsMixpropItem> citems = IConItems.Query().Where(m => m.ConsMixpropID == temp.ID).ToList();
                            foreach (ConsMixpropItem iitem in citems)
                            {
                                IConItems.Delete(iitem);
                            }
                            //temp.Weight = 0;
                            cm = temp;
                            //先提交删除，否则计算容重不正确
                            this.m_UnitOfWork.Flush();
                        }

                        else
                        {

                            //3. 先通过任务单信息+理论配比信息生成施工配比主表                            
                            ConsMixprop temp = new ConsMixprop();
                            temp.FormulaID = formulaid;
                            temp.TaskID = taskObj.ID;
                            temp.ConStrength = formulaObj.ConStrength;
                            temp.FlexStrength = taskObj.ImyGrade;
                            temp.ProductLineID = pl;
                            temp.SeasonType = formulaObj.SeasonType;
                            temp.SCRate = formulaObj.SCRate;
                            temp.FlexStrength = formulaObj.FlexStrength;
                            temp.IsSlurry = isSlurry;
                            temp.ImpGrade = formulaObj.ImpGrade;
                            temp.Remark = taskObj.Remark;
                            temp.MixingTime = formulaObj.MixingTime;
                            //temp.Weight = 0;
                            //检查是否自动审核
                            ConsMixpropService consMixpropservice = new ConsMixpropService(this.m_UnitOfWork);
                            consMixpropservice.CheckIsAutoAudit(temp);
                            ICons.Add(temp);
                            cm = temp;
                        }

                        decimal FAamount = 0;
                        decimal CAamount = 0;
                        decimal CA1amount = 0;
                        decimal WAamount = 0;

                        for (int i = 0; i < itemids.Length; i++)
                        {
                            int id = itemids[i];
                            decimal amount = amounts[i];

                            FormulaItem fItem = formulaItems.FirstOrDefault(f => f.ID == id);

                            if (fItem.StuffTypeID.Contains("FA"))
                            {
                                FAamount = amount;
                            }
                            else if (fItem.StuffTypeID.ToString()=="CA")
                            {
                                CAamount = amount;
                            }
                            else if (fItem.StuffTypeID.Contains("CA1"))
                            {
                                CA1amount = amount;
                            }
                            else if (fItem.StuffTypeID.Contains("WA"))
                            {
                                WAamount = amount;
                            }
                        }

                        //4. 在通过提交的细表生成施工配比细表
                        for (int i = 0; i < itemids.Length; i++)
                        {
                            int id = itemids[i];
                            decimal amount = amounts[i];

                            FormulaItem fItem = formulaItems.FirstOrDefault(f => f.ID == id);


                            foreach (SiloProductLine sp in silos)
                            {
                                if (sp.Silo.StuffInfo.StuffType.ID == fItem.StuffTypeID)
                                {
                                    if (sp.Silo.StuffInfo.StuffType.ID.Contains("CE"))
                                    {
                                        if (formulaObj.CementBreed.Trim() != sp.Silo.StuffInfo.Spec.Trim())
                                        {
                                            amount = 0;
                                        }

                                        else
                                        {
                                            amount = amounts[i];
                                        }
                                    }
                                    
                                    //砂用量=标准用量/(1-砂含水)*(1-砂含石)
                                    if (sp.Silo.StuffInfo.StuffType.ID.Contains("FA"))
                                    {
                                        amount = FAamount / ((1 - siwrate) * (1 - sirrate));
                                    }

                                    //大石用量=标准用量/(1-大石含水率)
                                    else if (sp.Silo.StuffInfo.StuffType.ID.ToString() == "CA")
                                    {
                                        amount = CAamount / (1 - riwrate1);
                                        //amount = (CAamount - FAamount * sirrate / ((1 - siwrate) * (1 - sirrate))) / (1 - riwrate);
                                    }

                                    //最终砂用量*砂含石+最终小石用量*(1-小石含水)=标准用量
                                    //最终小石用量=(标准用量-最终砂用量*砂含石)/(1-小石含水)
                                    else if (sp.Silo.StuffInfo.StuffType.ID.ToString() == "CA1")
                                    {
                                        amount = (CA1amount - FAamount * sirrate / ((1 - siwrate) * (1 - sirrate))) / (1 - riwrate);
                                    }

                                    //最终水用量+砂含水*砂用量+石含水*石用量=标准用量
                                    //最终水用量=标准用量-砂含水*砂用量-小石含水*小石用量-大石含水*大石用量
                                    else if (sp.Silo.StuffInfo.StuffType.ID.Contains("WA"))
                                    {
                                        amount = WAamount 
                                            - (FAamount * siwrate) / ((1 - siwrate) * (1 - sirrate))
                                            - CAamount * riwrate1 / (1 - riwrate1)
                                            - ((CA1amount - FAamount * sirrate / ((1 - siwrate) * (1 - sirrate))) / (1 - riwrate)) * riwrate;
                                    }
                                    if (amount < 0)
                                    {
                                        throw new Exception("参数填写不正确，导致材料用量小于0，请核实");
                                    }
                                    ConsMixpropItem item = new ConsMixpropItem();

                                    item.ConsMixpropID = cm.ID;

                                    item.SiloID = sp.Silo.ID;
                                    decimal iamount = amount * (decimal)sp.Rate;
                                    item.Amount = iamount;
                                    int ordernum = sp.OrderNum;
                                    Type cmType = cm.GetType();
                                    if (null != cmType.GetProperty(string.Format("S{0}_wet", ordernum).ToString()))
                                    {
                                        cmType.GetProperty(string.Format("S{0}_wet", ordernum).ToString()).SetValue(cm, iamount, null);
                                    }

                                    IConItems.Add(item);

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
         * */

        /// <summary>
        /// 通过标准配比用量生成施工配比
        /// </summary>
        /// <param name="taskid">任务单编号</param>
        /// <param name="formulaid">理论配比ID</param>
        /// <param name="isSlurry">是否为砂浆？</param>
        /// <returns>操作是否成功？</returns>
        public List<ConsMixprop> GenConsMix(string taskid, string formulaid, bool isSlurry, string[] plist)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {

                    var DispatchLists = this.m_UnitOfWork.GetRepositoryBase<DispatchList>().Query().Where(p => (p.TaskID == taskid && p.IsRunning == true && p.IsCompleted == false)).ToList();

                    if (DispatchLists.Count > 0)
                    {
                        logger.Error("正在生产的任务单不允许修改配比");
                        throw new Exception("正在生产的任务单不允许修改配比");

                    }

                    ProduceTask taskObj = this.Get(taskid);

                    IConsMixpropRepository ICons = this.m_UnitOfWork.ConsMixpropRepository;
                    IRepositoryBase<Formula> IFormula = this.m_UnitOfWork.GetRepositoryBase<Formula>();
                    IConsMixpropItemRepository IConItems = this.m_UnitOfWork.ConsMixpropItemRepository;
                    Formula formulaObj = IFormula.Get(formulaid);
                    IList<FormulaItem> formulaItems = this.m_UnitOfWork.GetRepositoryBase<FormulaItem>().All(string.Format("FormulaID = '{0}'", formulaid), "ID", false);
                    IList<ProductLine> productLines = this.m_UnitOfWork.GetRepositoryBase<ProductLine>().All();
                    List<ConsMixprop> tmplist = new List<ConsMixprop>();
                     
                    if (isSlurry)
                    {
                        taskObj.IsSlurry = true;
                        taskObj.SlurryFormula = formulaid;

                        //下发了混凝土配方的任务单不能同时下发砼标号砂浆

                        if (!string.IsNullOrEmpty(taskObj.BetonFormula) && formulaObj.FormulaType == "FType_CS")
                        {
                            throw new Exception("已经下发了混凝土配方的任务单不能下发砼标号砂浆");
                        }
                    }
                    else
                    {
                        
                        taskObj.BetonFormula = formulaid;
                        //下发了砼标号砂浆配方的任务单不能同时下发混凝土配方

                        if (!string.IsNullOrEmpty(taskObj.SlurryFormula) && formulaObj.FormulaType == "FType_CS")
                        {
                            throw new Exception("已经下发了砼标号砂浆的任务单不能下发混凝土");
                        }
                    }
                    base.Update(taskObj, null);
                    foreach (string pl in plist)
                    {
                        ProductLine pline = productLines.FirstOrDefault(p => p.ID == pl);
                        IList<SiloProductLine> silos = pline.SiloProductLines;

                        //1. 该任务单对应的施工配比是否存在？，
                        IList<ConsMixprop> list = ICons.Query()
                            .Where(c => c.ProduceTask.ID == taskid && c.ProductLineID == pline.ID && c.IsSlurry == isSlurry)
                            .ToList();

                        ConsMixprop cm = new ConsMixprop();
                        //2. 存在则找出更新
                        if (list.Count > 0)
                        {
                            ConsMixprop temp = list[0];
                            temp.FormulaID = formulaid;
                            temp.Formula = IFormula.Get(formulaid);
                            temp.Weight = temp.Formula.TuneWeight;
                            temp.TaskID = taskObj.ID;
                            temp.ConStrength = formulaObj.ConStrength;
                            temp.FlexStrength = formulaObj.FlexStrength;
                            temp.ProductLineID = pline.ID;
                            temp.SeasonType = formulaObj.SeasonType;
                            temp.SCRate = formulaObj.SCRate;
                            temp.FlexStrength = formulaObj.FlexStrength;
                            temp.IsSlurry = isSlurry;
                            temp.ImpGrade = taskObj.ImpGrade;
                            temp.Remark = taskObj.Remark;
                            temp.MixingTime = formulaObj.MixingTime;
                            temp.SynStatus = 0; //重发配比相当于更新，因此修改状态需要改变，技术员需要确认才能发送
                            
                            if (!base.GPSSwitch("IsAutoAuditForConsMixprop"))
                            {
                                temp.AuditStatus = 0;
                                temp.AuditTime = null;
                                temp.Auditor = "";
                            }
                            temp.MixingTime = formulaObj.MixingTime;
                            ICons.Update(temp, null);


                            IList<ConsMixpropItem> citems = IConItems.Query().Where(m => m.ConsMixpropID == temp.ID).ToList();


                            string is2012 = "";
                            Synmonitor synTable = null;
                            PublicService op = null;
                            foreach (ConsMixpropItem iitem in citems)
                            {
                                IConItems.Delete(iitem);

                                //针对2012工控添加同步 同步删除
                                is2012 = ConfigurationManager.AppSettings["Is2012"];
                                if (is2012 == "true")
                                {
                                    //是2012工控则插入同步表
                                    synTable = new Synmonitor();
                                    synTable.tb_name = "view_ConsMixpropItems";
                                    synTable.tb_action = "DELETE";
                                    synTable.key_name = "ConsMixpropID";
                                    synTable.key_value = iitem.ID.ToString();
                                    synTable.key_type = "0";
                                    synTable.action_time = DateTime.Now;
                                    synTable.ProductLineID = iitem.ConsMixprop.ProductLineID;
                                    if (op == null)
                                        op = new PublicService();
                                    op.Synmonitor.Add(synTable);
                                }
                            }
                            //temp.Weight = 0;
                            cm = temp;
                            tmplist.Add(cm);                            
                            //先提交删除，否则计算容重不正确
                            this.m_UnitOfWork.Flush();
                        }

                        else
                        {

                            //3. 先通过任务单信息+理论配比信息生成施工配比主表                            
                            ConsMixprop temp = new ConsMixprop();
                            temp.FormulaID = formulaid;
                            temp.Formula = IFormula.Get(formulaid);
                            temp.Weight = temp.Formula.TuneWeight;
                            temp.TaskID = taskObj.ID;
                            temp.ConStrength = formulaObj.ConStrength;
                            temp.FlexStrength = formulaObj.FlexStrength;
                            temp.ProductLineID = pline.ID;
                            temp.SeasonType = formulaObj.SeasonType;
                            temp.SCRate = formulaObj.SCRate;
                            temp.FlexStrength = formulaObj.FlexStrength;
                            temp.IsSlurry = isSlurry;
                            temp.ImpGrade = taskObj.ImpGrade;
                            temp.Remark = taskObj.Remark;
                            temp.MixingTime = formulaObj.MixingTime;
                            temp.Auditor = AuthorizationService.CurrentUserID;
                            //temp.Weight = 0;
                            //检查是否自动审核
                            ConsMixpropService consMixpropservice = new ConsMixpropService(this.m_UnitOfWork);
                            consMixpropservice.CheckIsAutoAudit(temp);
                            ICons.Add(temp);
                            cm = temp;
                            tmplist.Add(cm);
                        }


                        foreach (SiloProductLine sp in silos)
                        {
                            int num = 0;
                            bool flag = false;

                            foreach (FormulaItem fi in formulaItems)
                            {
                                num++;
                                decimal amount = fi.StuffAmount;
                                //fix:可能由于桶仓没指定材料报错（对接12版发现）
                                //date:2013/01/10 
                                //by: Sky
                                if (sp.Silo != null && sp.Silo.StuffInfo != null)
                                {
                                    if (sp.Silo.StuffInfo.StuffType != null && (sp.Silo.StuffInfo.StuffType.ID == fi.StuffTypeID || ((num == formulaItems.Count) && !flag)))
                                    {
                                        if (sp.Silo.StuffInfo.StuffType.ID == fi.StuffTypeID)
                                        {
                                            flag = true;
                                            if (sp.Silo.StuffInfo.StuffType.ID.Contains("CE"))
                                            {
                                                if (sp.Silo.StuffInfo.Spec == null) {
                                                    throw new ApplicationException(string.Format("材料[{0}]未指定材料品种", sp.Silo.StuffInfo.StuffName));
                                                }
                                                if (formulaObj.CementBreed.Trim() != sp.Silo.StuffInfo.Spec.Trim())
                                                {
                                                    amount = 0;
                                                }

                                                else
                                                {
                                                    amount = fi.StuffAmount;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            amount = 0;
                                        }

                                        ConsMixpropItem item = new ConsMixpropItem();

                                        item.ConsMixpropID = cm.ID;

                                        item.SiloID = sp.Silo.ID;
                                        decimal iamount = amount * (decimal)sp.Rate;
                                        item.Amount = iamount;
                                        int ordernum = sp.OrderNum;
                                        Type cmType = cm.GetType();
                                        if (null != cmType.GetProperty(string.Format("S{0}_wet", ordernum).ToString()))
                                        {
                                            cmType.GetProperty(string.Format("S{0}_wet", ordernum).ToString()).SetValue(cm, iamount, null);
                                        }

                                        IConItems.Add(item);

                                    }
                                }
                                else //如果筒仓异常，则抛出异常，提示用户修正
                                {
                                    throw new Exception(pline.ProductLineName + "筒仓：" + sp.Silo.ID +"【"+sp.Silo.SiloName+"】"+ "配置异常，请检查！");
                                }
                            }
                        }

                    }
                    tx.Commit();
                    return tmplist;
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
        /// 保存任务单Identities
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="identities"></param>
        public void SaveTaskIdentities(string taskId, int[] identities)
        {
            var task = this.Get(taskId);
            if (task != null)
            {
                using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
                {
                    task.TaskIdentities.Clear();
                    if (identities != null)
                    {
                        var setts = this.m_UnitOfWork.GetRepositoryBase<IdentitySetting>().Query()
                            .Where(p => identities.Contains(p.ID??0))
                            .ToList(); 
                        task.TaskIdentities = setts;
                    }
                    try
                    {
                        base.Update(task, null);
                        tx.Commit();
                    }
                    catch{
                        tx.Rollback(); 
                        throw;
                    }
                }
            }

        }
        /// <summary>
        /// 获取交班时间可调度的任务单
        /// </summary>
        /// <param name="request"></param> 
        /// <returns></returns>
        public IList<ProduceTask> GetRangeTimeTasks(JqGridRequest request)
        {
            string startDate;
            string endDate;
            
            SysConfigService svr = new SysConfigService(this.m_UnitOfWork);
            svr.GetTodayDateRange(out startDate, out endDate);
            string condition = "AuditStatus = 1 AND IsCompleted = 0 AND FormulaStatus > 0 AND NeedDate>='" + startDate + "' AND NeedDate < '" + endDate + "'";
            
            return this.Find(request, condition);
        }

        /// <summary>
        /// 获取非交班时间的任务单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IList<ProduceTask> GetNotRangeTimeTasks(JqGridRequest request)
        {
            string startDate;
            string endDate;

            SysConfigService svr = new SysConfigService(this.m_UnitOfWork);
            svr.GetTodayDateRange(out startDate, out endDate);
            string condition = "AuditStatus = 1 AND IsCompleted = 0 AND FormulaStatus > 0 AND (NeedDate<'" + startDate + "' OR NeedDate>='" + endDate + "')";

            return this.Find(request, condition);
        }

        public List<OpenCheck> AutoOpenCheck(string[] ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    List<OpenCheck> list = new List<OpenCheck>();
                    //IList<StuffExam> examlist=this.m_UnitOfWork.GetRepositoryBase<StuffExam>().Query().Where(m=>m.IsUsed).OrderByDescending(m=>m.BuildTime).ToList();
                    IRepositoryBase<StuffExam> stuffExam= this.m_UnitOfWork.GetRepositoryBase<StuffExam>();
                    foreach (string id in ids)
                    {
                        ProduceTask task = this.Get(id);
                        if (task.IsOut)
                        {
                            throw new Exception("该任务单已经开盘，请在试验管理->开盘鉴定查看");
                        }
                        else if (task.CustMixpropID == null || task.CustMixpropID.Length == 0)
                        {
                            throw new Exception("当前无法进行开盘鉴定，请先选择并保存客户配比号再操作");
                        }
                        else
                        {
                            CustMixprop cm = this.m_UnitOfWork.GetRepositoryBase<CustMixprop>().Get(task.CustMixpropID);
                            IList<CustMixpropItem> cmlist = cm.CustMixpropItems;
                            OpenCheck temp = new OpenCheck();
                            //temp.ID = id;
                            temp.TaskID = id;
                            temp.CustMixpropID = task.CustMixpropID;
                            temp.ConStrength = task.ConStrength;
                            temp.DeSlump = task.Slump;
                            temp.ReSlump = task.Slump;
                            temp.OpenTime = task.OpenTime == null ? DateTime.Now : task.OpenTime;
                            temp.WCRate = cm.WCRate;
                            temp.Slump = cm.Slump;
                            temp.SCRate = cm.SCRate;
                            temp.RIWRate = task.RIWRate;
                            temp.SIRRate = task.SIRRate;
                            temp.SIWRate = task.SIWRate;
                            temp.RIWRate1 = task.RIWRate1;
                            foreach (CustMixpropItem item in cmlist)
                            {
                                switch (item.StuffInfo.StuffType.ID)
                                {
                                    case "WA":
                                        temp.WAAmount = item.Amount;
                                        temp.WAExamID = stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "WA").Count() == 0 ?
                                                null : stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "WA").OrderByDescending(m => m.BuildTime).First().ID;
                                        temp.WAStuffID = item.StuffInfo.StuffName;
                                        break;
                                    case "CE":
                                        if (item.Amount > 0)
                                        {
                                            temp.CEAmount = item.Amount;
                                            temp.CEStuffID = item.StuffInfo.StuffName;
                                            temp.CESupplyID = item.StuffInfo.SupplyInfo.SupplyName;
                                            temp.CEExamID = stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "CE").Count() == 0 ?
                                                null : stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "CE").OrderByDescending(m => m.BuildTime).First().ID;
                                            temp.CESupplyID = stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "CE").Count() == 0 ?
                                                null : stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "CE").OrderByDescending(m => m.BuildTime).First().SupplyID;
                                        }
                                        break;

                                    case "CE1":
                                        if (item.Amount > 0)
                                        {
                                            temp.CEAmount = item.Amount;
                                            temp.CEStuffID = item.StuffInfo.StuffName;
                                            temp.CESupplyID = item.StuffInfo.SupplyInfo.SupplyName;
                                            temp.CEExamID = stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "CE1").Count() == 0 ?
                                                null : stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "CE1").OrderByDescending(m => m.BuildTime).First().ID;
                                            temp.CESupplyID = stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "CE1").Count() == 0 ?
                                                null : stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "CE1").OrderByDescending(m => m.BuildTime).First().SupplyID;
                                        }
                                        break;
                                    case "CA":
                                        temp.CAAmount = item.Amount;
                                        temp.CAStuffID = item.Amount > 0 ? item.StuffInfo.StuffName : "——";
                                        temp.CASupplyID = item.Amount > 0 ? item.StuffInfo.SupplyInfo.SupplyName : null;
                                        temp.CAExamID = (item.Amount > 0 && stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "CA").Count() > 0) ?
                                            stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "CA").OrderByDescending(m => m.BuildTime).First().ID : "——";
                                        temp.CASupplyID = (item.Amount > 0 && stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "CA").Count() > 0) ?
                                           stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "CA").OrderByDescending(m => m.BuildTime).First().SupplyID : "——";
                                        break;
                                    case "CA1":
                                        temp.CA1Amount = item.Amount;
                                        temp.CA1StuffID = item.Amount > 0 ? item.StuffInfo.StuffName : "——";
                                        temp.CA1SupplyID = item.Amount > 0 ? item.StuffInfo.SupplyInfo.SupplyName : null;
                                        temp.CA1ExamID = (item.Amount > 0 && stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "CA1").Count() > 0) ?
                                            stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "CA1").OrderByDescending(m => m.BuildTime).First().ID : "——";
                                        temp.CA1SupplyID = (item.Amount > 0 && stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "CA1").Count() > 0) ?
                                           stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "CA1").OrderByDescending(m => m.BuildTime).First().SupplyID : "——";
                                        break;
                                    case "FA":
                                        temp.FAAmount = item.Amount;
                                        temp.FAStuffID = item.StuffInfo.StuffName;
                                        temp.FASupplyID = item.StuffInfo.SupplyInfo.SupplyName;
                                        temp.FAExamID = stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "FA").Count() == 0 ?
                                               "——" : stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "FA").OrderByDescending(m => m.BuildTime).First().ID;
                                        temp.FASupplyID = stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "FA").Count() == 0 ?
                                               "——" : stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "FA").OrderByDescending(m => m.BuildTime).First().SupplyID;
                                        break;
                                    case "AIR1":
                                        temp.AIR1Amount = item.Amount;
                                        if (item.Amount > 0)
                                        {
                                            temp.AIR1StuffID = item.StuffInfo.StuffName;
                                            temp.AIR1SupplyID = item.StuffInfo.SupplyInfo.SupplyName;
                                            temp.AIR1ExamID = stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "AIR1").Count() == 0 ?
                                                "——" : stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "AIR1").OrderByDescending(m => m.BuildTime).First().ID;
                                            temp.AIR1SupplyID = stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "AIR1").Count() == 0 ?
                                                "——" : stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "AIR1").OrderByDescending(m => m.BuildTime).First().SupplyID;
                                        }
                                        break;
                                    case "AIR2":
                                        temp.AIR2Amount = item.Amount;
                                        if (item.Amount > 0)
                                        {
                                            temp.AIR2StuffID = item.StuffInfo.StuffName;
                                            temp.AIR2SupplyID = item.StuffInfo.SupplyInfo.SupplyName;
                                            temp.AIR2ExamID = stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "AIR2").Count() == 0 ?
                                                "——" : stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "AIR2").OrderByDescending(m => m.BuildTime).First().ID;
                                            temp.AIR2SupplyID = stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "AIR2").Count() == 0 ?
                                                "——" : stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "AIR2").OrderByDescending(m => m.BuildTime).First().SupplyID;
                                        }
                                        break;
                                    case "ADM1":
                                        temp.ADM1Amount = item.Amount;
                                        if (item.Amount > 0)
                                        {
                                            temp.ADM1StuffID = item.StuffInfo.StuffName;
                                            temp.ADM1SupplyID = item.StuffInfo.SupplyInfo.SupplyName;
                                            temp.ADM1ExamID = stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "ADM1").Count() == 0 ?
                                                "——" : stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "ADM1").OrderByDescending(m => m.BuildTime).First().ID;
                                            temp.ADM1SupplyID = stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "ADM1").Count() == 0 ?
                                                "——" : stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "ADM1").OrderByDescending(m => m.BuildTime).First().SupplyID;
                                        }
                                        break;
                                    case "ADM2":
                                        if (item.Amount > 0)
                                        {
                                            temp.ADM2Amount = item.Amount;
                                            temp.ADM2StuffID = item.StuffInfo.StuffName;
                                            temp.ADM2SupplyID = item.StuffInfo.SupplyInfo.SupplyName;
                                            temp.ADM2ExamID = stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "ADM2").Count() == 0 ?
                                                "——" : stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "ADM2").OrderByDescending(m => m.BuildTime).First().ID;
                                            temp.ADM2SupplyID = stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "ADM2").Count() == 0 ?
                                                "——" : stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "ADM2").OrderByDescending(m => m.BuildTime).First().SupplyID;
                                        }
                                        break;
                                    case "ADM3":
                                        temp.ADM3Amount = item.Amount;
                                        if (item.Amount > 0)
                                        {
                                            temp.ADM3StuffID = item.StuffInfo.StuffName;
                                            temp.ADM3SupplyID = item.StuffInfo.SupplyInfo.SupplyName;
                                            temp.ADM3ExamID = stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "ADM3").Count() == 0 ?
                                                "——" : stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "ADM3").OrderByDescending(m => m.BuildTime).First().ID;
                                            temp.ADM3SupplyID = stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "ADM3").Count() == 0 ?
                                                "——" : stuffExam.Query().Where(m => m.IsUsed && m.StuffInfo.StuffTypeID == "ADM3").OrderByDescending(m => m.BuildTime).First().SupplyID;
                                        }
                                        break;
                                }
                            }
                            this.m_UnitOfWork.GetRepositoryBase<OpenCheck>().Add(temp);
                            this.m_UnitOfWork.Flush();
                            list.Add(temp);
                            task.IsOut = true;
                            base.Update(task, null);
                        }
                    }
                    tx.Commit();
                    return list;
                }
                catch(Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    throw;
                }
            }
        }

        public bool SetComplete(string[] taskIDs, bool isCompleted = true)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    ProduceTask task = null;
                    foreach (string taskID in taskIDs)
                    {
                        task = this.Get(taskID);
                        task.IsCompleted = isCompleted;
                        task.CompleteDate = DateTime.Now;
                        base.Update(task, null);

                        IList<ConsMixprop> list = task.ConsMixprops;
                        foreach (ConsMixprop cons in list)
                        {
                            cons.IsCompleted = isCompleted;
                            this.m_UnitOfWork.ConsMixpropRepository.Update(cons, null);
                        }
                    }
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="task"></param>
        public void Audit(ProduceTask task)
        {
            try
            {
                ProduceTask ptask = this.Get(task.ID);
                string auditor = AuthorizationService.CurrentUserID;
                ptask.AuditStatus = task.AuditStatus;
                ptask.AuditInfo = task.AuditInfo;
                ptask.Auditor = auditor;
                ptask.AuditTime = DateTime.Now;
                base.Update(ptask, null);
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }
        }
        /// 取消审核
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="auditStatus"></param>
        public void UnAudit(string taskID, int auditStatus)
        {
            try
            {
                ProduceTask task = this.Get(taskID);
                task.AuditStatus = auditStatus;
                task.Auditor = AuthorizationService.CurrentUserID;
                task.AuditTime = DateTime.Now;
                this.m_UnitOfWork.GetRepositoryBase<ProduceTask>().Update(task, null);
                this.m_UnitOfWork.Flush();
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }
        }

        public void PromptTodayTasks()
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    IList<ProducePlan> list = this.m_UnitOfWork.GetRepositoryBase<ProducePlan>().Query().Where(m => m.PlanDate.Value.Date == DateTime.Now.Date).ToList();
                    foreach (ProducePlan obj in list)
                    {
                        ProduceTask task = obj.ProduceTask;
                        task.NeedDate = DateTime.Now;
                        base.Update(task, null);
                    }
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        public ProduceTask CopyTask(string id)
        {

            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    ProduceTask task = this.m_UnitOfWork.GetRepositoryBase<ProduceTask>().Get(id);
                    ProduceTask tmp = new ProduceTask();
                    Type t = task.GetType();
                    PropertyInfo[] pros = t.GetProperties();
                    foreach (PropertyInfo pro in pros)
                    {
                        if (pro.Name == "ContractID" || pro.Name == "ContractItemsID" || pro.Name == "ConStrength" || pro.Name == "ProjectID" || pro.Name == "ProjectAddr"
                            || pro.Name == "ProjectName" || pro.Name == "ConstructUnit" || pro.Name == "BuildUnit" || pro.Name == "SupplyUnit" || pro.Name == "CastMode"
                            || pro.Name == "PumpType" || pro.Name == "Slump" || pro.Name == "NeedDate" || pro.Name == "PlanCube" || pro.Name == "NeedTimes" || pro.Name == "TaskType"
                            || pro.Name == "ConsPosType" || pro.Name == "ConsPos" || pro.Name == "CarpGrade" || pro.Name == "IsSlurry" || pro.Name == "BetonTag" || pro.Name == "CementType"
                            || pro.Name == "OtherDemand" || pro.Name == "RegionID" || pro.Name == "Remark")
                        {
                            var value = pro.GetValue(task, null);
                            if (value != null && value.ToString() != "")
                                pro.SetValue(tmp, value, null);
                        }
                    }
                    base.CheckIsAutoAudit(tmp);
                    tmp = this.m_UnitOfWork.GetRepositoryBase<ProduceTask>().Add(tmp);
                    tx.Commit();
                    return tmp;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    return null;
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool UpdateTodayPlan(string[] taskIDs)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    ProduceTask task = null;
                    ConsMixpropService cmService = new ConsMixpropService(this.m_UnitOfWork);
                    SysConfigService configService = new SysConfigService(this.m_UnitOfWork);
                    SysConfig config = configService.GetSysConfig("ConsmixpropReaudit");
                    bool ConsmixpropReaudit = false;
                    IList<ConsMixprop> cmList = null;
                    foreach (string taskID in taskIDs)
                    {
                        task = this.Get(taskID);
                        DateTime beforeNeedDate = task.NeedDate;
                        task.NeedDate = DateTime.Now;
                        string startDate;
                        string endDate;

                     
                        configService.GetTodayDateRange(out startDate, out endDate);

                        if (!(beforeNeedDate >= DateTime.Parse(startDate) && beforeNeedDate <= DateTime.Parse(endDate))
                            && !(task.NeedDate >= DateTime.Parse(startDate) && task.NeedDate <= DateTime.Parse(endDate))
                            && bool.TryParse(config.ConfigValue, out ConsmixpropReaudit) && ConsmixpropReaudit)
                        {


                            cmList = cmService.All("taskID='" + task.ID + "'", "ID", true);
                            foreach (ConsMixprop cm in cmList)
                            {
                                if (cm.AuditTime != null)
                                {
                                    if (!(Convert.ToDateTime(cm.AuditTime) >= DateTime.Parse(startDate) && Convert.ToDateTime(cm.AuditTime) <= DateTime.Parse(endDate)))
                                    {
                                        cm.AuditStatus = 0;
                                        cm.AuditInfo = null;
                                        cm.Auditor = null;
                                        cm.AuditTime = null;
                                        cmService.UpdateAuditStatus(cm);  //直接调用基类的更新，避免调用重载的方法来向控制系统发送指令
                                    }
                                }
                            }
                        }
                        base.Update(task, null);
                    }
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 更新任务单用砼时间
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="beforeNeedDate"></param>
        public void Update_new(ProduceTask entity, DateTime beforeNeedDate)
        {
            SysConfigService configService = new SysConfigService(this.m_UnitOfWork);
            SysConfig config = configService.GetSysConfig("ConsmixpropReaudit");
            bool ConsmixpropReaudit = false;

            string startDate;
            string endDate;

            configService.GetTodayDateRange(out startDate, out endDate);

            if ( !(beforeNeedDate >= DateTime.Parse(startDate)
                 && !(entity.NeedDate >= DateTime.Parse(startDate) && entity.NeedDate <= DateTime.Parse(endDate))
                && beforeNeedDate<=DateTime.Parse(endDate)) && bool.TryParse(config.ConfigValue, out ConsmixpropReaudit) && ConsmixpropReaudit)
            {
                ConsMixpropService cmService = new ConsMixpropService(this.m_UnitOfWork);
                IList<ConsMixprop> cmList = cmService.All("taskID='" + entity.ID + "'", "ID", true);
                foreach (ConsMixprop cm in cmList)
                {
                    if (cm.AuditTime != null)
                    {
                        if (!(Convert.ToDateTime(cm.AuditTime) >= DateTime.Parse(startDate) && Convert.ToDateTime(cm.AuditTime) <= DateTime.Parse(endDate)))
                        {
                            cm.AuditStatus = 0;
                            cm.AuditInfo = null;
                            cm.Auditor = null;
                            cm.AuditTime = null;
                            cmService.UpdateAuditStatus(cm);      //直接调用基类的更新，避免调用重载的方法来向控制系统发送指令
                        }
                    }
                }
            }
            base.Update(entity);
        }
    }
}

