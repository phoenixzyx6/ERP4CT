
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
    public abstract class _SystemMsg : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(OperateObj);
			sb.Append(MsgTitle);
			sb.Append(SendTime);
			sb.Append(Sender);
			sb.Append(MsgType);
			sb.Append(DealStatus);
			sb.Append(Assignee);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 操作对象
        /// </summary>
        [DisplayName("操作对象")]
        [StringLength(4000)]
        public virtual string OperateObj
        {
            get;
			set;
        }	
        /// <summary>
        /// 主题
        /// </summary>
        [Required]
        [StringLength(128)]
        [DisplayName("主题")]
        public virtual string MsgTitle
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 发送时间
        /// </summary>
        [DisplayName("发送时间")]
        public virtual System.DateTime? SendTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 发送者
        /// </summary>
        [StringLength(30)]
        [DisplayName("发送者")]
        public virtual string Sender
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 消息类型
        /// </summary>
        [StringLength(50)]
        [DisplayName("消息类型")]
        public virtual string MsgType
        {
            get;
			set;			 
        }	

        /// <summary>
        /// 处理状态(0、正常数据(已发送)；1、草稿箱；-1、已删除)
        /// </summary>
        public virtual int? DealStatus
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 受理者
        /// </summary>
        [StringLength(30)]
        public virtual string Assignee
        {
            get;
			set;			 
        }

        [ScriptIgnore]
        public virtual IList<MyMsg> MyMsgs
        {
            get;
            set;
        }
        #endregion
    }
}
