
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
    public abstract class _PartInfo : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(PartName);
			sb.Append(PartSpec);
			sb.Append(GreatClassID);
			sb.Append(MiddClassID);
			sb.Append(MinClassID);
			sb.Append(SupplyName);
			sb.Append(BreadName);
			sb.Append(UnitID);
			sb.Append(IsOften);
			sb.Append(LowerLimit);
			sb.Append(UpperLimit);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 配件名称
        /// </summary>
        [Required]
        [DisplayName("配件名称")]
        [StringLength(50)]
        public virtual string PartName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 配件规格
        /// </summary>
        [DisplayName("配件规格")]
        [StringLength(50)]
        public virtual string PartSpec
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 大类
        /// </summary>
        [Required]
        [DisplayName("大类")]
        [StringLength(50)]
        public virtual string GreatClassID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 中类
        /// </summary>
        [DisplayName("中类")]
        [StringLength(50)]
        public virtual string MiddClassID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 小类
        /// </summary>
        [DisplayName("小类")]
        [StringLength(50)]
        public virtual string MinClassID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 供应商
        /// </summary>
        [DisplayName("供应商")]
        [StringLength(50)]
        public virtual string SupplyName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 厂牌
        /// </summary>
        [DisplayName("厂牌")]
        [StringLength(50)]
        public virtual string BreadName
        {
            get;
			set;			 
        }
        /// <summary>
        /// 单位
        /// </summary>
        [Required]
        [DisplayName("单位")]
        [StringLength(50)]
        public virtual string UnitID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否常用
        /// </summary>
        [DisplayName("是否常用")]
        public virtual bool IsOften
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 上限值
        /// </summary>
        [DisplayName("上限值")]
        public virtual decimal LowerLimit
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 下限值
        /// </summary>
        [DisplayName("下限值")]
        public virtual decimal UpperLimit
        {
            get;
			set;			 
        }	

        /// <summary>
        /// 当前库存
        /// </summary>
        [Required]
        [DisplayName("当前库存")]
        public virtual decimal Inventory
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
		 
		
        [ScriptIgnore]
		public virtual IList<MntZlItem> MntZlItems
        {
            get;
            set;
        }
        #endregion
    }
}
