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
    public class TZRalationHistoryService : ServiceBase<TZRalationHistory>
    {
        internal TZRalationHistoryService(IUnitOfWork uow)
            : base(uow) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override TZRalationHistory Add(TZRalationHistory entity)
        {
            return base.Add(entity);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operation"></param>
        /// <param name="opertionnum">真实源车车号</param>
        /// <param name="num">方量</param>
        public void AddHistory(TZRalation entity, string operation, string opertionnum, decimal? operationcube,string state)
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

            //if (operation == "delete")
            //    history.IsLock = "-1";
            //else
            //    history.IsLock = entity.IsLock;
            history.IsLock = state;

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

            base.Add(history);
        }
    }
}
