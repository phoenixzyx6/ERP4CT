
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 车种信息子表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CarClassItem : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(TyrPlace);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 轮胎位置
        /// </summary>
        [DisplayName("轮胎位置")]
        [StringLength(50)]
        public virtual string TyrPlace
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual CarClass CarClass
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
