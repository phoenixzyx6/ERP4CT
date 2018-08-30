
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 车种信息主表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CarClass : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(CarClassName);
			sb.Append(DetailRemark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 车种名称
        /// </summary>
        [DisplayName("车种名称")]
        [StringLength(50)]
        public virtual string CarClassName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 详细说明
        /// </summary>
        [DisplayName("详细说明")]
        [StringLength(256)]
        public virtual string DetailRemark
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual IList<CarClassItem> CarClassItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
