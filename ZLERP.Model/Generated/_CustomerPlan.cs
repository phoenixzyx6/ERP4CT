
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 工地计划单抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CustomerPlan : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ProjectName);
			sb.Append(ProjectAddr);
			sb.Append(ConsPos);
			sb.Append(ConStrength);
			sb.Append(Slump);
			sb.Append(CastMode);
            sb.Append(PumpName);
            sb.Append(PiepLen);
            sb.Append(PumpMan);
			sb.Append(PlanCube);
			sb.Append(NeedDate);
            sb.Append(PlanDate);
			sb.Append(Tel);
			sb.Append(LinkMan);
			sb.Append(Remark);
			sb.Append(AuditStatus);
			sb.Append(AuditTime);
			sb.Append(AuditInfo);
			sb.Append(Auditor);
			sb.Append(Version);
            //sb.Append(SupplyUnit);
            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties
        [DisplayName("任务单")]
        public virtual string TaskID { get; set; }
        /// <summary>
        /// 工程名称
        /// </summary>
        [Required]
        [DisplayName("工程名称")]
        [StringLength(128)]
        public virtual string ProjectName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 项目地址
        /// </summary>
        [DisplayName("项目地址")]
        [StringLength(128)]
        public virtual string ProjectAddr
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 施工部位
        /// </summary>
        [Required]
        [DisplayName("施工部位")]
        [StringLength(50)]
        public virtual string ConsPos
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砼强度
        /// </summary>
        [Required]
        [DisplayName("砼强度")]
        [StringLength(50)]
        public virtual string ConStrength
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 坍落度
        /// </summary>
        [Required]
        [DisplayName("坍落度")]
        [StringLength(20)]
        public virtual string Slump
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 浇筑方式
        /// </summary>
        [Required]
        [DisplayName("浇筑方式")]
        [StringLength(50)]
        public virtual string CastMode
        {
            get;
			set;			 
        }
        /// <summary>
        /// 泵名称
        /// </summary>
        [DisplayName("泵名称")]
        [StringLength(50)]
        public virtual string PumpName
        {
            get;
            set;
        }
        /// <summary>
        /// 管道长度
        /// </summary>
        [DisplayName("管道长度")]
        [StringLength(50)]
        public virtual string PiepLen
        {
            get;
            set;
        }
        /// <summary>
        /// 泵工
        /// </summary>
        [DisplayName("泵工")]
        [StringLength(50)]
        public virtual string PumpMan
        {
            get;
            set;
        }
        /// <summary>
        /// 计划方量
        /// </summary>
        [Required]
        [DisplayName("计划方量")]
        public virtual decimal PlanCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 到场时间
        /// </summary>
        [Required]
        [DisplayName("到场时间")]
        public virtual System.String NeedDate
        {
            get;
			set;			 
        }
        /// <summary>
        /// 计划日期
        /// </summary>
        [Required]
        [DisplayName("计划日期")]
        public virtual System.DateTime PlanDate
        {
            get;
            set;
        }	
        /// <summary>
        /// 工地电话
        /// </summary>
        [DisplayName("工地电话")]
        [StringLength(20)]
        public virtual string Tel
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 工地联系人
        /// </summary>
        [DisplayName("前场工长")]
        [StringLength(30)]
        public virtual string LinkMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(256)]
        public virtual string Remark
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 审核状态
        /// </summary>
        [DisplayName("审核状态")]
        public virtual int? AuditStatus
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 审核时间
        /// </summary>
        [DisplayName("审核时间")]
        public virtual System.DateTime? AuditTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 审核意见
        /// </summary>
        [DisplayName("审核意见")]
        [StringLength(128)]
        public virtual string AuditInfo
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 审核人
        /// </summary>
        [DisplayName("审核人")]
        [StringLength(30)]
        public virtual string Auditor
        {
            get;
			set;			 
        }
        [DisplayName("区间")]
        [StringLength(30)]
        public virtual string RegionID
        {
            get;
            set;
        }
        [DisplayName("施工单位")]
        [StringLength(256)]
        public virtual string ConstructUnit
        {
            get;
            set;
        }

		public virtual Contract Contract
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual ProduceTask ProduceTask
        {
            get;
			set;
        }
        /// <summary>
        /// 供应单位
        /// </summary>
        [DisplayName("供应单位")]
        [StringLength(128)]
        public virtual string SupplyUnit { get; set; }
        #endregion
    }
}
