
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  筒仓抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Silo : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(SiloName);
			sb.Append(SiloShortName);
			sb.Append(MinContent);
			sb.Append(MaxContent);
			sb.Append(Content);
			sb.Append(MinWarm);
			sb.Append(MaxWarm);
			sb.Append(OrderNum);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 筒仓名称
        /// </summary>
        [DisplayName("筒仓名称")]
        [StringLength(50)]
        public virtual string SiloName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 筒仓简码
        /// </summary>
        [DisplayName("筒仓简码")]
        [StringLength(20)]
        public virtual string SiloShortName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 最小容量
        /// </summary>
        [DisplayName("最小容量")]
        public virtual decimal? MinContent
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 最大容量
        /// </summary>
        [DisplayName("最大容量")]
        public virtual decimal? MaxContent
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 当前容量
        /// </summary>
        [DisplayName("当前容量")]
        public virtual decimal Content
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 最小报警容量
        /// </summary>
        [DisplayName("最小报警容量")]
        public virtual decimal? MinWarm
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 最大报警容量
        /// </summary>
        [DisplayName("最大报警容量")]
        public virtual decimal? MaxWarm
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        [DisplayName("排序")]
        public virtual int OrderNum
        {
            get;
			set;			 
        }	

        /// <summary>
        /// 原材料信息
        /// </summary>
		public virtual StuffInfo StuffInfo
        {
            get;
			set;
        }

        [ScriptIgnore]
		public virtual IList<ConsMixpropItem> ConsMixpropItems
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<SiloTune> SiloTunes
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<StuffIn> StuffIns
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual IList<CheckItem> CheckItems
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<SiloProductLine> SiloProductLines
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<ProductLine> ProductLines
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<MonthAccount> MonthAccounts
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<StuffAccount> StuffAccounts
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<Lab_Record> Lab_Records
        {
            get;
            set;
        }
        #endregion
    }
}
