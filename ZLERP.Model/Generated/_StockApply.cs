
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  采购申请抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _StockApply : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(Proposer);
			sb.Append(AppTime);
			sb.Append(Remark);
			sb.Append(OrderNum);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 申请人
        /// </summary>
        [DisplayName("申请人")]
        [StringLength(30)]
        public virtual string Proposer
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 申请时间
        /// </summary>
        [DisplayName("申请时间")]
        public virtual System.DateTime? AppTime
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
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public virtual int? OrderNum
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual IList<StockAppItem> StockAppItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
