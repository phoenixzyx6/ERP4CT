
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public abstract class _PurchaseContract : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(PurchaseContract_Name);
            sb.Append(GoodsID);
            sb.Append(GoodsName);
            sb.Append(PurchaseContract_SupplyTel);
            sb.Append(PurchaseContract_Supply);
            sb.Append(PurchaseContract_Price);
            sb.Append(PurchaseContract_Num);
            sb.Append(PurchaseContract_Money);
            sb.Append(PurchaseContract_One);
            sb.Append(PurchaseContract_Tow);
            sb.Append(PurchaseContract_Date);
            sb.Append(PurchaseContract_StartDate);
            sb.Append(PurchaseContract_EndDate);
            sb.Append(Remark);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 采购子表
        /// </summary>
        [ScriptIgnore]
        public virtual Purchase Purchase
        {
            get;
            set;
        }

        /// <summary>
        /// 采购子表id
        /// </summary>
        [DisplayName("采购子表id")]
        public virtual int Purchase_ID
        {
            get;
            set;
        }

        /// <summary>
        /// 合同名称
        /// </summary>
        [DisplayName("合同名称")]
        [StringLength(200)]
        public virtual string PurchaseContract_Name
        {
            get;
            set;
        }

        /// <summary>
        /// 供货单位电话
        /// </summary>
        [DisplayName("供货单位电话")]
        [StringLength(20)]
        public virtual string PurchaseContract_SupplyTel
        {
            get;
            set;
        }
        /// <summary>
        /// 供货单位
        /// </summary>
        [DisplayName("供货单位")]
        [StringLength(50)]
        [Required]
        public virtual string PurchaseContract_Supply
        {
            get;
            set;
        }
        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        public virtual decimal PurchaseContract_Price
        {
            get;
            set;
        }
        /// <summary>
        /// 数量
        /// </summary>
        [DisplayName("数量")]
        public virtual decimal PurchaseContract_Num
        {
            get;
            set;
        }
        /// <summary>
        /// 总价
        /// </summary>
        [DisplayName("总价")]
        public virtual decimal PurchaseContract_Money
        {
            get;
            set;
        }
        /// <summary>
        /// 甲方人
        /// </summary>
        [DisplayName("甲方人")]
        [StringLength(50)]
        public virtual string PurchaseContract_One
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方人
        /// </summary>
        [DisplayName("乙方人")]
        [StringLength(50)]
        public virtual string PurchaseContract_Tow
        {
            get;
            set;
        }
        /// <summary>
        /// 签订日期
        /// </summary>
        [DisplayName("签订日期")]
        public virtual DateTime PurchaseContract_Date
        {
            get;
            set;
        }
        /// <summary>
        /// 供货起始时间
        /// </summary>
        [DisplayName("供货起始时间")]
        public virtual DateTime PurchaseContract_StartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 供货截至时间
        /// </summary>
        [DisplayName("供货截至时间")]
        public virtual DateTime PurchaseContract_EndDate
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
        /// 物质名称
        /// </summary>
        [DisplayName("物质名称")]
        [StringLength(50)]
        public virtual string GoodsName
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

        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public virtual int PurchaseContract_state
        {
            get;
            set;
        }

        /// <summary>
        /// 供货单位电话
        /// </summary>
        [DisplayName("供货单位电话")]
        [StringLength(20)]
        public virtual string PurchaseContract_SupplyTel1
        {
            get;
            set;
        }
        /// <summary>
        /// 供货单位
        /// </summary>
        [DisplayName("供货单位")]
        [StringLength(50)]
        public virtual string PurchaseContract_Supply1
        {
            get;
            set;
        }
        /// <summary>
        /// 供货单位ID
        /// </summary>
        [DisplayName("供货单位ID")]
        [StringLength(50)]
        public virtual string PurchaseContract_SupplyID
        {
            get;
            set;
        }
        /// <summary>
        /// 运输单位ID
        /// </summary>
        [DisplayName("运输单位ID")]
        [StringLength(50)]
        public virtual string PurchaseContract_SupplyID1
        {
            get;
            set;
        }
        #endregion
    }
}
