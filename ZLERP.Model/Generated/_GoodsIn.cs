
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 物资入库记录抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _GoodsIn : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(SupplyName);
			sb.Append(TransportName);
			sb.Append(InNum);
			sb.Append(InPrice);
			sb.Append(InTime);
			sb.Append(Operator);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 合同ID
        /// </summary>
        [DisplayName("合同ID")]
        public virtual int? PurchaseContract_ID
        {
            get;
            set;
        }

        /// <summary>
        /// 合同名称
        /// </summary>
        [DisplayName("合同名称")]
        [StringLength(60)]
        public virtual string PurchaseContract_Name
        {
            get;
            set;
        }	

        /// <summary>
        /// 供应商
        /// </summary>
        [Required]
        [DisplayName("供应商")]
        [StringLength(50)]
        public virtual string SupplyName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 运输商
        /// </summary>
        [DisplayName("运输商")]
        [StringLength(50)]
        [Required]
        public virtual string TransportName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 入库数量
        /// </summary>
        [Required]
        [DisplayName("入库数量")]
        [Range(0.00001, double.MaxValue, ErrorMessage = "入库数量必须大于等于0！")]
        public virtual decimal InNum
        {
            get;
            set;
        }	
        /// <summary>
        /// 实际单价
        /// </summary>
        [DisplayName("实际单价")]
        [Required]
        [Range(0.000001, float.MaxValue, ErrorMessage = "实际单价必须大于0")]
        public virtual decimal InPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 入库时间
        /// </summary>
        [Required]
        [DisplayName("入库时间")]
        public virtual System.DateTime InTime
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
