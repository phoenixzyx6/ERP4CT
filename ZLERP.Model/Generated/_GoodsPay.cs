
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _GoodsPay : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(SupplyID);
            sb.Append(PayDate);
            sb.Append(PayMoney);
            sb.Append(Remark);
            sb.Append(Version);
            sb.Append(PayType);
            sb.Append(BalanceMoney);
            sb.Append(Manager);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 供应商编号
        /// </summary>
        [Required]
        [DisplayName("供应商编号")]
        [StringLength(30)]
        public virtual string SupplyID
        {
            get;
            set;
        }
        /// <summary>
        /// 付款时间
        /// </summary>
        [DisplayName("付款时间")]
        [Required]       
        public virtual System.DateTime? PayDate
        {
            get;
            set;
        }
        /// <summary>
        /// 付款金额
        /// </summary>
        [DisplayName("付款金额")]
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "必须是大于等于0的数字")]
        public virtual decimal? PayMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(50)]
        public virtual string Remark
        {
            get;
            set;
        }
        /// <summary>
        /// 付款方式
        /// </summary>
        [DisplayName("付款方式")]
        [StringLength(50)]
        public virtual string PayType
        {
            get;
            set;
        }
        /// <summary>
        /// 当前欠款余额
        /// </summary>
        [DisplayName("当前欠款余额")]
        public virtual decimal? BalanceMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 经办人
        /// </summary>
        [DisplayName("经办人")]
        [StringLength(30)]
        public virtual string Manager
        {
            get;
            set;
        }
        #endregion
    }
}
