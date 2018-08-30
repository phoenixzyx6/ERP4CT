
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
    public abstract class _EquipmentItem : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(Days);
			sb.Append(LatelyMaint);
			sb.Append(NextTimeMaint);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 期限（天）
        /// </summary>
        [DisplayName("期限（天）")]
        public virtual int? Days
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 最近保养日
        /// </summary>
        [DisplayName("最近保养日")]
        public virtual System.DateTime? LatelyMaint
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 下次保养日期
        /// </summary>
        [DisplayName("下次保养日期")]
        public virtual System.DateTime? NextTimeMaint
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual Equipment Equipment
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual Maintenance Maintenance
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
