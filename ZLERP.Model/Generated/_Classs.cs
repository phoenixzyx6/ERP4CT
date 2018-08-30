﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 类别细类抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Classs : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(ClassBID);
			sb.Append(ClassSName);
			sb.Append(ClassID);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties
        public virtual string ClassBID
        {
            get;
            set;
        }
        /// <summary>
        /// 细类名称
        /// </summary>
        [DisplayName("细类名称")]
        [StringLength(50)]
        [Required]
        public virtual string ClassSName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 类别类型
        /// </summary>
        [DisplayName("类别类型")]
        [StringLength(50)]
        public virtual string ClassID
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
		public virtual IList<MntZl> MntZls
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
