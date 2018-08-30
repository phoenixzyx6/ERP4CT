
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
    public abstract class _Maintenance : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ItemName);
			sb.Append(DrawType);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 项目名称
        /// </summary>
        [DisplayName("项目名称")]
        [StringLength(50)]
        public virtual string ItemName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 领用类别
        /// </summary>
        [DisplayName("领用类别")]
        [StringLength(50)]
        public virtual string DrawType
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
		public virtual ClassB ClassB
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
		public virtual IList<_EquipMtLyItem> EquipMtLyItems
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
		public virtual IList<MntZlItem> MntZlItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
