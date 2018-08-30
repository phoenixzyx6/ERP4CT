using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Model.Enums;
using ZLERP.Resources;
using ZLERP.Gps;
using System.Threading;
using ZLERP.Business.ControlSystem;
namespace ZLERP.Business
{
    public class DispatchListService : ServiceBase<DispatchList>
    {
        ControlSystemHelper controlSystem;
        PublicService s;

        internal DispatchListService(IUnitOfWork uow)
            : base(uow)
        {
            controlSystem = new ControlSystemHelper();
        }

        /// <summary>
        /// 生产调度表单提交处理
        /// </summary>
        /// <param name="entity"></param>
        public SchedulerViewModel SaveScheduler(SchedulerViewModel entity)
        {
            DispatchList dispatch2 = null;
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    DispatchList dispatch = entity.DispatchList;
                    ShippingDocument document = entity.ShippingDocument;
                    //发货单保存前赋值
                    document.ProductLineID = dispatch.ProductLineID;
                    document.BetonCount = dispatch.BetonCount;
                    document.SlurryCount = dispatch.SlurryCount;
                    //-----科嘉个性化修改：砂浆方量不参与累计 徐毅力
                    //----- 1、单独打砂浆车次与方量均不累计
                    //----- 2、附带砂浆只有方量不累计，车次需要累计
                    /*
                     * XJM注释
                    if (document.Remark == "润泵砂浆" && document.SlurryCount > 0)
                    {
                        if (document.BetonCount == 0)
                        { //情况1
                            document.ProvidedCube -= document.SlurryCount;
                            document.ProvidedTimes -= 1;

                        }
                        else
                        {//情况2
                            document.ProvidedCube -= document.SlurryCount;
                        }
                    }
                     */
                    document.SlurryConsMixpropID = dispatch.SlurryFormula;
                    //保存发货单
                    document = this.SaveShippingDocument(document);
                    //生产调度数据赋值
                    dispatch.ShipDocID = document.ID;
                    dispatch.TaskID = document.TaskID;
                    //保存调度
                    dispatch2 = this.Add(dispatch);
                    
                    //重新赋值返回。
                    entity.ShippingDocument = document;
                    entity.DispatchList = dispatch2;

                    IRepositoryBase<ProduceTask> produceTaskRepository = this.m_UnitOfWork.GetRepositoryBase<ProduceTask>();
                    ProduceTask task = produceTaskRepository.Get(entity.DispatchList.TaskID);
                    if (task.OpenTime == null)
                    {
                        task.OpenTime = DateTime.Now;
                        produceTaskRepository.Update(task, null);
                    }
                    dispatch2.ProduceTask = task;
                    dispatch2.ShippingDocument = document;
                    //GPS接口
                    GPSHelper gpshelper = new GPSHelper(this.m_UnitOfWork);
                    gpshelper.GPSOperate("add", dispatch2);



                    tx.Commit();
                    
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message, ex);
                    throw ex;
                }
            }
            //直连不参与调度与发货单的事物操作
            try
            {
                //直连：添加生产登记到控制系统
                ResultInfo result = controlSystem.AddDispatch(dispatch2);
                if (!result.Result) {//分两种情况
                    if (result.ResultCode == 218)//1、218表示记录已存在，只需更新ERP的发送状态
                    {
                        dispatch2.SendStatus = 1;
                        base.Update(dispatch2, null);
                    }
                    else {
                        base.Delete(dispatch2);//2、控制系统中本身业务逻辑错误导致插入失败，ERP删除
                        throw new Exception(Convert.ToString(result.Message));
                    }
                }       
                else
                {//TCP添加成功，直接将发送中，改成发送成功。
                    dispatch2.SendStatus = 1;
                    base.Update(dispatch2);
                }  
            }
            catch (Exception)
            {
                //网络异常，将生产调度的发送状态改成失败，并显示重发按钮
                if (this.Get(dispatch2.ID) != null) {
                    dispatch2.SendStatus = 0;
                    base.Update(dispatch2,null);
                }
                throw;
            }
            
            return entity;
        }

        /// <summary>
        /// 保存发货单
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public ShippingDocument SaveShippingDocument(ShippingDocument document)
        {
            try
            {
                ProduceTask task = this.m_UnitOfWork.GetRepositoryBase<ProduceTask>()
                    .Query().Where(t => t.ID == document.TaskID).FirstOrDefault();
                if (task == null)
                {
                    throw new Exception("任务单信息获取失败!");
                }
                document.IsEffective = true;
                document.ContractID = task.ContractID;
                if (!string.IsNullOrEmpty(task.ContractID))
                {
                    document.ContractName = task.Contract.ContractName;
                    document.CustName = task.Contract.CustName;
                    document.CustomerID = task.Contract.CustomerID;
                }
                document.CustMixpropID = task.CustMixpropID;
                //获取机组信息
                ProductLine productLine = this.m_UnitOfWork.GetRepositoryBase<ProductLine>().Get(document.ProductLineID);
                if (productLine == null)
                {
                    throw new Exception("机组信息获取失败!");
                }
                document.ProductLineName = productLine.ProductLineName;
                //获取配比号
                var consMixPro = this.m_UnitOfWork.GetRepositoryBase<ConsMixprop>()
                    .Query()
                    .Where(c => c.ProductLineID == document.ProductLineID && c.TaskID == task.ID)
                    .ToList();
                var betonConsMix = consMixPro.Where(c => !c.IsSlurry).FirstOrDefault();
                var SlurryMix = consMixPro.Where(c => c.IsSlurry).FirstOrDefault();
                document.ConsMixpropID = betonConsMix == null ? string.Empty : betonConsMix.ID;
                document.FormulaName = betonConsMix == null ? string.Empty : betonConsMix.FormulaName;

                document.ProjectName = task.ProjectName;
                document.ProjectAddr = task.ProjectAddr;
                document.ProjectID = task.ProjectID;
                document.ShipDocType = "0";
                if (document.BetonCount == 0 && document.SlurryCount > 0)
                {
                    //xuejm:根据配置文件来确认单打砂浆的砼强度信息
                    SysConfigService configService = new SysConfigService(this.m_UnitOfWork);
                    SysConfig config = configService.GetSysConfig(SysConfigEnum.SlurryShipDoc);
                    if (config != null)
                    {
                        switch (config.ConfigValue)
                        {
                            case "0":
                                document.ConStrength = SlurryMix == null ? string.Empty : SlurryMix.ConStrength;//砼强度打印砂浆理论配比的强度，如“砂浆”
                                break;
                            case "1":
                                document.ConStrength = task.ConStrength + "砂浆";//砼强度打印“任务单砼强度+砂浆”，如“C30砂浆”
                                break;
                            case "2":
                                document.ConStrength = task.ConStrength;//砼强度仅打印“任务单砼强度”，如“C30P6”
                                break;
                        }
                    }

                }
                else
                {
                    document.ConStrength = task.ConStrength;
                }
                document.CastMode = task.CastMode;
                document.ConsPos = task.ConsPos;
                document.ImpGrade = task.ImpGrade;
                document.ImyGrade = task.ImyGrade;
                document.ImdGrade = task.ImdGrade;
                document.CarpRadii = task.CarpRadii;
                document.CementBreed = task.CementBreed;
                //document.Distance = 

                //调度方量 = 混凝土 + 砂浆
                document.SendCube = document.BetonCount + document.SlurryCount;
                //出票方量 = 调度方量 + 其它方量 + 剩余方量
                decimal? parCube = document.SendCube + document.OtherCube + (document.RemainCube ?? 0);
                document.ParCube = parCube ?? 0;
                document.ShippingCube = parCube ?? 0;
                document.SignInCube = document.ParCube;
                document.ScrapCube = 0;
                document.TransferCube = 0;
                if (document.BetonCount > 0 && betonConsMix != null)
                {
                    document.Surveyor = string.IsNullOrEmpty(betonConsMix.Modifier) ? betonConsMix.Builder : betonConsMix.Modifier;
                }
                else if (document.SlurryCount > 0 && document.BetonCount == 0 && SlurryMix != null)
                {
                    document.Surveyor = string.IsNullOrEmpty(SlurryMix.Modifier) ? SlurryMix.Builder : SlurryMix.Modifier;
                }
                document.ForkLift = task.ForkLift;
                document.PlanClass = task.PlanClass;
                document.ProduceDate = DateTime.Now;
                document.SupplyUnit = task.SupplyUnit;
                document.ConstructUnit = task.ConstructUnit;

                document.RealSlump = task.Slump;
                document.Signer = AuthorizationService.CurrentUserID;
                document.LinkMan = task.LinkMan;
                document.Tel = task.Tel;
                document.RegionID = task.RegionID;
                //是否代外生产.
                document.EntrustUnit = task.IsCommission ? task.SupplyUnit : "";

                if (document.BetonCount == 0 && document.SlurryCount > 0)
                {
                    if (SlurryMix != null)
                    {
                        document.FormulaName = SlurryMix.FormulaName;
                        //只打砂浆，配比号使用砂浆配比号
                        document.ConsMixpropID = SlurryMix.ID;
                    }
                }

                //操作员
                var record = this.m_UnitOfWork.GetRepositoryBase<ProductRecord>()
                     .Query()
                     .Where(p => p.ProductLineID == document.ProductLineID)
                     .OrderByDescending(p => p.BuildTime)
                     .FirstOrDefault();
                if (record != null)
                {
                    document.Operator = record.Builder;
                }
                var docRepository = this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>();
                document = docRepository.Add(document);
                string carId = document.CarID;
                CarService carService = new CarService(this.m_UnitOfWork);
                 /**********************************
                     天助城投特殊情况：先搅料后发车，因为车辆可以为空
                     修改：徐毅力 2012-11-8
                 **********************************/
                if (!string.IsNullOrEmpty(carId))
                {

                    carService.ChangeCarStatus(carId, CarStatus.ShippingCar);

                    //如果有剩余方量，则加入剩料记录
                    if (document.RemainCube > 0)
                    {
                        IRepositoryBase<TZRalation> TZRalationRespository = this.m_UnitOfWork.GetRepositoryBase<TZRalation>();
                        TZRalation obj = TZRalationRespository.Query()
                            .Where(p => p.CarID == carId && p.IsCompleted == false)
                            .FirstOrDefault();

                        if (obj == null)
                        {
                            ShippingDocument prevDoc = docRepository.Query()
                                .Where(d => d.CarID == carId && d.ID != document.ID && d.IsEffective)
                                .OrderByDescending(d => d.ID).ToList()
                                .FirstOrDefault();

                            if (prevDoc != null)
                            {
                                TZRalation tz = new TZRalation();
                                tz.SourceShipDocID = prevDoc.ID;
                                tz.TargetShipDocID = document.ID;
                                tz.SourceCube = document.RemainCube ?? 0;
                                tz.Cube = document.RemainCube ?? 0;
                                tz.CarID = carId;
                                tz.Driver = prevDoc.Driver;
                                tz.ReturnType = ZLERP.Model.Enums.ReturnType.ShengLiao;// "剩料转发";
                                tz.ActionType = ZLERP.Model.Enums.ActionType.Transfer;// "转发";

                                tz.IsAudit = false;
                                tz.IsCompleted = true;

                                tz.ActionTime = DateTime.Now;
                                tz.ActionCube = tz.Cube;

                                TZRalationRespository.Add(tz);

                                //添加历史记录 类型为暗扣
                                s = (s == null ? (new PublicService()) : s);
                                s.TZRalationHistory.AddHistory(tz, "add", tz.CarID, tz.Cube, "5");
                            }
                        }
                        else
                        {
                            IList<TZRalation> tzList = TZRalationRespository.Query().Where(m => m.CarID == obj.CarID && m.ActionTime == obj.ActionTime && m.IsCompleted == false).ToList();
                            foreach (TZRalation tz in tzList)
                            {
                                tz.TargetShipDocID = document.ID;
                                tz.IsCompleted = true;
                                TZRalationRespository.Update(tz, null);

                                //添加历史记录 类型为暗扣
                                s = (s == null ? (new PublicService()) : s);
                                s.TZRalationHistory.AddHistory(tz, "update", tz.CarID, tz.Cube, "5");
                            }
                        }
                    }

                    ////如果有剩余方量，则加入剩料记录
                    //if (document.RemainCube > 0)
                    //{
                    //    ShippingDocument prevDoc = docRepository.Query()
                    //        .Where(d => d.CarID == carId && d.ID != document.ID && d.IsEffective)
                    //        .OrderByDescending(d => d.ID).ToList()
                    //        .FirstOrDefault();
                    //    IRepositoryBase<TZRalation> TZRalationRespository=this.m_UnitOfWork.GetRepositoryBase<TZRalation>();
                    //    if (prevDoc != null)
                    //    {

                    //        TZRalation obj = TZRalationRespository.Query()
                    //           .Where(p => p.CarID == carId && p.IsCompleted == false && p.SourceShipDocID == prevDoc.ID && p.ActionType == "AT1")
                    //           .FirstOrDefault();
                    //        if (obj == null)
                    //        {
                    //            TZRalation tz = new TZRalation();
                    //            tz.SourceShipDocID = prevDoc.ID;
                    //            tz.TargetShipDocID = document.ID;
                    //            tz.Cube = document.RemainCube ?? 0;
                    //            tz.CarID = carId;
                    //            tz.Driver = prevDoc.Driver;
                    //            tz.ReturnType = ZLERP.Model.Enums.ReturnType.ShengLiao;// "剩料转发";
                    //            tz.ActionType = ZLERP.Model.Enums.ActionType.Transfer;// "转发";

                    //            tz.IsAudit = false;
                    //            tz.IsCompleted = true;
                    //            TZRalationRespository.Add(tz);
                    //        }
                    //        else
                    //        {
                    //            obj.TargetShipDocID = document.ID;
                    //            obj.IsCompleted = true;
                    //            TZRalationRespository.Update(obj);
                    //        }
                    //    }
                    //}
                }
                return document;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }


        /// <summary>
        /// 保存生产调度
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override DispatchList Add(DispatchList entity)
        {
            this.CalculateProduce(entity, true);
            if (entity.BetonCount == 0 && entity.SlurryCount == 0)
            {
                entity.IsCompleted = true;
            }
            //增加剩余盘数 2013-09-23
            entity.Field4 = ((entity.BTotalPot ?? 0) + (entity.STotalPot ?? 0)).ToString();
            return base.Add(entity);
        }



        /// <summary>
        /// 生产调度计算方法，包括罐次分配、生产方量处理。
        /// </summary>
        /// <param name="entity"></param>
        public void CalculateProduce(DispatchList entity, bool changeOrder)
        {
            entity.IsRunning = false;
            entity.IsCompleted = false;
            entity.OneCube = 0;
            entity.OnePCRate = 0;
            entity.TwoCube = 0;
            entity.TwoPCRate = 0;
            entity.BTotalPot = 0;
            entity.BNextPot = 0;
            entity.OneSlurryCube = 0;
            entity.OneSlurryPCRate = 0;
            entity.TwoSlurryCube = 0;
            entity.TwoSlurryPCRate = 0;
            entity.STotalPot = 0;
            entity.SNextPot = 0;
            //打砂浆只允许平均分配
            if (entity.SlurryCount > 0)
            {
                entity.IsAverage = true;
            }
            //顺序计算。
            if (changeOrder)
            {
                int maxOrder = this.Query()
                    .Where(d => d.IsCompleted == false)
                    .Max(d => d.DispatchOrder) ?? 0;

                entity.DispatchOrder = maxOrder + 1;
            }
            //混凝土和砂浆配比一次获取
            ConsMixpropService cmService = new ConsMixpropService(this.m_UnitOfWork);
            var mixProps = cmService.Query().Where(c => c.TaskID == entity.TaskID &&
                c.ProductLineID == entity.ProductLineID)
                .ToList();

            //获取砂浆配比
            ConsMixprop slurryCm = mixProps.Where(p => p.IsSlurry).FirstOrDefault();
            //实际总生产方量
            entity.ProduceCube = entity.BetonCount + entity.SlurryCount;
            
            if (entity.BetonCount == 0 && entity.SlurryCount > 0)
            {
                ProductLineService plService = new ProductLineService(this.m_UnitOfWork);
                ProductLine productLine = plService.Get(entity.ProductLineID);
                if (productLine.KZVersion == "12")
                {
                    /*12版楼站可以单独打砂浆，不需要将砂浆做混凝土打，只需提供单独砂浆方量与砂浆配比
                     * 徐毅力 2013-03-28
                    //entity.BetonCount = entity.SlurryCount;
                    //entity.SlurryCount = 0;
                    //entity.OneCube = entity.ProduceCube;
                     * */
                    //查找 任务单对应生产线上的砂浆配比
                    if (slurryCm == null)
                    {
                        throw new Exception("没有找到砂浆配比，无法生产!");
                    }
                    //配比是否已审核
                    else if (slurryCm.AuditStatus == 0 || slurryCm.SynStatus == 0)
                    {
                        throw new Exception("砂浆配比未审核或修改未发送至搅拌楼，无法生产，请联系实验室!");
                    }
                    //entity.BetonFormula = slurryCm.ID;
                entity.BetonFormula = mixProps.Where(p => p.IsSlurry == false).FirstOrDefault() == null ? slurryCm.ID : mixProps.Where(p => p.IsSlurry == false).FirstOrDefault().ID;//打砂浆时，12版楼站要求必须给混凝土配比号，若没有混凝土配比，则用砂浆配比号给混凝土配比号赋值
                    entity.SlurryFormula = slurryCm.ID;
                    //做混凝土打必为false,控制系统status = 0
                    //entity.IsSlurry = false;
                    this.Average(entity);
                }
                else
                {
                    entity.BetonCount = entity.SlurryCount;
                    entity.SlurryCount = 0;
                    entity.OneCube = entity.ProduceCube;
                    //查找 任务单对应生产线上的砂浆配比
                    if (slurryCm == null)
                    {
                        throw new Exception("没有找到砂浆配比，无法生产!");
                    }
                    //配比是否已审核
                    else if (slurryCm.AuditStatus == 0 || slurryCm.SynStatus == 0)
                    {
                        throw new Exception("砂浆配比未审核或修改未发送至搅拌楼，无法生产，请联系实验室!");
                    }
                    entity.BetonFormula = slurryCm.ID;
                    entity.SlurryFormula = string.Empty;
                    //做混凝土打必为false,控制系统status = 0
                    entity.IsSlurry = false;
                    this.Average(entity);
                }
            }
            else
            {
                //获取混凝土配比
                ConsMixprop betonCm = mixProps.Where(p => p.IsSlurry == false).FirstOrDefault();
                //ConsMixprop betonCm = this.m_UnitOfWork.GetRepositoryBase<ConsMixprop>()
                //.Query().Where(c => c.TaskID == entity.TaskID &&
                //c.ProductLineID == entity.ProductLineID && !c.IsSlurry).FirstOrDefault();
                if (betonCm == null)
                {
                    throw new Exception("没有找到此任务单的混凝土配比，无法生产!");
                }
                //配比是否已审核
                else if (betonCm.AuditStatus == 0 || betonCm.SynStatus == 0)
                {
                    throw new Exception("混凝土配比未审核或修改未发送至搅拌楼，无法生产，请联系实验室!");
                }

                entity.BetonFormula = betonCm.ID;
                if (entity.SlurryCount > 0 && slurryCm == null)
                {
                    throw new Exception("没有找到此任务单的砂浆配比，无法生产!");
                }
                //配比是否已审核
                else if (entity.SlurryCount > 0 && slurryCm.AuditStatus == 0)
                {
                    throw new Exception("砂浆配比未审核，无法生产，请联系实验室!");
                }

                entity.SlurryFormula = slurryCm == null ? string.Empty : slurryCm.ID;
                //平均分配、非平均分配不同算法处理
                if (entity.IsAverage)
                {
                    this.Average(entity);
                }
                else
                {
                    this.NotAverage(entity);
                }
            }
        }

        /// <summary>
        /// 平均分配算法
        /// </summary>
        /// <param name="entity"></param>
        public void Average(DispatchList entity)
        {
            //混凝土生产值设定
            if (entity.BetonCount > 0)
            {
                if (entity.BetonCount <= entity.PCRate)
                {
                    entity.OnePCRate = entity.BetonCount;
                    entity.OneCube = entity.BetonCount;
                    entity.BTotalPot = 1;
                    entity.BNextPot = 1;
                }
                else
                {
                    entity.BTotalPot = this.CalculateTotalPot(entity.PCRate, entity.BetonCount);
                    entity.OnePCRate = entity.BetonCount / entity.BTotalPot;
                    entity.OneCube = entity.OnePCRate ?? 0;
                    entity.BNextPot = entity.BTotalPot;
                }
            }
            //附带砂浆设定
            if (entity.SlurryCount > 0)
            {
                entity.IsSlurry = true;
                if (entity.SlurryCount <= entity.PCRate)
                {
                    entity.OneSlurryPCRate = entity.SlurryCount;
                    entity.OneSlurryCube = entity.OneSlurryPCRate ?? 0;
                    entity.STotalPot = 1;
                    entity.SNextPot = 1;
                }
                else
                {
                    entity.STotalPot = this.CalculateTotalPot(entity.PCRate, entity.SlurryCount);
                    entity.OneSlurryPCRate = entity.SlurryCount / entity.STotalPot;
                    entity.OneSlurryCube = entity.OneSlurryPCRate ?? 0;
                    entity.SNextPot = entity.STotalPot;
                }
            }
        }

        /// <summary>
        /// 非平均分配算法，不存在砂浆问题处理，小于等于2盘或者能够整除罐容比，仍然使用平均分配
        /// </summary>
        /// <param name="entity"></param>
        public void NotAverage(DispatchList entity)
        {
            int? ps = this.CalculateTotalPot(entity.PCRate, entity.BetonCount);
            if (entity.BetonCount % entity.PCRate == 0 || ps <= 2)
            {
                this.Average(entity);
            }
            else
            {
                //计算前罐数及前方量
                entity.BTotalPot = ps;
                entity.BNextPot = ps - 2;
                entity.OnePCRate = entity.PCRate;
                entity.OneCube = entity.OnePCRate ?? 0;
                decimal? beforeCube = entity.PCRate * entity.BNextPot;
                //计算后罐容比及罐数
                entity.TwoPCRate = (entity.BetonCount - beforeCube) / 2;
                entity.TwoCube = entity.TwoPCRate ?? 0;
            }
        }

        /// <summary>
        /// 计算总盘数
        /// </summary>
        /// <returns></returns>
        public int? CalculateTotalPot(decimal? grb, decimal fs)
        {
            if (fs == 0)
            {
                throw new Exception("生产方量不能为0，无法生产!");
            }
            if (grb > fs)
            {
                return 1;
            }
            if ((fs % grb) == 0)
            {
                return Convert.ToInt32(fs / grb);
            }
            else
            {
                return Convert.ToInt32((fs - (fs % grb)) / grb + 1);
            }
        }


        public override void Delete(DispatchList entity)
        {

            try
            {
                string id = entity.ID;
                string docId = entity.ShipDocID;
                if (entity.IsRunning && !entity.IsCompleted)
                {
                    throw new Exception("该调度正在生产，不允许删除!");
                }
                if (entity.IsCompleted)
                {
                    throw new Exception("该调度已经生产完毕，不允许删除!");
                }
                //判断第一个生产登记是否锁定
                SysConfigService configService = new SysConfigService(this.m_UnitOfWork);
                SysConfig config = configService.GetSysConfig(SysConfigEnum.LockFirstDispatch);
                if (config != null && config.ConfigValue == "true")
                {
                    int prevCount = this.Query()
                                   .Where(p => p.DispatchOrder < entity.DispatchOrder
                                       && p.ProductLineID == entity.ProductLineID
                                       && p.IsCompleted == false).Count();
                    if (prevCount == 0)
                    {
                        throw new Exception("该生产调度已锁定，不允许删除!");
                    }
                }

                //此处用entity.ShippingDocument，修改shippingdocument后会导致DispatchList更新，导致插入更新的synmonitor
                //by:Sky 2012-05-10
                ShippingDocumentService shipService = new ShippingDocumentService(this.m_UnitOfWork);
                ShippingDocument document = shipService.Get(entity.ShipDocID);


                /*//直连:通知控制系统删除生产登记
                    by:Sky 2013/3/17
                *****************************************************************************/
                ResultInfo result = controlSystem.DeleteDispatch(entity);
                if (!result.Result)       //没有删除成功
                {
                    throw new Exception(Convert.ToString(result.Message));
                }
               
                base.Delete(entity);
                if (document.DispatchLists.Count > 1)
                {
                    //递减方量值
                    document.SendCube = document.SendCube - entity.ProduceCube;
                    document.BetonCount = document.BetonCount - entity.BetonCount;
                    document.SlurryCount = document.SlurryCount - entity.SlurryCount;
                    this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Update(document, null);
                }
                else
                {
                    document.IsEffective = false;
                    this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Update(document, null);
                }
                CarService carService = new CarService(this.m_UnitOfWork);
                if (!string.IsNullOrEmpty(document.CarID))
                {
                    carService.ChangeCarStatus(document.CarID, CarStatus.AllowShipCar);
                }



                GPSHelper gpshelper = new GPSHelper(this.m_UnitOfWork);
                gpshelper.GPSOperate("delete", entity);

            }
            catch (Exception ex)
            {

                logger.Error(ex.Message, ex);
                throw ex;
            }
            
        }


        /// <summary>
        /// 根据DJH获取可转生产线、扣补方量、车号等。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public dynamic GetInfoByDJH(string id)
        {
            DispatchList entity = this.Get(id);
            if (entity == null)
            {
                throw new Exception("生产调度信息获取失败！");
            }
            ShippingDocument document = entity.ShippingDocument;
            if (document == null)
            {
                document = this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Get(entity.ShipDocID);
            }
            if (document == null)
            {
                throw new Exception("发货单信息获取失败!");
            }
            string productLineId = document.ProductLineID;
            string taskId = document.TaskID;
            //查询已经分配配比的列表
            IList<ConsMixprop> phblist = this.m_UnitOfWork.ConsMixpropRepository.Query()
                .Where(c => c.TaskID == taskId && c.ProductLineID != productLineId).ToList();
            IList<string> pdlist = phblist.Select(p => p.ProductLineID).Distinct().ToList();
            IList<ProductLine> productlines = null;
            //获取分配配比的机组。
            if (pdlist.Count > 0)
            {
                productlines = this.m_UnitOfWork.GetRepositoryBase<ProductLine>()
                    .Query().Where(p => p.IsUsed && pdlist.Contains(p.ID))
                    .OrderBy(p => p.ID).ToList();
            }
            //计算可扣补方量
            string defaultCheck = "subtract";
            string carId = document.CarID;
            decimal? subtract = entity.ProduceCube - 1;
            decimal? fillCube = 0;
            if (carId != null)
            {
                Car currentCar = this.m_UnitOfWork.GetRepositoryBase<Car>().Get(carId);
                fillCube = currentCar.MaxCube - entity.ProduceCube;
            }
            if (fillCube > 0)
            {
                defaultCheck = "fill";
            }
            return new
            {
                Result = true
                ,
                Message = string.Empty
                ,
                DefaultCheck = defaultCheck
                ,
                SubTract = subtract
                ,
                FillCube = fillCube
                ,
                ProductLines = productlines
            };
        }


        /// <summary>
        /// 修改车号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carid"></param>
        /// <param name="driver"></param>
        public void ChangeCar(string id, string carid, string driver)
        {
            try
            {
                DispatchList entity = this.Get(id);
                if (entity.CarID == carid && entity.Driver == driver)
                {
                    throw new Exception("换车失败，选择了同一辆车！");
                }
                CarService carService = new CarService(this.m_UnitOfWork);
                if (entity.CarID != carid)
                {
                    carService.ChangeCarStatus(carid, CarStatus.ShippingCar);
                }
                ShippingDocument document = entity.ShippingDocument;
                if (document != null)
                {
                    if (entity.CarID != carid)
                    {
                        carService.ChangeCarStatus(document.CarID, CarStatus.AllowShipCar);
                        document.Remark = document.Remark + " 换车:" + document.CarID + "->" + carid;
                    }
                    else
                    {
                        document.Remark = document.Remark + " 换司机:" + document.Driver + "->" + driver;
                    }
                    document.CarID = carid;
                    document.Driver = driver;
                    this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Update(document, null);
                    this.m_UnitOfWork.Flush();
                }

                /*//直连:通知控制系统更新生产登记 
                by:Sky 2013/3/17
                *****************************************************************************/
                ResultInfo result = controlSystem.UpdateDispatch(entity);
                if (!result.Result)
                {
                    throw new Exception(result.Message);
                }

                GPSHelper gpshelper = new GPSHelper(this.m_UnitOfWork);
                gpshelper.GPSOperate("update", entity);

                this.Update(entity, null);    //修改完车辆跟司机更新下调度，让其触发同步
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw;
            }
        }


        public void UpdateProvided(string id,int providedtimes,decimal providedcube)
        {
            try
            {
                DispatchList entity = this.Get(id);
                ShippingDocument sd = entity.ShippingDocument;
                sd.ProvidedTimes = providedtimes;
                sd.ProvidedCube = providedcube;
                this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Update(sd, null);
                this.m_UnitOfWork.Flush();

                this.Update(entity, null);    //修改完车辆跟司机更新下调度，让其触发同步
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// 修改方量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cube"></param>
        public void ChangeCube(string id, decimal? cube)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    DispatchList entity = this.Get(id);
                    if (entity.IsRunning)
                    {
                        throw new Exception("该任务正在生产，无法执行该操作！");
                    }
                    if (entity.IsCompleted)
                    {
                        throw new Exception("生产完成的调度无法执行该操作！");
                    }
                    string carId = entity.CarID;
                    decimal operCube = cube ?? 0;
                    if (!string.IsNullOrEmpty(carId))
                    {
                        Car car = this.m_UnitOfWork.GetRepositoryBase<Car>().Get(carId);
                        decimal maxCube = car.MaxCube ?? 0;
                        if (operCube > 0 && (entity.ProduceCube + operCube) > maxCube)
                        {
                            throw new Exception("超过了车的最大装载容量，操作失败！");
                        }
                        if (entity.ProduceCube + operCube < 0)
                        {
                            throw new Exception("最多允许扣除" + (entity.ProduceCube - 1) + "方");
                        }
                    }
                    entity.ProduceCube = entity.ProduceCube + operCube;
                    entity.BetonCount = entity.BetonCount + operCube;
                    this.CalculateProduce(entity, false);
                    base.Update(entity, null);
                    ShippingDocument document = entity.ShippingDocument;
                    if (document != null)
                    {
                        if (document.BetonCount == 0 && document.SlurryCount > 0)
                        {
                            throw new Exception("单独生产砂浆的情况不允许扣补方量!");
                        }
                        document.BetonCount = document.BetonCount + operCube;
                        document.ParCube = document.ParCube + operCube;
                        document.ProvidedCube = document.ProvidedCube + operCube;
                        //调度方量 = 混凝土 + 砂浆
                        document.SendCube = document.BetonCount + document.SlurryCount;
                        document.ShippingCube = document.SendCube;
                        this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Update(document, null);
                        this.m_UnitOfWork.Flush();
                    }
                    
                        //GPS
                        GPSHelper gpshelper = new GPSHelper(this.m_UnitOfWork);
                        gpshelper.GPSOperate("update", entity);
                    
                    tx.Commit();
                    
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message, ex);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 转换生产线
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pid"></param>
        public void ChangeProductLine(string id, string pid)
        {
            #region 删除
            DispatchList newdisp = DeleteFromOneProductLine(id, pid);
            #endregion
            #region 新增
            ToAnotherProductLine(newdisp);
            #endregion
        }


        private DispatchList DeleteFromOneProductLine(string id, string pid)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //删除原来的，新增加记录
                    DispatchList entity = this.Get(id);
                    string docId = entity.ShipDocID;

                    if (entity.IsRunning)
                    {
                        throw new Exception("该任务正在生产，无法执行该操作！");
                    }
                    if (entity.IsCompleted)
                    {
                        throw new Exception("生产完成的调度无法执行该操作！");
                    }
                    ShippingDocument document = entity.ShippingDocument;
                    ShippingDocument newShipDoc = new ShippingDocument();
                    newShipDoc.TaskID = document.TaskID;
                    newShipDoc.BetonCount = document.BetonCount;
                    newShipDoc.SlurryCount = document.SlurryCount;
                    newShipDoc.SendCube = document.SendCube;
                    newShipDoc.SignInCube = document.SignInCube;
                    newShipDoc.ShippingCube = document.ShippingCube;
                    newShipDoc.ParCube = document.ParCube;
                    newShipDoc.RemainCube = document.RemainCube;
                    newShipDoc.ProvidedCube = document.ProvidedCube;
                    newShipDoc.TransferCube = document.TransferCube;
                    newShipDoc.PlanCube = document.PlanCube;
                    newShipDoc.ScrapCube = document.ScrapCube;
                    newShipDoc.OtherCube = document.OtherCube;
                    newShipDoc.CarID = document.CarID;
                    newShipDoc.PumpName = document.PumpName;
                    newShipDoc.ProvidedTimes = document.ProvidedTimes;
                    newShipDoc.DeliveryTime = document.DeliveryTime;
                    newShipDoc.Remark = "CODEADD该票由[" + document.ProductLineName + ":" + document.ID + "]转线生成";
                    //新增记录.
                    DispatchList newEntity = entity.Clone() as DispatchList;
                    newEntity.ID = string.Empty;
                    //newEntity.ShipDocID = docId;
                    newEntity.ProductLineID = pid;
                    ProductLine productLine = this.m_UnitOfWork.GetRepositoryBase<ProductLine>()
                        .Query().Where(p => p.ID == pid).FirstOrDefault();
                    if (productLine == null)
                    {
                        throw new Exception("生产线信息获取失败!");
                    }
                    if (newShipDoc != null)
                    {
                        newShipDoc.ProductLineID = productLine.ID;
                        newShipDoc.ProductLineName = productLine.ProductLineName;
                        //this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Update(document, null);

                        ShippingDocument savedNewShipDoc = this.SaveShippingDocument(newShipDoc);
                        newEntity.ShipDocID = savedNewShipDoc.ID;
                        //如果为单独打砂浆的情况转线，调度信息需要进行修改。
                        if (document.BetonCount == 0 && document.SlurryCount > 0)
                        {
                            newEntity.BetonCount = 0;
                            newEntity.SlurryCount = document.SlurryCount;
                        }
                    }

                    newEntity.ProductLineID = pid;
                    newEntity.PCRate = productLine.DishNum;
                    bool isAllowDelete = true;
                    

                    /*//直连:通知控制系统删除原有生产登记，并增加新生产登记
                    by:Sky 2013/3/17
                    *****************************************************************************/ 
                    ResultInfo result = controlSystem.DeleteDispatch(entity);
                    if (!result.Result)       //没有删除成功
                    {
                        isAllowDelete = false;
                        throw new Exception("[控制系统]：该生产登记配比已切换或正在生产，不允许转线");
                    } 
                     
                    /*[直连修改结束]
                    by:Sky 2013/3/17
                    ******************************************************************************/

                    if (isAllowDelete)
                    {
                        base.Delete(entity);
                    }

                    tx.Commit();
                    return this.Add(newEntity);
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message, ex);
                    throw ex;
                }


            }
        }
        private void ToAnotherProductLine(DispatchList newEntity)
        {
            try
            {
                /*//直连: 增加新生产登记到新线
                     by:Sky 2013/3/17
                 *****************************************************************************/ 
                ResultInfo result = controlSystem.AddDispatch(newEntity);
                if (!result.Result)       //没有添加成功
                {
                    
                    throw new Exception(Convert.ToString(result.Message));
                }
                /*[直连修改结束]
                by:Sky 2013/3/17
                ******************************************************************************/

            }
            catch (Exception ex)
            {
                newEntity.SendStatus = 0;
                base.Update(newEntity);
               // base.Delete(newEntity);
                logger.Error(ex.Message, ex);
                throw ex;
            }
        }


        public string CheckCubeLimit(SchedulerViewModel schedulShippEntity)
        {
            try
            {
                //先取得任务单信息
                ProduceTask task = this.m_UnitOfWork.GetRepositoryBase<ProduceTask>().Get(schedulShippEntity.ShippingDocument.TaskID);
                if (task == null)
                {
                    throw new Exception(Lang.Error_TaskNotExist);
                }
                ShippingDocument shipp = schedulShippEntity.ShippingDocument;
                //获取当前发货单中的已供方量及本车方量
                decimal providedcube = shipp.ProvidedCube;
                
                //取得合同中的限制类型及合同状态（是否锁定）
                SystemMsgService systemMsgService = new SystemMsgService(this.m_UnitOfWork);
                Contract contract = this.m_UnitOfWork.GetRepositoryBase<Contract>().Get(task.ContractID);

                //本车方量
                decimal parcube = schedulShippEntity.DispatchList.BetonCount + schedulShippEntity.DispatchList.SlurryCount + schedulShippEntity.ShippingDocument.OtherCube + (decimal)schedulShippEntity.ShippingDocument.RemainCube;

                string cubeLimit = contract.ContractLimitType;
                string contractStatus = contract.ContractStatus;
                bool isLimited = contract.IsLimited;
                string resultMsg = null;
                //先检查是否已受限，然后再依据受限类型进行处理
                if (isLimited)
                {
                    resultMsg = Lang.Msg_ContractIsLimited + contract.ID;
                    return resultMsg;
                }
                else
                {
                    //这里的比较条件需要讨论，建议使用const
                    if (contractStatus.Equals(Consts.ContractStatusBeginning))
                    {
                        switch (cubeLimit)
                        {
                            case Consts.LimitNone:
                                break;
                            case Consts.LimitContractTime:
                                //合同结束时间
                                DateTime? contractET = contract.ContractET;
                                if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(contractET)) >= 0)
                                {
                                    SetContractToLimit(contract);
                                    systemMsgService.SendMsg("ContractExpire", "合同编号：" + contract.ID);
                                    resultMsg = Lang.Msg_LimitContractTime + contract.ID;
                                    return resultMsg;
                                }
                                break;
                            case Consts.LimitMatCube:
                                //本合同垫资方量
                                decimal _matcube = 0;
                                if (this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Query().Where(m => m.ContractID == contract.ID).Count() > 0)
                                {
                                    _matcube = this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Query().Where(m => m.ContractID == contract.ID && m.IsEffective).ToList().Sum(m => m.SignInCube);
                                }
                                decimal matcube = contract.MatCube ?? 0;
                                if (_matcube + parcube > matcube)
                                {
                                    SetContractToLimit(contract);
                                    systemMsgService.SendMsg("ContractPadding", "合同编号：" + contract.ID);
                                    resultMsg = Lang.Msg_LimitMatCube + (_matcube + parcube - matcube) + Lang.Cube_Unit;
                                    return resultMsg;
                                }
                                break;
                            case Consts.LimitPrepayCube:
                                //本合同预付方量
                                decimal _prepaycube = 0;
                                if (this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Query().Where(m => m.ContractID == contract.ID).Count() > 0)
                                {
                                    _prepaycube = this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Query()
                                    .Where(m => m.ContractID == contract.ID && m.IsEffective).Sum(m => m.SignInCube); 
                                }
                                decimal prepaycube = contract.PrepayCube ?? 0;
                                if (_prepaycube + parcube > prepaycube)
                                {
                                    SetContractToLimit(contract);
                                    systemMsgService.SendMsg("ContractPrepay", "合同编号：" + contract.ID);
                                    resultMsg = Lang.Msg_LimitPrepayCube + (_prepaycube + parcube - prepaycube) + Lang.Cube_Unit;
                                    return resultMsg;
                                }
                                break;
                            default:
                                break;
                        }

                    }
                    else
                    {
                        //合同状态为非进行中
                        SetContractToLimit(contract);
                        systemMsgService.SendMsg("ContractExpire", "非进行中合同，编号：" + contract.ID);
                        resultMsg = Lang.Msg_ContractStatus + contract.ID;
                        return resultMsg;
                    }
                }


            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }
            return null;
        }

        /// <summary>
        /// 标识合同为已受限
        /// </summary>
        /// <param name="contract"></param>
        private void SetContractToLimit(Contract contract)
        {
            try
            {
                contract.IsLimited = true;
                this.m_UnitOfWork.GetRepositoryBase<Contract>().Update(contract, null);
                this.m_UnitOfWork.Flush();
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }

        }



        /// <summary>
        /// 置为完成
        /// </summary>
        /// <param name="id"></param>
        public void SetCompleted(string id)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    DispatchList entity = this.Get(id);
                    string docId = entity.ShipDocID;
                    ShippingDocument document = entity.ShippingDocument;
                    if (entity.IsCompleted)
                    {
                        throw new Exception(" 已经完成的调度无法执行该操作！");
                    }

                    //判断第一个生产登记是否锁定
                    SysConfigService configService = new SysConfigService(this.m_UnitOfWork);
                    SysConfig config = configService.GetSysConfig(SysConfigEnum.LockFirstDispatch);
                    if (config != null && config.ConfigValue == "true")
                    {
                        int prevCount = this.Query()
                                       .Where(p => p.DispatchOrder < entity.DispatchOrder
                                           && p.ProductLineID == entity.ProductLineID
                                           && p.IsCompleted == false).Count();
                        if (prevCount == 0)
                        {
                            throw new Exception("该生产调度已锁定，不允许置为完成，请通知搅拌楼取消!");
                        }
                    }

                    //没有生产记录的发货单作废
                    //IList<ProductRecord> list = this.m_UnitOfWork.GetRepositoryBase<ProductRecord>()
                    //    .Query().Where(p => p.ShipDocID == docId).ToList();
                    //if (list.Count == 0)
                    //{
                    //    document.IsEffective = false;
                    //    this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Update(document, null);
                    //}
                    entity.IsCompleted = true;
                    base.Update(entity, null);
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message, ex);
                    throw ex;
                }
            }
        }

        public void SetRun(string id)
        {
            DispatchList entity = this.Get(id);
            entity.IsRunning = true;
            base.Update(entity, null);
        }

        /// <summary>
        /// 上移生产调度
        /// </summary>
        /// <param name="id"></param>
        public void MoveUp(string id)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    DispatchList entity = this.Get(id);
                    if (entity.IsRunning)
                    {
                        throw new Exception(" 正在生产的生产调度不允许移动！");
                    }
                    //没有生产记录的发货单作废
                    IList<DispatchList> list = this.Query()
                        .Where(p => p.IsCompleted == false && p.DispatchOrder < entity.DispatchOrder && p.ProductLineID == entity.ProductLineID)
                        .OrderByDescending(p => p.DispatchOrder).ToList();
                    if (list.Count == 0)
                    {
                        throw new Exception(" 此调度已经是最顶部，无法移动！");
                    }
                    //获取系统锁定配置，锁定且移动第2个之后的，或者没锁定第1个以后的，允许移动。
                    SysConfigService configSvr = new SysConfigService(this.m_UnitOfWork);
                    SysConfig config = configSvr.GetSysConfig("LockDispatchMoveUp");
                    bool lockscdj = config == null ? true : Convert.ToBoolean(config.ConfigValue);
                    if ((lockscdj && list.Count >= 2) || (!lockscdj && list.Count >= 1))
                    {
                        DispatchList swapEntity = list[0];
                        int orderNum = swapEntity.DispatchOrder ?? 0;
                        swapEntity.DispatchOrder = entity.DispatchOrder;
                        entity.DispatchOrder = orderNum;
                        base.Update(entity, null);
                        base.Update(swapEntity, null);
                    }
                    else
                    {
                        throw new Exception(" 此调度不允许移动，请通知搅拌楼操作！");
                    }
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message, ex);
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 检查最大计量值
        /// </summary>
        /// <param name="svm"></param>
        /// <returns></returns>
        public string CheckCheckMesureScale(SchedulerViewModel svm)
        {
            decimal concrete = svm.DispatchList.BetonCount;
            decimal slurry = svm.DispatchList.SlurryCount;
            decimal rz = svm.DispatchList.PCRate ?? 0;
            string taskId = svm.ShippingDocument.TaskID;
            string productLine = svm.DispatchList.ProductLineID;
            bool checkConcrete = false;
            bool checkSlurry = false;
            decimal concreteRZ = rz;
            decimal slurryRZ = rz;
            //小于罐容比，使用方量
            if (concrete > 0)
            {
                checkConcrete = true;
                if (concrete < rz)
                {
                    concreteRZ = concrete;
                }
            }
            if (slurry > 0)
            {
                checkSlurry = true;
                if (slurry < rz)
                {
                    slurryRZ = slurry;
                }
            }
            if (checkConcrete || checkSlurry)
            {
                ConsMixpropService service = new ConsMixpropService(this.m_UnitOfWork);
                return service.CheckMesureScale(taskId, productLine, concreteRZ, slurryRZ, checkConcrete, checkSlurry);
            }
            else
                return null;
        }
        /// <summary>
        /// 车辆取样
        /// </summary>
        /// <param name="dispatchid"></param>
        /// <param name="carid"></param>
        public void Sampling(string dispatchid, string carid)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    DispatchList dispatch = this.Get(dispatchid);
                    dispatch.Field1 = true;
                    base.Update(dispatch, null);
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

        public void RepeatSend(string dispatchid, int sendstatus)
        {

            DispatchList dispatch2 = this.Get(dispatchid);
            try
            {
                //直连：添加生产登记到控制系统
                ResultInfo result = controlSystem.AddDispatch(dispatch2);
                if (!result.Result)//没有添加成功
                {//分两种情况
                    if (result.ResultCode == 218)//1、218表示记录已存在，只需更新ERP的发送状态
                    {
                        dispatch2.SendStatus = 1;
                        base.Update(dispatch2);
                    }
                    else
                    {
                        base.Delete(dispatch2);//2、控制系统中本身业务逻辑错误导致插入失败，ERP删除
                        throw new Exception(result.Message);
                    }
                }
                else {//TCP添加成功，直接将发送中，改成发送成功。
                    dispatch2.SendStatus = 1;
                    base.Update(dispatch2);
                }     
            }
            catch (Exception)
            {
                //网络异常，将生产调度的发送状态改成失败，并显示重发按钮
                if (this.Get(dispatch2.ID) != null)
                {
                    dispatch2.SendStatus = 0;
                    base.Update(dispatch2);
                }
                throw;
            }

        }

    }
}

