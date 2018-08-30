
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public abstract class _Remindinfo : EntityBase<int>
    { 
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(DispatchID);
            sb.Append(ShipDocID);
            sb.Append(StuffID);
            sb.Append(StuffName);
            sb.Append(ProjectID);
            sb.Append(ProjectName);
            sb.Append(Status);
            sb.Append(ErrorValue);
            sb.Append(TaskID);
            sb.Append(PotTimes);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 调度id
        /// </summary>
        //[DisplayName("调度ID")]
        //[StringLength(30)]
        public virtual string DispatchID
        {
            get;
            set;
        }
        /// <summary>
        /// 运输单ID
        /// </summary>
        //[DisplayName("运输单ID")]
        //[StringLength(30)]
        public virtual string ShipDocID
        {
            get;
            set;
        }
        /// <summary>
        /// 原料ID
        /// </summary>
        //[DisplayName("原料ID")]
        //[StringLength(30)]
        public virtual string StuffID
        {
            get;
            set;
        }

        /// <summary>
        /// 原料
        /// </summary>
        //[DisplayName("原料")]
        //[StringLength(50)]
        public virtual string StuffName
        {
            get;
            set;
        }
        /// <summary>
        /// 工程ID
        /// </summary>
        //[DisplayName("工程ID")]
        //[StringLength(30)]
        public virtual string ProjectID
        {
            get;
            set;
        }
        /// <summary>
        /// 工程
        /// </summary>
        //[DisplayName("工程")]
        //[StringLength(128)]
        public virtual string ProjectName
        {
            get;
            set;
        }
        /// <summary>
        /// 状态
        /// </summary>
        //[DisplayName("状态")]
        //[StringLength(10)]
        public virtual string Status
        {
            get;
            set;
        }
        /// <summary>
        /// 值
        /// </summary>
        //[DisplayName("值")]
        public virtual int? ErrorValue
        {
            get;
            set;
        }
        /// <summary>
        /// 任务单
        /// </summary>
        //[DisplayName("任务单")]
        //[StringLength(30)]
        public virtual string TaskID
        {
            get;
            set;
        }

        /// <summary>
        /// 盘数
        /// </summary>
        public virtual int? PotTimes
        {
            get;
            set;
        }

        #endregion
    }
}
