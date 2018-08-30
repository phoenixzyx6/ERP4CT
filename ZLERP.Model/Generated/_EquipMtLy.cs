
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
    public abstract class _EquipMtLy : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(MtDate);
            sb.Append(TyreID);
            sb.Append(IsEntrust);
            sb.Append(Sumx);
            sb.Append(Remark);
            sb.Append(YM);
            sb.Append(Version);
            sb.Append(Finder);
            sb.Append(FindTime);
            sb.Append(ApplyMan);
            sb.Append(ApplyTime);
            sb.Append(TroubleDes);
            sb.Append(RepairAdv); 
            sb.Append(GoodsOutID);
            sb.Append(mtlystate);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 出库ID
        /// </summary>
        [DisplayName("出库ID")]
        public virtual string GoodsOutID
        {
            get;
            set;
        }	

        /// <summary>
        /// 预估金额
        /// </summary>
        [DisplayName("预估金额")]
        public virtual int summoney
        {
            get;
            set;
        }	

        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public virtual int mtlystate
        {
            get;
            set;
        }	

        /// <summary>
        /// 维修日期
        /// </summary>
        [DisplayName("维修日期")]
        public virtual System.DateTime MtDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 胎号
        /// </summary>
        [DisplayName("胎号")]
        [StringLength(30)]
        public virtual string TyreID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否委外
        /// </summary>
        [DisplayName("是否委外")]
        public virtual bool IsEntrust
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 金额
        /// </summary>
        [DisplayName("金额")]
        public virtual decimal? Sumx
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(128)]
        public virtual string Remark
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 年/月
        /// </summary>
        [DisplayName("年/月")]
        public virtual System.DateTime? YM
        {
            get;
			set;			 
        }

        /// <summary>
        /// 发现人
        /// </summary>
        [DisplayName("发现人")]
        [StringLength(30)]
        public virtual string Finder
        {
            get;
            set;
        }
        /// <summary>
        /// 发现时间
        /// </summary>
        [DisplayName("发现时间")]
        public virtual System.DateTime? FindTime
        {
            get;
            set;
        }
        /// <summary>
        /// 申请人
        /// </summary>
        [DisplayName("申请人")]
        [StringLength(30)]
        public virtual string ApplyMan
        {
            get;
            set;
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        [DisplayName("申请时间")]
        public virtual System.DateTime? ApplyTime
        {
            get;
            set;
        }
        /// <summary>
        /// 故障描述
        /// </summary>
        [DisplayName("故障描述")]
        [StringLength(256)]
        public virtual string TroubleDes
        {
            get;
            set;
        }
        /// <summary>
        /// 维修建议
        /// </summary>
        [DisplayName("维修建议")]
        [StringLength(256)]
        public virtual string RepairAdv
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual ClassB ClassB
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual ClassM ClassM
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual Classs Classs
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual Contract Contract
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual Equipment Equipment
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual MntZl MntZl
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<_EquipMtLyItem> EquipMtLyItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
