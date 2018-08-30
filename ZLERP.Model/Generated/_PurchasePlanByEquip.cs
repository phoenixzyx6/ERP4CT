
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public abstract class _PurchasePlanByEquip : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(PurchasePlan_NeedDate);
            sb.Append(GoodsID);
            //sb.Append(BID);
            //sb.Append(MID);
            //sb.Append(SID);
            sb.Append(PurchasePlan_reason);
            sb.Append(PurchasePlan_planstate);
            sb.Append(PurchasePlan_state);
            sb.Append(PurchasePlan_claimer);
            sb.Append(PurchasePlan_auditor);
            sb.Append(PurchasePlan_audit_date);
            sb.Append(PurchasePlan_audit_opinion);
            sb.Append(Remark);
            sb.Append(planmoney);
            sb.Append(EquipMtLyID);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        public virtual string EquipMtLyID
        {
            get;
            set;
        }

        /// <summary>
        /// 申请时间
        /// </summary>
        [DisplayName("申请时间")]
        [Required]
        public virtual DateTime? PurchasePlan_NeedDate
        {
            get;
            set;
        }

        /// <summary>
        /// 设备id
        /// </summary>
        [DisplayName("设备id")]
        [StringLength(30)]
        public virtual string GoodsID
        {
            get;
            set;
        }

        ///// <summary>
        ///// 大类id
        ///// </summary>
        //[DisplayName("大类id")]
        //[StringLength(30)]
        //public virtual string BID
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 中类id
        ///// </summary>
        //[DisplayName("中类id")]
        //[StringLength(30)]
        //public virtual string MID
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 小类id
        ///// </summary>
        //[DisplayName("小类id")]
        //[StringLength(30)]
        //public virtual string SID
        //{
        //    get;
        //    set;
        //}
        
        
        /// <summary>
        /// 维修原因
        /// </summary>
        [DisplayName("维修原因")]
        [StringLength(200)]
        [Required]
        public virtual string PurchasePlan_reason
        {
            get;
            set;
        }
        /// <summary>
        /// 维修计划状态 0未开始 1维修中 2维修完毕
        /// </summary>
        [DisplayName("维修计划状态")]
        public virtual int PurchasePlan_planstate
        {
            get;
            set;
        }
        /// <summary>
        /// 维修状态 0申请 1申请审核通过 2申请审核不通过 3完成 4xxx 5xxx
        /// </summary>
        [DisplayName("维修状态")]
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
        /// 
        /// </summary>
        [DisplayName("预估价格")]
        public virtual decimal? planmoney
        {
            get;
            set;
        }

        /// <summary>
        /// 数据类型 0设备 1车辆
        /// </summary>
        [DisplayName("数据类型")]
        public virtual int _type
        {
            get;
            set;
        }


        //[ScriptIgnore]
        //public virtual ClassB ClassB
        //{
        //    get;
        //    set;
        //}


        //[ScriptIgnore]
        //public virtual ClassM ClassM
        //{
        //    get;
        //    set;
        //}

        //[ScriptIgnore]
        //public virtual Classs Classs
        //{
        //    get;
        //    set;
        //}

        //[ScriptIgnore]
        //public virtual Equipment Equipment
        //{
        //    get;
        //    set;
        //}

        //[DisplayName("设备名称")]
        //public virtual string EquipmentName
        //{
        //    get { return Equipment == null ? string.Empty : Equipment.EquipmentName; }
        //}
        //[DisplayName("设备大类")]
        //public virtual string ClassBName
        //{
        //    get { return ClassB == null ? string.Empty : ClassB.ClassBName; }
        //}
        //[DisplayName("设备中类")]
        //public virtual string ClassMName
        //{
        //    get { return ClassM == null ? string.Empty : ClassM.ClassMName; }
        //}
        //[DisplayName("设备小类")]
        //public virtual string ClassSName
        //{
        //    get { return Classs == null ? string.Empty : Classs.ClassSName; }
        //}
        #endregion
    }
}
