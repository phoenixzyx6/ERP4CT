
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
    public abstract class _Msg : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(MsgTitle);
			sb.Append(MsgType);
            sb.Append(BelongFuncID);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 消息标题
        /// </summary>
        [StringLength(50)]
        [DisplayName("消息标题")]
        public virtual string MsgTitle
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 消息类别
        /// </summary>
        [StringLength(50)]
        [DisplayName("消息类别")]
        public virtual string MsgType
        {
            get;
			set;			 
        }
	    /// <summary>
        /// 所属模块
        /// </summary>
        [StringLength(50)]
        [DisplayName("所属模块")]
        public virtual string BelongFuncID
        {
            get;
			set;			 
        }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(128)]
        [DisplayName("备注")]
        public virtual string Remark
        {
            get;
			set;			 
        }

        [ScriptIgnore]
        public virtual IList<MsgUser> MsgUsers
        {
            get;
            set;
        }

        #endregion
    }
}
