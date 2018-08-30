using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    public abstract class _StuffPayableAdjust : EntityBase<int?>
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
        /// 入库材料金额
        /// </summary>
        [DisplayName("入库材料金额")]
        public virtual decimal? PayBalance
        {
            get;
            set;
        }
        /// <summary>
        /// 垫资金额
        [DisplayName("垫资金额")]
        public virtual decimal? DzMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 欠发票金额
        /// </summary>
        [DisplayName("欠发票金额")]
        public virtual decimal? QianFaPiao
        {
            get;
            set;
        }
        /// <summary>
        /// 1~3月未付款
        /// </summary>
        [DisplayName("1~3月未付款")]
        public virtual decimal? NoPay1
        {
            get;
            set;
        }
        /// <summary>
        /// 4~6月未付款
        /// </summary>
        [DisplayName("4~6月未付款")]
        public virtual decimal? NoPay2
        {
            get;
            set;
        }
        /// <summary>
        /// 6~12月未付款
        /// </summary>
        [DisplayName("6~12月未付款")]
        public virtual decimal? NoPay3
        {
            get;
            set;
        }
        /// <summary>
        /// 12月以上未付款
        /// </summary>
        [DisplayName("12月以上未付款")]
        public virtual decimal? NoPay4
        {
            get;
            set;
        }
         /// <summary>
        /// 供应商
        /// </summary>
        [DisplayName("供应商")]
        public virtual string SupplyID
        {
            get;
            set;
        }
         /// <summary>
        /// 采购单号
        /// </summary>
        [DisplayName("采购单号")]
        public virtual string StockPactID
        {
            get;
            set;
        }
         /// <summary>
        /// 结算时间
        /// </summary>
        [DisplayName("结算时间")]
        public virtual DateTime? AdjustDate
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual SupplyInfo SupplyInfo
        {
            get;
            set;
        }
        
        #endregion
    }
}
