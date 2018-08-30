
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 出租车辆明细表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CarLendItem : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(BackTime);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 回厂时间
        /// </summary>
        [DisplayName("回厂时间")]
        public virtual System.DateTime? BackTime
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
		public virtual CarLend CarLend
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<CarLendFoot> CarLendFeet
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
