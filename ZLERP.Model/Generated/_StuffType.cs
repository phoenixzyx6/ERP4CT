
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  原料类型表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _StuffType : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(StuffTypeName);
			sb.Append(TypeID);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 原料类型名称
        /// </summary>
        [DisplayName("类型名称")]
        [StringLength(20)]
        [Required]
        public virtual string StuffTypeName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 如生产、办公等 所属类别
        /// </summary>
        [DisplayName("所属类别")]
        [StringLength(50)]
        [Required]
        public virtual string TypeID
        {
            get;
			set;			 
        }

        /// <summary>
        /// 材料大类编号
        /// </summary>
        [DisplayName("材料大类")]
        [StringLength(50)]
        public virtual string FinalStuffType
        {
            get;
            set;
        }


        [ScriptIgnore]
		public virtual IList<FormulaItem> FormulaItems
        {
            get;
            set;
        }
       
        [StringLength(50)]
        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }
        #endregion
    }
}
