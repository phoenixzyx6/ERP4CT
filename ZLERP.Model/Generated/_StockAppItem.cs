
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  采购申请明细抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _StockAppItem : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(StuffTypeID);
			sb.Append(StuffID);
			sb.Append(AppNum);
			sb.Append(QualityNeed);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 材料类型编号
        /// </summary>
        [DisplayName("材料类型编号")]
        [StringLength(50)]
        public virtual string StuffTypeID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 原料编号
        /// </summary>
        [DisplayName("原料编号")]
        [StringLength(30)]
        public virtual string StuffID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 申请数量
        /// </summary>
        [DisplayName("申请数量")]
        public virtual decimal? AppNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 质量要求
        /// </summary>
        [DisplayName("质量要求")]
        [StringLength(256)]
        public virtual string QualityNeed
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
        [ScriptIgnore]
		public virtual StockApply StockApply
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
