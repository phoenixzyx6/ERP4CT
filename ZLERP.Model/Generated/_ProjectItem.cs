
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 针对合同加入砼价格明细，控制合同生产 合同明细抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ProjectItem : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ConStrength);
			sb.Append(UnPumpPrice);
			sb.Append(PumpCost);
			sb.Append(SlurryPrice);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 砼强度
        /// </summary>
        [DisplayName("砼强度")]
        [StringLength(50)]
        public virtual string ConStrength
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 非泵价格
        /// </summary>
        [DisplayName("非泵价格")]
        public virtual decimal? UnPumpPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 泵送费
        /// </summary>
        [DisplayName("泵送费")]
        public virtual decimal? PumpCost
        {
            get;
			set;			 
        }
        /// <summary>
        /// 砂浆价格
        /// </summary>
        [DisplayName("砂浆价格")]
        public virtual decimal? SlurryPrice
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
        public virtual Project Project
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<IdentitySetting> IdentitySettings
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<ProduceTask> ProduceTasks
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
