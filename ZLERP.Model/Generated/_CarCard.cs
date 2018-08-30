
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 车辆卡片抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CarCard : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(CardType);
            sb.Append(Driver);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 卡片类型
        /// </summary>
        [DisplayName("卡片类型")]
        [StringLength(50)]
        public virtual string CardType
        {
            get;
			set;			 
        }

        /// <summary>
        /// 司机
        /// </summary>
        [DisplayName("司机")]
        [StringLength(50)]
        public virtual string Driver
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
		public virtual SupplyInfo SupplyInfo
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
