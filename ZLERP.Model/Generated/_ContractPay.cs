
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public abstract class _ContractPay : EntityBase<int?>
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
        /// 付款日期
        /// </summary>
        [DisplayName("付款日期")]
        public virtual DateTime PayDate
        {
            get;
			set;			 
        }

        /// <summary>
        /// 付款类型
        /// </summary>
        [DisplayName("付款类型")]
        public virtual String PayType
        {
            get;
            set;
        }

        /// <summary>
        /// 付款金额
        /// </summary>
        [DisplayName("付款金额")]
        public virtual Decimal PayMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public virtual string Remark
        {
            get;
            set;
        }
        /// <summary>
        /// 经办人
        /// </summary>
        [DisplayName("经办人")]
        public virtual string Manager
        {
            get;
            set;
        }
       
        [ScriptIgnore]
		public virtual Contract Contract
        {
            get;
			set;
        }
		
		
        #endregion
    }
}
