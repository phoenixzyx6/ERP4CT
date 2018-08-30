
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CheckInfo : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(Checker);
			sb.Append(CheckTime);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 盘点人
        /// </summary>
        [DisplayName("盘点人")]
        [StringLength(30)]
        public virtual string Checker
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 盘点日期
        /// </summary>
        [DisplayName("盘点日期")]
        public virtual System.DateTime? CheckTime
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
		public virtual IList<CheckItem> CheckItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
