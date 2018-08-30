
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 筒仓机组关系 筒仓机组关系抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _SiloProductLine : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(SiloID);
			sb.Append(ProductLineID);
			sb.Append(OrderNum);
			sb.Append(Rate);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        //public override string ID
        //{
        //    get
        //    {
        //        System.Text.StringBuilder uniqueId = new System.Text.StringBuilder();
        //        uniqueId.Append(SiloID.ToString());
        //        uniqueId.Append("^");
        //        uniqueId.Append(ProductLineID.ToString());
        //        return uniqueId.ToString();
        //    }
        //}		
        /// <summary>
        /// 筒仓编号
        /// </summary>
        [Required]
        [DisplayName("筒仓编号")]
        [StringLength(30)]
        public virtual string SiloID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 搅拌机组编号
        /// </summary>
        [Required]
        [DisplayName("搅拌机组编号")]
        [StringLength(20)]
        public virtual string ProductLineID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 筒仓顺序
        /// </summary>
        [Required]
        [DisplayName("筒仓顺序")]
        public virtual int OrderNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 比率
        /// </summary>
        [DisplayName("比率")]
        [Range(0,1)]
        public virtual decimal? Rate
        {
            get;
			set;			 
        }
        [DisplayName("秤位")]
        public virtual int? MeasureID
        { get; set; 
        
        }
        public virtual MeasureInfo MeasureInfo
        {
            get;
            set;
        }
        #endregion
    }
}
