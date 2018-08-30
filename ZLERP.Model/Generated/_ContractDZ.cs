using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{

    public abstract class _ContractDZ : EntityBase<int?>
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
        /// 垫资日期
        /// </summary>
        [DisplayName("垫资日期")]
        public virtual DateTime DZDate
        {
            get;
            set;
        }
        /// <summary>
        /// 类型
        /// </summary>
        [DisplayName("类型")]
        public virtual string Type
        {
            get;
            set;
        }
        /// <summary>
        /// 垫资金额
        /// </summary>
        [DisplayName("垫资金额")]
        public virtual Decimal DZMoney
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

        #endregion
    }
}
