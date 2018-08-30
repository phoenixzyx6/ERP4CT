using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    public abstract class _CarLeaseCostSet : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(CostItemName);
            sb.Append(Meno);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("费用名称")]
        [StringLength(150)]
        public virtual string CostItemName
        {
            get;
            set;
        }
        /// <summary>
        /// 单位
        /// </summary>
        [DisplayName("单位")]
        [StringLength(150)]
        public virtual string Unit
        {
            get;
            set;
        }
        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        public virtual decimal UnitPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 数量
        /// </summary>
        [DisplayName("数量")]
        public virtual decimal Amount
        {
            get;
            set;
        }
        /// <summary>
        /// 金额
        /// </summary>
        [DisplayName("金额")]
        public virtual decimal Money
        {
            get;
            set;
        }
        /// <summary>
        /// 发生时间
        /// </summary>
        [DisplayName("发生时间")]
        public virtual DateTime CostDate
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(350)]
        public virtual string Meno
        {
            get;
            set;
        }
        /// <summary>
        /// 车号
        /// </summary>
        [DisplayName("车号")]
        [StringLength(350)]
        public virtual string CarID
        {
            get;
            set;
        }
        #endregion
    }
}
