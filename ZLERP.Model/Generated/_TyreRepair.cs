
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 轮胎维修记录抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _TyreRepair : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(RepairDate);
			sb.Append(ReceiveDate);
			sb.Append(TyreID);
			sb.Append(RepairType);
			sb.Append(Remark);
			sb.Append(Version);
			sb.Append(CarID);
            sb.Append(RepairResult);
            sb.Append(RepairRemark);
            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 维修时间
        /// </summary>
        [Required]
        [DisplayName("维修时间")]
        public virtual System.DateTime RepairDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 送修时间
        /// </summary>
        [Required]
        [DisplayName("送修时间")]
        public virtual System.DateTime ReceiveDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 轮胎类型
        /// </summary>
        [Required]
        [DisplayName("轮胎编号")]
        [StringLength(30)]
        public virtual string TyreID
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
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(256)]
        public virtual string Remark
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 车辆代号
        /// </summary>
        [DisplayName("车辆代号")]
        [StringLength(30)]
        public virtual string CarID
        {
            get;
			set;			 
        }	
        #endregion

        [DisplayName("是否修好")]
        public virtual bool? RepairResult
        {
            get;
            set;
        }

        [DisplayName("修理备注")]
        public virtual string RepairRemark
        {
            get;
            set;
        }
    }
}
