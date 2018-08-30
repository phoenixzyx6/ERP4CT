
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
    public abstract class _MsgUser : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(UserID);
			sb.Append(MsgID);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public virtual string UserID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(30)]
        public virtual string MsgID
        {
            get;
			set;			 
        }

        public virtual User User
        {
            get;
            set;
        }

        public virtual Msg Msg
        {
            get;
            set;
        }
        #endregion
    }
}
