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
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _EquipMtLyItem : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(Amount);
            sb.Append(PurposeMill);
            sb.Append(IsAssess);
            sb.Append(Remark);
            sb.Append(MntZlItemID);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        [DisplayName("数量")]
        public virtual int Amount
        {
            get;
            set;			 
        }	
        /// <summary>
        /// 用途厂牌
        /// </summary>
        [DisplayName("用途厂牌")]
        [StringLength(50)]
        public virtual string PurposeMill
        {
            get;
            set;			 
        }	
        /// <summary>
        /// 是否考核
        /// </summary>
        [DisplayName("是否考核")]
        public virtual bool IsAssess
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
        /// 
        /// </summary>
        public virtual int? MntZlItemID
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
        public virtual EquipMtLy EquipMtLy
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
        public virtual Maintenance Maintenance
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
