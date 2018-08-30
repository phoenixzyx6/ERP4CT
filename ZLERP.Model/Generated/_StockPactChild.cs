
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public abstract class _StockPactChild : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(PayMoney);
            sb.Append(StockPactID);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        [ScriptIgnore]
        public virtual StuffInfo StuffInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 合同ID
        /// </summary>
        [DisplayName("合同ID")]
        public virtual string StockPactID
        {
            get;
			set;			 
        }

        /// <summary>
        /// 付款金额
        /// </summary>
        [Required]
        [DisplayName("付款金额")]
        public virtual decimal PayMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 付款时间
        /// </summary>
        [Required] 
        [DisplayName("付款时间")]
        public virtual DateTime? PayTime
        {
            get;
            set;
        }

        [Required, DisplayName("采购材料")]
        public virtual string StuffID
        {
            get;
            set;
        }
        #endregion
    }
}
