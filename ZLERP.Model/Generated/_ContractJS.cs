using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{

    public abstract class _ContractJS : EntityBase<int>
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
        /// 结算日期
        /// </summary>
        [DisplayName("结算日期")]
        public virtual DateTime JSDate
        {
            get;
            set;
        }
        /// <summary>
        /// 开始日期
        /// </summary>
        [DisplayName("开始日期")]
        public virtual DateTime StartJSDate
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
        /// 结算金额
        /// </summary>
        [DisplayName("结算金额")]
        public virtual Decimal JSMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 推迟结算(月)
        /// </summary>
        [DisplayName("推迟结算(月)")]
        public virtual int? BSMoney
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
