
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 司机辅助作业抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CarAss : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(AssDate);
			sb.Append(AssType);
			sb.Append(AssTimes);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 出车时间
        /// </summary>
        [Required]
        [DisplayName("出车时间")]
        public virtual System.DateTime AssDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 辅助类型
        /// </summary>
        [DisplayName("辅助类型")]
        [StringLength(50)]
        public virtual string AssType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 趟次
        /// </summary>
        [Required]
        [DisplayName("趟次")]
        public virtual int AssTimes
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
		public virtual Car Car
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual ProduceTask ProduceTask
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual User User
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
