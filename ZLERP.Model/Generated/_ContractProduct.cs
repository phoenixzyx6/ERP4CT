using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{

    public abstract class _ContractProduct : EntityBase<int?>
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
        /// 生产日期
        /// </summary>
        [DisplayName("生产日期")]
        public virtual DateTime ProductDate
        {
            get;
            set;
        }

        /// <summary>
        /// 生产类型
        /// </summary>
        [DisplayName("生产类型")]
        public virtual String ProductType
        {
            get;
            set;
        }

        /// <summary>
        /// 生产金额
        /// </summary>
        [DisplayName("生产金额")]
        public virtual Decimal ProductMoney
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
