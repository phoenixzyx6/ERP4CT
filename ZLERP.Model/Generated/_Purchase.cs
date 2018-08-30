
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public abstract class _Purchase : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(Purchase_Price);
            sb.Append(GoodsID);
            sb.Append(GoodsName);
            sb.Append(Purchase_Money);
            sb.Append(Purchase_ManTel);
            sb.Append(Purchase_Man);
            sb.Append(Purchase_Date);
            sb.Append(Purchase_StickMoney);
            sb.Append(Purchase_NoMoney);
            sb.Append(Purchase_StickNo);
            sb.Append(Purchase_State);
            sb.Append(Remark);
            sb.Append(Main_ID);
            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 采购主表
        /// </summary>
        [ScriptIgnore]
        public virtual PurchaseMain PurchaseMain
        {
            get;
            set;
        }
        /// <summary>
        /// 主表id
        /// </summary>
        [DisplayName("主表id")]
        public virtual int Main_ID
        {
            get;
            set;
        }

        /// <summary>
        /// 采购合同
        /// </summary>
        [ScriptIgnore]
        public virtual IList<PurchaseContract> PurchaseContracts
        {
            get;
            set;
        }
        /// <summary>
        /// 数量
        /// </summary>
        [DisplayName("数量")]
        public virtual decimal Purchase_Num
        {
            get;
            set;
        }

        /// <summary>
        /// 数量
        /// </summary>
        [DisplayName("入库数量")]
        public virtual decimal Purchase_Num1
        {
            get;
            set;
        }

        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        public virtual decimal Purchase_Price
        {
            get;
            set;
        }
        /// <summary>
        /// 总价
        /// </summary>
        [DisplayName("总价")]
        public virtual decimal Purchase_Money
        {
            get;
            set;
        }
        /// <summary>
        /// 采购人电话
        /// </summary>
        [DisplayName("采购人电话")]
        [StringLength(20)]
        public virtual string Purchase_ManTel
        {
            get;
            set;
        }
        /// <summary>
        /// 采购人
        /// </summary>
        [DisplayName("采购人")]
        [StringLength(12)]
        public virtual string Purchase_Man
        {
            get;
            set;
        }
        /// <summary>
        /// 采购时间
        /// </summary>
        [DisplayName("采购时间")]
        public virtual DateTime Purchase_Date
        {
            get;
            set;
        }
        /// <summary>
        /// 开票金额
        /// </summary>
        [DisplayName("开票金额")]
        public virtual decimal Purchase_StickMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 挂账金额
        /// </summary>
        [DisplayName("挂账金额")]
        public virtual decimal Purchase_NoMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 票据编号
        /// </summary>
        [DisplayName("票据编号")]
        [StringLength(300)]
        public virtual string Purchase_StickNo
        {
            get;
            set;
        }
        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public virtual int Purchase_State
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

        #endregion
    }
}
