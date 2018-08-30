
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public abstract class _PurchasePlan:EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(PurchasePlan_NeedDate);
            sb.Append(GoodsID);
            sb.Append(GoodsName);
            sb.Append(GoodsTypeID);
            sb.Append(GoodsTypeName);
            sb.Append(PurchasePlan_num);
            sb.Append(PurchasePlan_reason);
            sb.Append(PurchasePlan_planstate);
            sb.Append(PurchasePlan_state);
            sb.Append(PurchasePlan_claimer);
            sb.Append(PurchasePlan_auditor);
            sb.Append(PurchasePlan_audit_date);
            sb.Append(PurchasePlan_audit_opinion);
            sb.Append(Remark);
            sb.Append(goodsername);
            sb.Append(goodsertime);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 采购需求时间
        /// </summary>
        [DisplayName("采购需求时间")]
        [Required]
        public virtual DateTime? PurchasePlan_NeedDate
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
        [Required]
        public virtual string GoodsName
        {
            get;
            set;
        }
        /// <summary>
        /// 物资类型ID
        /// </summary>
        [DisplayName("物资类型ID")]
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
        /// 采购数量
        /// </summary>
        [DisplayName("采购数量")]
        [Required]
        //[Range(0.00001, float.MaxValue, ErrorMessage = "请输入大于0的数。")]
        public virtual decimal? PurchasePlan_num
        {
            get;
            set;
        }
        /// <summary>
        /// 事由
        /// </summary>
        [DisplayName("事由")]
        [StringLength(200)]
        [Required]
        public virtual string PurchasePlan_reason
        {
            get;
            set;
        }
        /// <summary>
        /// 计划执行状态 0未提交（本人未提交） 1计划阶段（本人提交后） 2采购阶段（领导审批完毕） 3入库阶段（第一批次采购完毕） 4完成（采购计划完毕）
        /// </summary>
        [DisplayName("计划执行状态")]
        public virtual int PurchasePlan_planstate
        {
            get;
            set;
        }
        /// <summary>
        /// 审核状态 0草稿 1提交 2审核通过 3审核不通过  4物资员审核通过 5物资员审核不通过
        /// </summary>
        [DisplayName("审核状态")]
        public virtual int PurchasePlan_state
        {
            get;
            set;
        }
        /// <summary>
        /// 申请人
        /// </summary>
        [DisplayName("申请人")]
        [StringLength(12)]
        public virtual string PurchasePlan_claimer
        {
            get;
            set;
        }
        /// <summary>
        /// 审核人
        /// </summary>
        [DisplayName("审核人")]
        [StringLength(12)]
        public virtual string PurchasePlan_auditor
        {
            get;
            set;
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        [DisplayName("审核时间")]
        public virtual DateTime? PurchasePlan_audit_date
        {
            get;
            set;
        }
        /// <summary>
        /// 审核意见
        /// </summary>
        [DisplayName("审核意见")]
        [StringLength(200)]
        public virtual string PurchasePlan_audit_opinion
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
        /// 物资员名称
        /// </summary>
        [DisplayName("物资员名称")]
        [StringLength(20)]
        public virtual string goodsername
        {
            get;
            set;
        }

        /// <summary>
        /// 物资员审核时间
        /// </summary>
        [DisplayName("物资员审核时间")]
        public virtual DateTime? goodsertime
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("预估价格")]
        public virtual decimal? planmoney
        {
            get;
            set;
        }

        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        public virtual decimal? planprice
        {
            get;
            set;
        }
        /// <summary>
        /// 单位
        /// </summary>
        [DisplayName("单位")]
        public virtual string Unit
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual GoodsInfo GoodsInfo
        {
            get;
            set;
        }
        #endregion
    }
}
