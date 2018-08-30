
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 采购合同明细抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _PartStockContractItem : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(StockPlanID);
			sb.Append(UnitPrice);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 采购计划编号
        /// </summary>
        [DisplayName("采购计划编号")]
        [StringLength(30)]
        public virtual string StockPlanID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        public virtual decimal? UnitPrice
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual PartStockContract PartStockContract
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
