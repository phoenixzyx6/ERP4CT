
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  车辆信息抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CarChangeLog : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(Builder);
            sb.Append(BuildTime);
            sb.Append(Version);
    
			
            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        
        /// <summary>
        /// 车辆类型编号
        /// </summary>
        [DisplayName("调整前")]
        [StringLength(255)]
        public virtual string beforeNum
        {
            get;
			set;			 
        }

        /// <summary>
        /// 车辆类型编号
        /// </summary>
        [DisplayName("调整后")]
        [StringLength(255)]
        public virtual string afterNum
        {
            get;
            set;
        }	
		 
		
        #endregion
    }
}
