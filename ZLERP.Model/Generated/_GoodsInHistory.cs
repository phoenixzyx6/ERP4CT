
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
    public abstract class _GoodsInHistory : EntityBase<string>
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

        public virtual string GoodsInIDHistory
        {
            get;
            set;
        }
        public virtual string GoodsName
        {
            get;
            set;
        }

        public virtual string GoodsID
        {
            get;
            set;
        }

        public virtual string action_u
        {
            get;
            set;
        }	

        /// <summary>
        /// 供应商
        /// </summary>
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
        [Range(0, int.MaxValue, ErrorMessage = "请输入大于等于0的数。")]
        public virtual decimal InNum
        {
            get;
            set;
        }	
        /// <summary>
        /// 实际单价
        /// </summary>
        [DisplayName("实际单价")]
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
		
        #endregion
    }
}
