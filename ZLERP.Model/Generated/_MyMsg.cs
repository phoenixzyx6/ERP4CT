
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
    public abstract class _MyMsg : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(UserID);
			sb.Append(IsRead);
			sb.Append(ReadTime);
			sb.Append(DealStatus);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 收信人
        /// </summary>
        [StringLength(30)]
        [DisplayName("收信人")]
        public virtual string UserID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否已读
        /// </summary>
        [DisplayName("是否已读")]
        public virtual bool? IsRead
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 阅读时间
        /// </summary>
        [DisplayName("阅读时间")]
        public virtual System.DateTime? ReadTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 处理状态
        /// </summary>
        [DisplayName("处理状态")]
        public virtual int? DealStatus
        {
            get;
			set;			 
        }	

		public virtual SystemMsg SystemMsg
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
