
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
    public abstract class _ProjectRoute : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ProjectId);
			sb.Append(IsPrimary);
			sb.Append(Times);
            sb.Append(Source);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 工地编号
        /// </summary>
        [Required]
        [DisplayName("工地编号")]
        [StringLength(30)]
        public virtual string ProjectId
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否主线
        /// </summary>
        [DisplayName("主线")]
        public virtual bool? IsPrimary
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 频次
        /// </summary>
        [DisplayName("频次")]
        public virtual int? Times
        {
            get;
			set;			 
        }

        /// <summary>
        /// 来源
        /// </summary>
        [DisplayName("来源")]
        public virtual string Source
        {
            get;
            set;
        }	


        [ScriptIgnore]
		public virtual IList<RouteDetail> RouteDetails
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
