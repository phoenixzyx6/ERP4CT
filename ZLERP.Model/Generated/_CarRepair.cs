
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
    public abstract class _CarRepair : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(CarMan);
			sb.Append(CarID);
			sb.Append(RepairType);
			sb.Append(RepairTime);
            sb.Append(ReturnTime);
			sb.Append(RepairAddr);
			sb.Append(RepairMan);
			sb.Append(RepairReason);
			sb.Append(RepairDes);
			sb.Append(RepairCost);
            sb.Append(mtlystate);
            sb.Append(GoodsOutID);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 出库ID
        /// </summary>
        [DisplayName("出库ID")]
        public virtual string GoodsOutID
        {
            get;
            set;
        }	

        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public virtual int mtlystate
        {
            get;
            set;
        }	

        /// <summary>
        /// 预估金额
        /// </summary>
        [DisplayName("预估金额")]
        public virtual decimal summoney
        {
            get;
            set;
        }	

        /// <summary>
        /// 送修人
        /// </summary>
        [DisplayName("送修人")]
        [StringLength(50)]
        public virtual string CarMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 车号
        /// </summary>
        [Required]
        [DisplayName("车号")]
        [StringLength(50)]
        public virtual string CarID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 维修类型
        /// </summary>
        [Required]
        [DisplayName("维修类型")]
        [StringLength(50)]
        public virtual string RepairType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 维修时间
        /// </summary>
        [Required]
        [DisplayName("维修时间")]
        public virtual System.DateTime RepairTime
        {
            get;
			set;			 
        }
        /// <summary>
        /// 返厂时间
        /// </summary>
        [DisplayName("返厂时间")]
        public virtual System.DateTime? ReturnTime
        {
            get;
            set;
        }	
        /// <summary>
        /// 维修地点
        /// </summary>
        [DisplayName("维修地点")]
        [StringLength(256)]
        public virtual string RepairAddr
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 维修人
        /// </summary>
        [DisplayName("维修人")]
        [StringLength(50)]
        public virtual string RepairMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 维修原因
        /// </summary>
        [DisplayName("维修原因")]
        [StringLength(256)]
        public virtual string RepairReason
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 维修描述
        /// </summary>
        [DisplayName("维修描述")]
        [StringLength(256)]
        public virtual string RepairDes
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 维修费用
        /// </summary>
        [DisplayName("维修费用")]
        public virtual decimal? RepairCost
        {
            get;
			set;			 
        }	
        #endregion
    }
}
