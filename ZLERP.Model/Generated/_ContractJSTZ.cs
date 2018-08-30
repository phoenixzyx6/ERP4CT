
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public abstract class _ContractJSTZ : EntityBase<int?>
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
        /// 调整日期
        /// </summary>
        [DisplayName("调整日期")]
        public virtual DateTime ChangeDate
        {
            get;
			set;			 
        }
        /// <summary>
        /// 调整金额日期范围
        /// </summary>
        [DisplayName("调整金额日期范围")]
        public virtual String JSDate
        {
            get;
            set;
        }
        /// <summary>
        /// 调整类型
        /// </summary>
        [DisplayName("调整类型")]
        public virtual string AdjustType
        {
            get;
            set;
        }
        /// <summary>
        /// 调整金额
        /// </summary>
        [DisplayName("调整金额")]
        public virtual Decimal ChangeMoney
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

       
        [ScriptIgnore]
		public virtual Contract Contract
        {
            get;
			set;
        }
		
		
        #endregion
    }
}
