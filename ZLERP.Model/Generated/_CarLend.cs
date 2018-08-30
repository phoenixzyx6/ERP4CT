
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 车辆出租表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CarLend : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(LendTime);
			sb.Append(LendIner);
			sb.Append(LendOuter);
			sb.Append(ProductDate);
			sb.Append(ProjectAddr);
			sb.Append(ProjectName);
			sb.Append(ConsPos);
			sb.Append(LendPrice);
			sb.Append(PriceType);
			sb.Append(CurSign);
			sb.Append(MotorSign);
			sb.Append(MangSign);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 出租时间
        /// </summary>
        [DisplayName("出租时间")]
        [Required]
        public virtual System.DateTime LendTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 租入办理人
        /// </summary>
        [DisplayName("租入办理人")]
        [StringLength(30)]
        public virtual string LendIner
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 租出办理人
        /// </summary>
        [DisplayName("租出办理人")]
        [StringLength(30)]
        [Required]
        public virtual string LendOuter
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 施工日期
        /// </summary>
        [DisplayName("施工日期")]
        public virtual System.DateTime? ProductDate
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
        /// 工程名称
        /// </summary>
        [DisplayName("工程名称")]
        [StringLength(128)]
        public virtual string ProjectName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 施工部位
        /// </summary>
        [DisplayName("施工部位")]
        [StringLength(50)]
        public virtual string ConsPos
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 租车单价
        /// </summary>
        [DisplayName("租车单价")]
        [Required]
        public virtual decimal? LendPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 单价类型
        /// </summary>
        [DisplayName("单价类型")]
        [StringLength(20)]
        public virtual string PriceType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 当班签字
        /// </summary>
        [DisplayName("当班签字")]
        [StringLength(30)]
        public virtual string CurSign
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 车队签字
        /// </summary>
        [DisplayName("车队签字")]
        [StringLength(30)]
        public virtual string MotorSign
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 经理签字
        /// </summary>
        [DisplayName("经理签字")]
        [StringLength(30)]
        public virtual string MangSign
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual Customer Customer
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<CarLendItem> CarLendItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
