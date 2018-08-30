
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 前场工长记录表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _LinkManRecord : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(Content);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 内容
        /// </summary>
        [DisplayName("内容")]
        [StringLength(256)]
        public virtual string Content
        {
            get;
			set;			 
        }	
        #endregion
    }
}
