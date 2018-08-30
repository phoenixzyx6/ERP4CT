
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 膨胀剂试验抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _AdvanceMoney : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(moneytype);
            sb.Append(typeorder);
            sb.Append(num);
            sb.Append(numtime);
            sb.Append(bilv);
            sb.Append(ContractID);
            sb.Append(BY1);
            sb.Append(Contract);
            sb.Append(CurrentDate);	
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public virtual int typeorder
        {
            get;
            set;
        }

        /// <summary>
        /// 类型：金额 方量
        /// </summary>
        [DisplayName("垫资类型")]
        [StringLength(20)]
        public virtual string moneytype
        {
            get;
            set;
        }
        /// <summary>
        /// 数量
        /// </summary>
        [DisplayName("数量")]
        public virtual decimal? num
        {
            get;
            set;
        }
        /// <summary>
        /// 时长(天)
        /// </summary>
        [DisplayName("时长(天)")]
        public virtual decimal? numtime
        {
            get;
            set;
        }
        /// <summary>
        /// 比率
        /// </summary>
        [DisplayName("比率")]
        public virtual decimal? bilv
        {
            get;
            set;
        }
        /// <summary>
        /// 合同编码
        /// </summary>
        [DisplayName("合同编码")]
        [StringLength(30)]
        public virtual string ContractID
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual Contract Contract
        {
            get;
            set;
        }
        /// <summary>
        /// 备用1
        /// </summary>
        [DisplayName("备用1")]
        [StringLength(50)]
        public virtual string BY1
        {
            get;
            set;
        }
        /// <summary>
        /// 当前时间
        /// </summary>
        [DisplayName("当前时间")]
        public virtual System.DateTime? CurrentDate
        {
            get;
			set;			 
        }	
		 
		
        #endregion
    }
}
