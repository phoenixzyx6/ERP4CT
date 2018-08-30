using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.Model.Enums;
using ZLERP.Resources;

namespace ZLERP.Business
{
    public class TZRalationService : ServiceBase<TZRalation>
    {
        PublicService s;
        internal TZRalationService(IUnitOfWork uow)
            : base(uow) { }

        /// <summary>
        /// 增加转退料记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override TZRalation Add(TZRalation entity)
        {
            DateTime ts = DateTime.Now;
            entity.ActionTime = ts;
            entity.ActionCube = entity.Cube;
            entity.IsLock = "0";
            entity.TzRalationFlag = getZtlDh();

            TZRalation z = base.Add(entity);

            AddHistory(entity, "add", entity.CarID, entity.Cube);

            return z;
        }
        /// <summary>
        /// 生产转退料单号
        /// </summary>
        /// <returns></returns>
        private string getZtlDh()
        {
            return "TZ" + DateTime.Now.ToString("yyMMddHHmmss");
        }
        //public override void Update(TZRalation entity)
        //{
        //    base.Update(entity);
        //    entity.IsLock = "3";
        //    AddHistory(entity, "update", entity.CarID, entity.Cube);

        //}

        /// <summary>
        /// 审核退料转发记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Audit(int id)
        {
            TZRalation entity = this.Get(id);
            if (entity != null)
            {
                if (entity.IsCompleted && !entity.IsAudit)
                {
                    entity.IsAudit = true;
                    entity.Auditor = AuthorizationService.CurrentUserID;
                    entity.AuditTime = DateTime.Now;
                    ShippingDocument sourceDoc = entity.SourceShippingDocument;

                    ////分装审核处理，肖敏，2013-09-15
                    //IList<TZRalation> tzList = this.Query().Where(p => p.SourceShipDocID == entity.SourceShipDocID).ToList();
                    
                    ////分装
                    //if (tzList.Count > 1)
                    //{
                    //    //剩料时，原运输单的转料方量累加
                    //    //签收方量不变
                    //    if (entity.ReturnType == ReturnType.ShengLiao && entity.ActionType == ActionType.Transfer)
                    //    {
                    //        decimal tc = 0;
                    //        foreach (var tz in tzList)
                    //        {
                    //            if (tz.IsAudit && tz.IsCompleted)
                    //            {
                    //                tc += tz.Cube;
                    //            }
                    //        }

                    //        sourceDoc.TransferCube = tc;
                    //    }

                    //    //退料时，转料方量=剩料方量累加
                    //    //原运输单的签收方量=出票方量-退料方量
                    //    if (entity.ReturnType == ReturnType.TuiLiao && entity.ActionType == ActionType.Transfer)
                    //    {
                    //        decimal tc = 0;
                    //        foreach (var tz in tzList)
                    //        {
                    //            if (tz.IsAudit && tz.IsCompleted)
                    //            {
                    //                tc += tz.Cube;
                    //            }
                    //        }

                    //        sourceDoc.TransferCube = tc;
                    //        sourceDoc.SignInCube = sourceDoc.ParCube - tc;
                    //    }
                    //}
                    //else
                    //{
                    //    //xjm修改
                    //    //2013-04-08

                    //    //剩料时，原运输单的转料方量=剩料方量
                    //    //签收方量不变
                    //    if (entity.ReturnType == ReturnType.ShengLiao && entity.ActionType == ActionType.Transfer)
                    //    {
                    //        sourceDoc.TransferCube = entity.Cube;
                    //    }

                    //    //退料时，原运输单的签收方量=出票方量-退料方量
                    //    //转料方量=剩料方量
                    //    if (entity.ReturnType == ReturnType.TuiLiao && entity.ActionType == ActionType.Transfer)
                    //    {
                    //        //sourceDoc.SignInCube -= entity.Cube;
                    //        sourceDoc.SignInCube = sourceDoc.ParCube - entity.Cube;
                    //        sourceDoc.TransferCube = entity.Cube;
                    //    }
                    //}

                    ////报废，原运输单报废方量设置
                    ////签收方量=出票方量-报废方量
                    //if (entity.ReturnType == ReturnType.TuiLiao && entity.ActionType == ActionType.Reject)
                    //{
                    //    sourceDoc.ScrapCube = entity.Cube;
                    //    sourceDoc.SignInCube = sourceDoc.ParCube - entity.Cube;
                    //}

                    ////整车转发，原运输单的转料方量=运输方量
                    ////签收方量=0
                    //if (entity.ReturnType == ReturnType.Forward)
                    //{
                    //    sourceDoc.TransferCube = sourceDoc.ShippingCube;
                    //    sourceDoc.SendCube = 0;
                    //    sourceDoc.SignInCube = 0;
                    //    sourceDoc.ParCube = 0;
                    //}

                    //剩料时，转料方量=剩料方量累加
                    //签收方量不变
                    if (entity.ReturnType == ReturnType.ShengLiao && entity.ActionType == ActionType.Transfer)
                    {
                        sourceDoc.TransferCube += entity.ActionCube;
                    }

                    //退料时，转料方量=退料方量累加
                    //签收方量=出票方量-退料方量
                    if (entity.ReturnType == ReturnType.TuiLiao && entity.ActionType == ActionType.Transfer)
                    {
                        sourceDoc.TransferCube += entity.ActionCube;
                        //sourceDoc.SignInCube = sourceDoc.ParCube - (sourceDoc.ParCube - sourceDoc.SignInCube) - entity.ActionCube;
                        sourceDoc.SignInCube =sourceDoc.SignInCube- entity.ActionCube;
                    }

                    //报废，报废方量=剩退方量累加
                    //签收方量=出票方量-报废方量
                    if (entity.ReturnType == ReturnType.TuiLiao && entity.ActionType == ActionType.Reject)
                    {
                        sourceDoc.ScrapCube += entity.ActionCube;
                        //sourceDoc.SignInCube = sourceDoc.ParCube - (sourceDoc.ParCube - sourceDoc.SignInCube) - entity.ActionCube;
                        sourceDoc.SignInCube = sourceDoc.SignInCube - entity.ActionCube;
                    }

                    //整车转发，原运输单的转料方量=运输方量
                    //签收方量=0
                    if (entity.ReturnType == ReturnType.Forward)
                    {
                        sourceDoc.TransferCube = sourceDoc.ShippingCube;
                        sourceDoc.SendCube = 0;
                        sourceDoc.SignInCube = 0;
                        sourceDoc.ParCube = 0;
                    }
                    using (IGenericTransaction trans = this.m_UnitOfWork.BeginTransaction())
                    {
                        try
                        {
                            this.Update(entity, null);
                            this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Update(sourceDoc, null);
                            this.m_UnitOfWork.Flush();
                            trans.Commit();
                            return true;
                        }
                        catch
                        {
                            trans.Rollback();
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 整车转发
        /// </summary>
        /// <param name="sourceShipDocID">源运输单ID</param>
        /// <param name="targetTaskId">转至任务单</param>
        /// <param name="isOriginComplete">原任务单已完成（不需要转料后补）</param>
        /// <param name="isTrashOrigin">原任务单作废</param>
        /// <param name="returnReason">转发原因</param>
        /// <returns></returns>
        public ShippingDocument Forward(string sourceShipDocID, string targetTaskId, bool isOriginComplete, bool isTrashOrigin, string returnReason, out string message, string[] _carids)
        {
            var shipDocRepo = this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>();
            ShippingDocument oldDoc = shipDocRepo.Get(sourceShipDocID);
            ProduceTask task = this.m_UnitOfWork.GetRepositoryBase<ProduceTask>().Get(targetTaskId);
            message = Lang.Msg_Operate_Success;
            if (oldDoc != null && task != null)
            {

                //UNDONE: 转发方量和生产方量是否要处理
                ShippingDocument newDoc = new ShippingDocument();// oldDoc.Clone() as ShippingDocument;\

                //如果有_carids说明不光转发还要换车
                if (_carids == null || _carids.Length == 0 || string.IsNullOrEmpty(_carids[0]))
                    newDoc.CarID = oldDoc.CarID;
                else
                    newDoc.CarID = _carids[0];

                newDoc.IsEffective = true;
                newDoc.ContractID = task.ContractID;

                newDoc.ContractName = task.Contract.ContractName;
                newDoc.CustName = task.Contract.CustName;
                newDoc.CustomerID = task.Contract.CustomerID;

                newDoc.CustMixpropID = task.CustMixpropID;

                newDoc.ProductLineName = oldDoc.ProductLineName;
                newDoc.ProductLineID = oldDoc.ProductLineID;

                newDoc.DeliveryTime = DateTime.Now;
                //newDoc.ProduceDate = oldDoc.ProduceDate;
                newDoc.ProduceDate = DateTime.Now;

                newDoc.ConsMixpropID = oldDoc.ConsMixpropID;
                newDoc.SlurryConsMixpropID = oldDoc.SlurryConsMixpropID;
                newDoc.FormulaName = oldDoc.FormulaName;
                newDoc.ProjectName = task.ProjectName;
                newDoc.ProjectAddr = task.ProjectAddr;
                newDoc.ProjectID = task.ProjectID;
                newDoc.ShipDocType = "0";
                newDoc.ConStrength = task.ConStrength;
                newDoc.CastMode = task.CastMode;
                newDoc.ConsPos = task.ConsPos;
                newDoc.ImpGrade = task.ImpGrade;
                newDoc.ImyGrade = task.ImyGrade;
                newDoc.ImdGrade = task.ImdGrade;
                newDoc.CarpRadii = task.CarpRadii;
                newDoc.CementBreed = task.CementBreed;
                newDoc.Distance = oldDoc.Distance;
                newDoc.RealSlump = task.Slump;//oldDoc.RealSlump;此处换成目标工地的坍落度，保持与票面一致
                newDoc.BetonCount = oldDoc.BetonCount;
                newDoc.SlurryCount = oldDoc.SlurryCount;
                newDoc.OtherCube = oldDoc.OtherCube;
                newDoc.XuCube = oldDoc.XuCube;
                newDoc.RemainCube = oldDoc.RemainCube;


                newDoc.ShippingCube = oldDoc.ShippingCube;
                newDoc.PlanCube = task.PlanCube;

                

                //调度方量 = 混凝土 + 砂浆
                newDoc.SendCube = newDoc.BetonCount + newDoc.SlurryCount;
                //出票方量 = 调度方量 + 其它方量 + 剩余方量
                decimal? parCube = newDoc.SendCube + newDoc.OtherCube + (newDoc.RemainCube ?? 0);
                newDoc.ParCube = parCube ?? 0;
                newDoc.SignInCube = newDoc.ParCube;
                newDoc.ScrapCube = 0;
                newDoc.TransferCube = 0;
                newDoc.Surveyor = oldDoc.Surveyor;
                
                newDoc.Operator = oldDoc.Operator;
                newDoc.RegionID = task.RegionID;
                newDoc.ForkLift = task.ForkLift;
                newDoc.PlanClass = task.PlanClass;
                //newDoc.ProduceDate = DateTime.Now;
                newDoc.SupplyUnit = task.SupplyUnit;
                newDoc.ConstructUnit = task.ConstructUnit;

                newDoc.Signer = AuthorizationService.CurrentUserID;
                newDoc.LinkMan = task.LinkMan;
                newDoc.Tel = task.Tel;
                //是否代外生产.
                newDoc.EntrustUnit = task.IsCommission ? task.SupplyUnit : "";
                newDoc.Remark = string.Format("CODEADD由运输单:{0}整车转发生成", sourceShipDocID);
                newDoc.IsProduce = oldDoc.IsProduce;

                newDoc.ID = null;
                newDoc.TaskID = targetTaskId;

                //从最后一个发货单取得累计方量和车数
                ShippingDocumentService sdService = new ShippingDocumentService(this.m_UnitOfWork);
                ShippingDocument doc = sdService.Query()
                    .Where(p => p.TaskID == targetTaskId && p.IsEffective == true && p.ShipDocType == "0")
                    .OrderByDescending(p => p.BuildTime)
                    .FirstOrDefault();
                if (doc != null)
                {
                    newDoc.ProvidedTimes = doc.ProvidedTimes + 1;
                    newDoc.ProvidedCube = doc.ProvidedCube + newDoc.ParCube;

                    newDoc.PumpName = doc.PumpName;
                }
                else {
                    newDoc.ProvidedTimes = 1;
                    newDoc.ProvidedCube = newDoc.ParCube;
                }
                
                //newDoc.ProjectName = task.ProjectName;
                //newDoc.ConStrength = task.ConStrength;
                //newDoc.CastMode = task.CastMode;
                //newDoc.ConsPos = task.ConsPos;
                //newDoc.ImpGrade = task.ImpGrade;
                //newDoc.ImyGrade = task.ImyGrade;
                //newDoc.ImdGrade = task.ImdGrade;
                //newDoc.CarpRadii = task.CarpRadii;
                //newDoc.CementBreed = task.CementBreed; 　
                //newDoc.ForkLift = task.ForkLift;
                //newDoc.PlanClass = task.PlanClass;
                //newDoc.ProduceDate = DateTime.Now;
                //newDoc.SupplyUnit = task.SupplyUnit;
                //newDoc.ConstructUnit = task.ConstructUnit;

                ////是否托外生产.
                //newDoc.EntrustUnit = task.IsCommission ? task.SupplyUnit : "";
                //newDoc.IsEffective = true;

                //生产登记关系转移到新发货单

                newDoc.DispatchLists = new List<DispatchList>();
                //newDoc.DispatchLists.Clear();
                //newDoc.DispatchLists = null;
                newDoc.TZRalations = null;
                foreach (var d in oldDoc.DispatchLists)
                {
                    newDoc.DispatchLists.Add(d);
                }

                //原调度关系清空
                oldDoc.DispatchLists.Clear();
                //原运输单是否作废
                oldDoc.IsEffective = !isTrashOrigin;
                
                //记录作废原因
                if (oldDoc.IsEffective)
                    oldDoc.Remark = (string.IsNullOrEmpty(oldDoc.Remark) ? "" : oldDoc.Remark + " ") + "CODEADD整车转发";
                else
                    oldDoc.Remark = (string.IsNullOrEmpty(oldDoc.Remark) ? "" : oldDoc.Remark + " ") + "CODEADD整车转发作废";

                //退转料记录
                TZRalation tzRelation = new TZRalation
                {
                    SourceShipDocID = oldDoc.ID,
                    SourceCube=oldDoc.ParCube,
                    CarID = oldDoc.CarID,
                    Driver = oldDoc.Driver,
                    Cube = oldDoc.ParCube,
                    ReturnType = ZLERP.Model.Enums.ReturnType.Forward,
                    ActionType = ZLERP.Model.Enums.ActionType.Transfer,
                    IsCompleted = true,
                    ReturnReason = returnReason
                    
                };
                //UNDONE:目标任务单是否要更新已供和累计方量
                using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        oldDoc.IsProduce = false;
                        shipDocRepo.Update(oldDoc, null);
                        //this.m_UnitOfWork.Flush();
                        shipDocRepo.Add(newDoc);
                        // this.m_UnitOfWork.Flush();
                        tzRelation.TargetShipDocID = newDoc.ID;
                        //整车转发时，生产记录自动转
                        List<ProductRecord> prList = this.m_UnitOfWork.GetRepositoryBase<ProductRecord>().Query()
                            .Where(m => m.ShipDocID == oldDoc.ID)
                            .ToList();
                        foreach (ProductRecord pr in prList)
                        {
                            pr.ShipDocID = newDoc.ID;
                            this.m_UnitOfWork.GetRepositoryBase<ProductRecord>().Update(pr, null);
                        }
                        DateTime ts = DateTime.Now;
                        tzRelation.ActionTime = ts;
                        tzRelation.ActionCube = tzRelation.Cube;

                        //整车转发状态设置为3
                        tzRelation.IsLock = "3";
                        //转退料单号
                        tzRelation.TzRalationFlag = getZtlDh() + "ZC";

                        base.Add(tzRelation);
                        tx.Commit();
                        return newDoc;
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                        tx.Rollback();
                        logger.Error(ex.Message, ex);
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(TZRalation entity)
        {
            if (entity.IsAudit)
                throw new Exception("已审核记录禁止删除");
            logger.Debug(string.Format("删除转退料记录,ID:{0},ReturnType:{1},ActionType:{2}", entity.ID, entity.ReturnType, entity.ActionType));
            if (entity.ReturnType == "RT0")
            {
                var shipDocRepo = this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>();

                ShippingDocument sourceShippingDocument = entity.SourceShippingDocument;
                sourceShippingDocument.IsEffective = true;
                if (!string.IsNullOrEmpty(sourceShippingDocument.Remark) && sourceShippingDocument.Remark.IndexOf("CODEADD整车转发") >= 0)
                    sourceShippingDocument.Remark = sourceShippingDocument.Remark.Remove(sourceShippingDocument.Remark.IndexOf("CODEADD整车转发"));

                ShippingDocument targetShippingDocument = entity.TargetShippingDocument;
                targetShippingDocument.IsEffective = false;
                targetShippingDocument.Remark = (string.IsNullOrEmpty(targetShippingDocument.Remark) ? "" : targetShippingDocument.Remark + " ") + "整车转发记录被删除";

                shipDocRepo.Update(sourceShippingDocument, null);
                shipDocRepo.Update(targetShippingDocument, null);
            }
            base.Delete(entity);
        }

        /// <summary>
        /// 分装
        /// </summary>
        /// <param name="sourceShipDocID"></param>
        /// <param name="sourceCube"></param>
        /// <param name="returnType"></param>
        /// <param name="returnReason"></param>
        /// <param name="carIDArr"></param>
        /// <param name="carCubeArr"></param>
        /// <returns></returns>
        public bool Split(string sourceShipDocID, decimal sourceCube, string returnType, string returnReason, string[] carIDArr, string[] carCubeArr, string[] actionTypeArr)
        {
            lock (obj)
            {
                using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        DateTime ts = DateTime.Now;
                        for (int i = 0; i < carIDArr.Length; ++i)
                        {
                            if (!string.IsNullOrEmpty(carIDArr[i]) && !string.IsNullOrEmpty(carCubeArr[i]) && !string.IsNullOrEmpty(actionTypeArr[i]))
                            {
                                TZRalation tz = new TZRalation();
                                tz.SourceShipDocID = sourceShipDocID;
                                tz.SourceCube = sourceCube;
                                tz.ReturnType = returnType;
                                tz.CarID = carIDArr[i];
                                tz.Cube = Decimal.Parse(carCubeArr[i]);
                                tz.ActionType = actionTypeArr[i];

                                if (tz.ActionType == Model.Enums.ActionType.Reject)
                                {
                                    tz.IsCompleted = true;

                                }
                                else
                                {
                                    tz.IsCompleted = false;
                                }
                                tz.AH = Model.Enums.Consts.Handle;
                                tz.ReturnReason = returnReason;

                                tz.ActionTime = ts;
                                tz.ActionCube = tz.Cube;
                                tz.IsLock = "1";//锁定

                                base.Add(tz);
                            }
                        }

                        tx.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {
                        tx.Rollback();
                        logger.Error(e.Message, e); ;
                    }
                }
                return false;
            }
        }


        /// <summary>
        /// 修改转退料
        /// </summary>
        /// <param name="sourceShipDocID"></param>
        /// <param name="sourceCube"></param>
        /// <param name="returnType"></param>
        /// <param name="returnReason"></param>
        /// <param name="carIDArr"></param>
        /// <param name="carCubeArr"></param>
        /// <returns></returns>
        private static readonly object obj = new object();
        public bool SplitByZT(string id ,string sourceShipDocID, decimal sourceCube, string returnType, string returnReason, string[] carIDArr, string[] carCubeArr, string[] actionTypeArr,string tzrFlag,ref string msg)
        {

            lock (obj)
            {
                using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        msg = "";
                        PublicService ps = new PublicService();
                        //查看是否符合分车标准
                        TZRalation tz;
                        //for (int i = 0; i < carIDArr.Length; i++)
                        //{
                        //    if (string.IsNullOrEmpty(carIDArr[i]))
                        //        continue;
                        //    ztl = this.Query().Where(p => p.CarID == carIDArr[i] && p.IsCompleted == false && p.IsLock != "2").FirstOrDefault();
                        //    if (ztl != null)
                        //    {
                        //        //存在转退料记录
                        //        decimal sum = (string.IsNullOrEmpty(carCubeArr[i]) ? 0.00m : Convert.ToDecimal(carCubeArr[i])) + ztl.Cube;
                        //        //查找车容重
                        //        Car car = ps.Car.Query().Where(p => p.ID == carIDArr[i]).FirstOrDefault();
                        //        if (car == null)
                        //        {
                        //            msg += "没有找到" + carIDArr[i] + "号车</br>";
                        //            break;
                        //        }
                        //        if (car.MaxCube < sum)
                        //        {
                        //            //不能大于车辆最大容重
                        //            msg += string.Format("{0}车有转退料{1}方,超过了最大容重{2}。</br>", carIDArr[i], ztl.Cube, car.MaxCube);
                        //            break;
                        //        }
                        //        else
                        //        { 

                        //        }
                        //    }
                        //}
                        //if (!string.IsNullOrEmpty(msg))
                        //{
                        //    return false;
                        //}



                        //查看是否为完成
                        TZRalation _tz = new TZRalation();
                        _tz.ID = Convert.ToInt32(id);
                        List<TZRalation> list = this.Query().Where(m => m.ID == Convert.ToInt32(id)).ToList();
                        if (list == null || list.Count == 0)
                        {
                            msg += "没有找到转退料信息，请重新刷新。";
                            return false;
                        }
                        if (list[0].IsCompleted == true)
                        {
                            msg += "不能修改已完成的转退料信息。";
                            return false;
                        }
                        //if (list[0].IsLock == "1")
                        //{
                        //    msg = "此转退料已锁定。";
                        //    return false;
                        //}
                        if (list[0].IsLock == "2")
                        {
                            msg += "不能修改合车的转退料信息。";
                            return false;
                        }

                        bool isSame = false;
                        DateTime ts = DateTime.Now;
                        for (int i = 0; i < carIDArr.Length; ++i)
                        {
                            decimal sum = 0.00m;
                            bool Flag = false;

                            if (!string.IsNullOrEmpty(carIDArr[i]) && !string.IsNullOrEmpty(carCubeArr[i]) && !string.IsNullOrEmpty(actionTypeArr[i]))
                            {
                                tz = null;
                                tz = this.Query().Where(p => p.CarID == carIDArr[i] && p.IsCompleted == false && (p.IsLock != "2" || p.IsLock != "3")).FirstOrDefault();
                                if (tz != null)
                                {
                                    Flag = true;
                                    //存在转退料记录 如果目标车和源车是同一车则不能相加
                                    //if (tz.CarID == carIDArr[i])
                                    if (list[0].CarID == carIDArr[i])
                                    {
                                        //同一车
                                        sum = string.IsNullOrEmpty(carCubeArr[i]) ? 0.00m : Convert.ToDecimal(carCubeArr[i]);
                                        isSame = true;//同车标识，有同车则不删除老的转退料。
                                    }
                                    else
                                    {
                                        sum = (string.IsNullOrEmpty(carCubeArr[i]) ? 0.00m : Convert.ToDecimal(carCubeArr[i])) + tz.Cube;
                                    }
                                    //查找车容重
                                    Car car = ps.Car.Query().Where(p => p.ID == carIDArr[i]).FirstOrDefault();
                                    if (car == null)
                                    {
                                        msg += "没有找到" + carIDArr[i] + "号车</br>";
                                        break;
                                    }
                                    if (car.MaxCube < sum)
                                    {
                                        //不能大于车辆最大容重
                                        msg += string.Format("{0}车有转退料{1}方,超过了最大容重{2}。</br>", carIDArr[i], tz.Cube, car.MaxCube);
                                        break;
                                    }
                                }
                                else
                                {
                                    tz = new TZRalation();
                                    sum = string.IsNullOrEmpty(carCubeArr[i]) ? 0.00m : Convert.ToDecimal(carCubeArr[i]);
                                    Flag = false;
                                }

                                tz.CarID = carIDArr[i];
                                tz.Cube = sum;
                                tz.ActionType = actionTypeArr[i];
                                tz.ActionCube = tz.Cube;
                                tz.TzRalationFlag = tzrFlag;

                                //tz.SourceShipDocID = sourceShipDocID;
                                //tz.SourceCube = sourceCube;
                                //tz.ReturnType = returnType;

                                //不相等说明这个有转退料 不要动里面的源信息
                                if (tz.SourceShipDocID == list[0].SourceShipDocID)
                                {
                                    //相等说明源不变 不用动
                                }
                                else
                                {
                                    //不想等说明源变化了 不要动

                                    if (string.IsNullOrEmpty(tz.SourceShipDocID))
                                    {
                                        //没有转退料信息的时候给予最初的
                                        tz.SourceShipDocID = list[0].SourceShipDocID;
                                        tz.SourceCube = list[0].SourceCube;
                                        tz.ReturnType = list[0].ReturnType;

                                    }
                                }


                                if (tz.ActionType == Model.Enums.ActionType.Reject)
                                {
                                    tz.IsCompleted = true;

                                }
                                else
                                {
                                    tz.IsCompleted = false;
                                }
                                tz.AH = Model.Enums.Consts.Handle;
                                tz.ReturnReason = returnReason;

                                tz.ActionTime = ts;
                                //tz.ActionCube = string.IsNullOrEmpty(carCubeArr[i]) ? 0.00m : Convert.ToDecimal(carCubeArr[i]); //tz.SourceCube;
                                tz.IsLock = "1";//分车锁定

                                decimal? value;
                                if (string.IsNullOrEmpty(carCubeArr[i]))
                                    value = null;
                                else
                                    value = Convert.ToDecimal(carCubeArr[i]);

                                if (Flag)
                                {
                                    base.Update(tz,null);

                                    AddHistory(tz, "update", list[0].CarID, value);
                                }
                                else
                                {
                                    base.Add(tz);
                                    AddHistory(tz, "add", list[0].CarID, value);
                                }


                            }
                        }

                        if (!string.IsNullOrEmpty(msg))
                        {
                            tx.Rollback();
                            return false;
                        }

                        //删除老的          
                        if (!isSame)
                        {
                            base.Delete(list[0]);
                            AddHistory(list[0], "delete", list[0].CarID, list[0].Cube);
                        }

                        tx.Commit();
                        
                        return true;
                    }
                    catch (Exception e)
                    {
                        tx.Rollback();
                        logger.Error(e.Message, e); ;
                    }
                }
                return false;
            }
        }


        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="sourceShipDocIDArr"></param>
        /// <param name="sourceCubeArr"></param>
        /// <param name="returnTypeArr"></param>
        /// <param name="returnReason"></param>
        /// <param name="carID"></param>
        /// <param name="cube"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public bool Merge(string[] sourceShipDocIDArr, string[] sourceCubeArr, string[] returnTypeArr, string returnReason, string carID, decimal cube, string actionType)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    DateTime ts = DateTime.Now;
                    for (int i = 0; i < sourceShipDocIDArr.Length; ++i)
                    {
                        if (!string.IsNullOrEmpty(sourceShipDocIDArr[i]) && !string.IsNullOrEmpty(sourceCubeArr[i]) && !string.IsNullOrEmpty(returnTypeArr[i]))
                        {
                            TZRalation tz = new TZRalation();
                            tz.SourceShipDocID = sourceShipDocIDArr[i];
                            tz.SourceCube = decimal.Parse(sourceCubeArr[i]);
                            tz.ReturnType = returnTypeArr[i];
                            tz.CarID = carID;
                            tz.Cube = cube;
                            tz.ActionType = actionType;
                            tz.IsLock = "2";//0未锁定 1锁定 2永久锁定
                            if (tz.ActionType == Model.Enums.ActionType.Reject)
                            {
                                tz.IsCompleted = true;

                            }
                            else
                            {
                                tz.IsCompleted = false;
                            }
                            tz.AH = Model.Enums.Consts.Handle;
                            tz.ReturnReason = returnReason;

                            tz.ActionTime = ts;
                            tz.ActionCube = tz.SourceCube;

                            base.Add(tz);
                        }
                    }

                    tx.Commit();
                    return true;
                }
                catch(Exception e)
                {
                    tx.Rollback();
                    logger.Error(e.Message, e);                   
                }
            }

            return false;
        }

        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="sourceShipDocIDArr">源转退料信息</param>
        /// <param name="sourceCubeArr">源转退料方量</param>
        /// <param name="returnTypeArr"></param>
        /// <param name="returnReason"></param>
        /// <param name="carID"></param>
        /// <param name="cube"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public bool MergeZT(string[] sourceShipDocIDArr, string[] sourceCubeArr, string[] returnTypeArr, string returnReason, string carID, decimal cube, string actionType, string[] returnTypeArrTarget)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    DateTime ts = DateTime.Now;

                    //查找目标本身是否有转退料
                    TZRalation unComplete = this.Query()
                                .Where(p => p.CarID == carID && p.IsCompleted == false && (p.IsLock != "2" || p.IsLock != "3"))
                                .FirstOrDefault();
                    if (unComplete != null)
                    {
                        unComplete.ActionTime = ts;
                        unComplete.ActionCube = cube;

                        cube += unComplete.Cube;
                        unComplete.CarID = carID;
                        unComplete.Cube = cube;
                        unComplete.ActionCube = cube;
                        unComplete.ActionType = actionType;
                        unComplete.IsLock = "2";//合车锁定
                        if (unComplete.ActionType == Model.Enums.ActionType.Reject)
                        {
                            unComplete.IsCompleted = true;

                        }
                        else
                        {
                            unComplete.IsCompleted = false;
                        }
                        unComplete.ReturnType = returnTypeArrTarget != null ? returnTypeArrTarget[0] : "";
                        unComplete.AH = Model.Enums.Consts.Handle;
                        unComplete.ReturnReason = returnReason;

                        AddHistory(unComplete, "update", unComplete.CarID, cube);
                        base.Update(unComplete);
                    }


                    for (int i = 0; i < sourceShipDocIDArr.Length; ++i)
                    {
                        if (!string.IsNullOrEmpty(sourceShipDocIDArr[i]) && !string.IsNullOrEmpty(sourceCubeArr[i]) && !string.IsNullOrEmpty(returnTypeArr[i]))
                        {
                            TZRalation tz = this.Query()
                                .Where(p => p.ID == Convert.ToInt32(sourceShipDocIDArr[i]) && (p.IsLock != "2" || p.IsLock != "3") && p.IsCompleted == false)
                                .FirstOrDefault();
                            if (tz == null)
                                continue;

                            //历史
                            string _carid = tz.CarID;

                            tz.CarID = carID;
                            tz.Cube = cube;
                            tz.ActionType = actionType;
                            tz.IsLock = "2";//合车锁定
                            if (tz.ActionType == Model.Enums.ActionType.Reject)
                            {
                                tz.IsCompleted = true;

                            }
                            else
                            {
                                tz.IsCompleted = false;
                            }
                            tz.ReturnType = returnTypeArr[i];
                            tz.AH = Model.Enums.Consts.Handle;
                            tz.ReturnReason = returnReason;

                            tz.ActionTime = ts;
                            //tz.ActionCube = cube;
                            tz.ActionCube = string.IsNullOrEmpty(sourceCubeArr[i]) ? 0.00m : Convert.ToDecimal(sourceCubeArr[i]);


                            decimal? value;
                            if (string.IsNullOrEmpty(sourceCubeArr[i]))
                                value = null;
                            else
                                value = Convert.ToDecimal(sourceCubeArr[i]);
                            AddHistory(tz, "update", _carid, value);

                            base.Update(tz,null);
                        }
                    }



                    tx.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    logger.Error(e.Message, e);
                }
            }

