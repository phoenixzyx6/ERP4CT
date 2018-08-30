
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 更换轮胎记录抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _TyreChange : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ChangeDate);
			sb.Append(TyreType);
			sb.Append(NewTyreID);
			sb.Append(OldTyreID);
			sb.Append(ChangeReason);
			sb.Append(InstallPlace);
			sb.Append(Mileage);
			sb.Append(ChangeMan);
			sb.Append(Version);
			sb.Append(CarID);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 更换日期
        /// </summary>
        [Required]
        [DisplayName("更换日期")]
        public virtual System.DateTime ChangeDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 轮胎类型
        /// </summary>
         
        [DisplayName("轮胎类型")]
        [StringLength(50)]
        public virtual string TyreType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 新轮胎编号
        /// </summary>
        [DisplayName("新轮胎编号")]
        [StringLength(30)]
        public virtual string NewTyreID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 旧轮胎编号
        /// </summary>
        [DisplayName("旧轮胎编号")]
        [StringLength(30)]
        public virtual string OldTyreID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 更换原因
        /// </summary>
        [Required]
        [DisplayName("更换理由")]
        [StringLength(50)]
        public virtual string ChangeReason
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 更换部位
        /// </summary>
       
        [DisplayName("更换部位")]
        [StringLength(50)]
        public virtual string InstallPlace
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 里程
        /// </summary>
        [Required]
        [DisplayName("里程")]
        public virtual decimal Mileage
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 更换人
        /// </summary>
        [Required]
        [DisplayName("更换人")]
        [StringLength(30)]
        public virtual string ChangeMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 车辆代号
        /// </summary>
        [Required]
        [DisplayName("车辆代号")]
        [StringLength(30)]
        public virtual string CarID
        {
            get;
			set;			 
        }	
        #endregion
    }
}
