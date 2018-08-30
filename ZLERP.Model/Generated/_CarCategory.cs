
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
    public abstract class _CarCategory : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(CarCategoryName);
			sb.Append(Version);
			sb.Append(LifeCycle);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 车种名称
        /// </summary>
        [DisplayName("车种名称")]
        [StringLength(50)]
        public virtual string CarCategoryName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 生命周期
        /// </summary>
        [Required]
        [DisplayName("生命周期")]
        public virtual int LifeCycle
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual IList<Car> Cars
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<CarCategoryItem> CarCategoryItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
