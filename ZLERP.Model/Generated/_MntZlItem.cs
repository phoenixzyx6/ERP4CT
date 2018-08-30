
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
    public abstract class _MntZlItem : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(PurposeMill);
			sb.Append(Unit);
			sb.Append(amount);
			sb.Append(IsAssess);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

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
        /// 单位
        /// </summary>
        [DisplayName("单位")]
        [StringLength(50)]
        public virtual string Unit
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 支领数量
        /// </summary>
        [DisplayName("支领数量")]
        public virtual int? amount
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
		public virtual Maintenance Maintenance
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
		public virtual PartInfo PartInfo
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