            return false;
        }

        /// <summary>
        /// 插入历史数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operation"></param>
        /// <param name="opertionnum">真实源车车号</param>
        /// <param name="num">方量</param>
        public void AddHistory(TZRalation entity,string operation,string opertionnum,decimal? operationcube)
        {
            //插入历史数据
            TZRalationHistory history = new TZRalationHistory();
            history.ActionCube = entity.ActionCube;
            history.ActionTime = entity.ActionTime;
            history.ActionType = entity.ActionType;
            history.AH = entity.AH;
            history.Auditor = entity.Auditor;
            history.AuditTime = entity.AuditTime;
            history.Builder = entity.Builder;
            history.BuildTime = entity.BuildTime;
            history.CarID = entity.CarID;
            history.CarWeight = entity.CarWeight;
            history.Cube = entity.Cube;
            history.DealMan = entity.DealMan;
            history.DealTime = entity.DealTime;
            history.Driver = entity.Driver;
            history.DriverUser = entity.DriverUser;
            history.Exchange = entity.Exchange;
            history.ID = entity.ID;
            history.IsAudit = entity.IsAudit;
            history.IsCompleted = entity.IsCompleted;

            if (operation == "delete")
                history.IsLock = "-1";
            else
                history.IsLock = entity.IsLock;

            history.Lifecycle = entity.Lifecycle;
            history.Modifier = entity.Modifier;
            history.ModifyTime = entity.ModifyTime;
            history.ReturnReason = entity.ReturnReason;
            history.ReturnType = entity.ReturnType;
            history.ShippingDocument = entity.ShippingDocument;
            history.ShippingDocuments = entity.ShippingDocuments;
            //history.SourceCarID = entity.SourceCarID;
            //history.SourceConStrength = entity.SourceConStrength;
            history.SourceCube = entity.SourceCube;
            //history.SourceProjectName = entity.SourceProjectName;
            history.SourceShipDocID = entity.SourceShipDocID;
            history.SourceShippingDocument = entity.SourceShippingDocument;
            //history.TargetConStrength = entity.TargetConStrength;
            //history.TargetProjectName = entity.TargetProjectName;
            history.TargetShipDocID = entity.TargetShipDocID;
            history.TargetShippingDocument = entity.TargetShippingDocument;
            history.TotalWeight = entity.TotalWeight;
            history.Version = entity.Version;
            history.Weight = entity.Weight;
            history.ParentID = entity.ID;
            history.operation = operation;
            history.operationnum = opertionnum;
            history.operationcube = operationcube;
            history.TzRalationFlag = entity.TzRalationFlag;

            s = (s == null ? (new PublicService()) : s);
            s.TZRalationHistory.Add(history);
        }
    }
}
