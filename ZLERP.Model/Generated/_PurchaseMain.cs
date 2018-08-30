
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public abstract class _PurchaseMain : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(Main_Num);
            sb.Append(GoodsID);
            sb.Append(GoodsName);
            sb.Append(GoodsTypeID);
            sb.Append(GoodsTypeName);
            sb.Append(Main_Sate);
            sb.Append(Main_Price);
            sb.Append(Main_Sumprice);
            sb.Append(Main_Money);
            sb.Append(Main_NoMoney);
            sb.Append(Main_num1);
            sb.Append(Remark);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 采购需求计划
        /// </summary>
        [ScriptIgnore]
        public virtual PurchasePlan PurchasePlan
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<Purchase> Purchases
        {
            get;
            set;
        }


        /// <summary>
        /// 数量
        /// </summary>
        [DisplayName("数量")]
        public virtual decimal Main_Num
        {
            get;
            set;
        }
        /// <summary>
        /// 物资ID
        /// </summary>
        [DisplayName("物资ID")]
        [StringLength(30)]
        public virtual string GoodsID
        {
            get;
            set;
        }
        /// <summary>
        /// 物资名称
        /// </summary>
        [DisplayName("物资名称")]
        [StringLength(50)]
        public virtual string GoodsName
        {
            get;
            set;
        }
        /// <summary>
        /// 物资类型
        /// </summary>
        [DisplayName("物资类型")]
        [StringLength(30)]
        public virtual string GoodsTypeID
        {
            get;
            set;
        }
        /// <summary>
        /// 物资类型名称
        /// </summary>
        [DisplayName("物资类型名称")]
        [StringLength(50)]
        public virtual string GoodsTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 采购状态
        /// </summary>
        [DisplayName("采购状态")]
        public virtual int Main_Sate
        {
            get;
            set;
        }
        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        public virtual decimal Main_Price
        {
            get;
            set;
        }
        /// <summary>
        /// 总价
        /// </summary>
        [DisplayName("总价")]
        public virtual decimal Main_Sumprice
        {
            get;
            set;
        }
        /// <summary>
        /// 开票金额
        /// </summary>
        [DisplayName("开票金额")]
        public virtual decimal Main_Money
        {
            get;
            set;
        }
        /// <summary>
        /// 挂账金额
        /// </summary>
        [DisplayName("现金金额")]
        public virtual decimal Main_NoMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 已购买数量
        /// </summary>
        [DisplayName("已入库数量")]
        public virtual decimal Main_num1
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(300)]
        public virtual string Remark
        {
            get;
            set;
        }

        #endregion
    }
}
