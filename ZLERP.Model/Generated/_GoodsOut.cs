
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 物资领用记录抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _GoodsOut : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(OutMan);
			sb.Append(OutNum);
			sb.Append(OutTime);
			sb.Append(OutReason);
			sb.Append(Operator);
			sb.Append(Remark);
            sb.Append(IsR);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 是否同步到维修 0不同步 1同步到车辆维修 2同步到设备维修 3都同步
        /// </summary>
        [DisplayName("是否同步到维修")]
        public virtual int IsR
        {
            get;
            set;
        }	

        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        public virtual decimal price
        {
            get;
            set;
        }	

        /// <summary>
        /// 领用人
        /// </summary>
        [DisplayName("领用人")]
        [StringLength(50)]
        public virtual string OutMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 领用数量
        /// </summary>
        [Required]
        [DisplayName("领用数量")]
        [Range(0, int.MaxValue, ErrorMessage = "请输入大于等于0的数。")]
        public virtual decimal OutNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 领用时间
        /// </summary>
        [Required]
        [DisplayName("领用时间")]
        public virtual System.DateTime OutTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 领用原因
        /// </summary>
        [DisplayName("领用原因")]
        [StringLength(128)]
        public virtual string OutReason
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 经办人
        /// </summary>
        [DisplayName("经办人")]
        [StringLength(50)]
        public virtual string Operator
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(128)]
        public virtual string Remark
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual GoodsInfo GoodsInfo
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
