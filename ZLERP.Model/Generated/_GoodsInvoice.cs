using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated
{
    public abstract class _GoodsInvoice : EntityBase<int?>
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
        /// <summary>
        /// 供应商
        /// </summary>
        [DisplayName("供应商")]
        [Required]
        public virtual string SupplyID
        {
            get;
            set;
        }
        /// <summary>
        /// 收取时间
        /// </summary>
        [DisplayName("收取时间")]
        [Required]
        public virtual DateTime? InvoiceDate
        {
            get;
            set;
        }
        /// <summary>
        /// 发票金额
        /// </summary>
        [DisplayName("发票金额")]
        [Required]
        public virtual decimal? InvoiceMoney
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
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public virtual string Meno
        {
            get;
            set;
        }
         /// <summary>
        /// 物资类别
        /// </summary>
        [DisplayName("物资类别")]
        public virtual string GoodsTypeId
        {
            get;
            set;
        }
        
    }
}
