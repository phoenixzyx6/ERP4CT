
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public abstract class _PriceSetting : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 改价时间
        /// </summary>
        [DisplayName("改价时间")]
        public virtual DateTime ChangeTime
        {
            get;
            set;
        }
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
        /// 非泵价格-》单价
        /// </summary>
        [DisplayName("单价")]
        public virtual decimal? UnPumpPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 泵送费-》增加费
        /// </summary>
        [DisplayName("增加费")]
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
         /// <summary>
        /// 运费
        /// </summary>
        [DisplayName("运费")]
        public virtual decimal TranPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 父id
        /// </summary>
        [DisplayName("父id")]
        public virtual string FatherID
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual ContractItem ContractItem
        {
            get;
			set;
        }
       
		
        #endregion
    }
}
