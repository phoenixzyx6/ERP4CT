
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
    public abstract class _EquipTermlyMtItem : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(UnitPrice);
			sb.Append(Amount);
			sb.Append(Sumx);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        public virtual decimal? UnitPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 数量
        /// </summary>
        [DisplayName("数量")]
        public virtual int? Amount
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
        [ScriptIgnore]
		public virtual Department Department
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual EquipTermlyMt EquipTermlyMt
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual PartInfo PartInfo
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual User User
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
