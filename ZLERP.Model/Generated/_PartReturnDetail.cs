
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 配件退货明细抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _PartReturnDetail : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(PartID);
			sb.Append(ReturnNum);
			sb.Append(UnitPrice);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 配件编号
        /// </summary>
        [DisplayName("配件编号")]
        [StringLength(30)]
        public virtual string PartID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 退货数量
        /// </summary>
        [DisplayName("退货数量")]
        public virtual decimal? ReturnNum
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
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(50)]
        public virtual string Remark
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual PartReturn PartReturn
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
