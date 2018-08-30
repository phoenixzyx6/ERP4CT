
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  车辆司机关系抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _DriverForCar : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(Driver);
			sb.Append(PlanClass);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 是否当班司机
        /// </summary>
        [DisplayName("是否当班司机")]
        public virtual bool Driver
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 计划班组
        /// </summary>
        [DisplayName("计划班组")]
        [StringLength(20)]
        public virtual string PlanClass
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
		 
		
        
		public virtual User User
        {
            get;
			set;
        }
        #endregion
    }
}
