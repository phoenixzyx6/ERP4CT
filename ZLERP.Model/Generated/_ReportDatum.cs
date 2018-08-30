
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
    public abstract class _ReportDatum : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(Stencil);
			sb.Append(ObjectID);
			sb.Append(ReportDataContent);
			sb.Append(ReportType);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public virtual string Stencil
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public virtual string ObjectID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        
        public virtual string ReportDataContent
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public virtual string ReportType
        {
            get;
			set;			 
        }	
        #endregion
    }
}
