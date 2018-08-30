
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  生产记录罐数（转换后）抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Manufacture : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(SynID);
			sb.Append(ProductRecID);
			sb.Append(ProduceCube);
			sb.Append(PotTimes);
			sb.Append(PCRate);
			sb.Append(ElectValue);
			sb.Append(IsManual);
			sb.Append(ProductLineID);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 同步编号
        /// </summary>
        [DisplayName("同步编号")]
        public virtual int? SynID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 生产记录编号
        /// </summary>
        [DisplayName("生产记录编号")]
        [StringLength(30)]
        public virtual string ProductRecID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 生产方量（实际生产）
        /// </summary>
        [Required]
        [DisplayName("生产方量（实际生产）")]
        public virtual decimal ProduceCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 罐次
        /// </summary>
        [DisplayName("罐次")]
        public virtual int? PotTimes
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 罐容比
        /// </summary>
        [DisplayName("罐容比")]
        public virtual decimal? PCRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 电流值
        /// </summary>
        [DisplayName("电流值")]
        public virtual decimal? ElectValue
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否手动
        /// </summary>
        [Required]
        [DisplayName("是否手动")]
        public virtual bool IsManual
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 搅拌机组编号
        /// </summary>
        [DisplayName("搅拌机组编号")]
        [StringLength(20)]
        public virtual string ProductLineID
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual IList<ManufactureItem> ManufactureItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
