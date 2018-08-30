
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 设备数据维护抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Equipment : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(EquipmentName);
			sb.Append(Spec);
			sb.Append(Brand);
			sb.Append(ClassBID);
			sb.Append(ClassMID);
			sb.Append(ClassSID);
			sb.Append(PurchaseDate);
			sb.Append(EquipStatus);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 设备名称
        /// </summary>
        [Required]
        [DisplayName("设备名称")]
        [StringLength(50)]
        public virtual string EquipmentName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 规格
        /// </summary>
        [DisplayName("规格")]
        [StringLength(50)]
        public virtual string Spec
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 厂牌
        /// </summary>
        [DisplayName("厂牌")]
        [StringLength(50)]
        public virtual string Brand
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 设备大类
        /// </summary>
        [Required]
        [DisplayName("设备大类")]
        [StringLength(30)]
        public virtual string ClassBID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 设备中类
        /// </summary>
        [DisplayName("设备中类")]
        [StringLength(30)]
        public virtual string ClassMID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 设备细类
        /// </summary>
        [DisplayName("设备细类")]
        [StringLength(30)]
        public virtual string ClassSID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 购置日期
        /// </summary>
        [DisplayName("购置日期")]
        public virtual System.DateTime? PurchaseDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 设备状态
        /// </summary>
        [DisplayName("设备状态")]
        [StringLength(50)]
        public virtual string EquipStatus
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
        [ScriptIgnore]
		public virtual Department Department
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
        [ScriptIgnore]
		public virtual IList<EquipmentItem> EquipmentItems
        {
            get;
            set;
        }
		 
        [ScriptIgnore]
		public virtual IList<MntZl> MntZls
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual IList<Equipment> EquipmentMember
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<EquipMtLy> EquipMtLies
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<EquipMtLyReturn> EquipMtLyReturns
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<EquipTermlyMt> EquipTermlyMts
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
		 
		
        [ScriptIgnore]
		public virtual IList<EquipMtLyReturnItem> EquipMtLyReturnItems
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<EquipTermlyMtItem> EquipTermlyMtItems
        {
            get;
            set;
        }
		
        #endregion
    }
}
