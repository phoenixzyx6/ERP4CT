
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 车辆轮胎维护抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _TyreInfo : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(StockDate);
			sb.Append(TyreType);
			sb.Append(TyreModel);
			sb.Append(TyreSpec);
			sb.Append(InstallPlace);
			sb.Append(Manufacture);
			sb.Append(BreedCode);
			sb.Append(MileageAble);
			sb.Append(MileageUsed);
			sb.Append(CurrentStatus);
			sb.Append(Version);
			sb.Append(CarID);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 进货日期
        /// </summary>
        [Required]
        [DisplayName("进货日期")]
        public virtual System.DateTime StockDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 轮胎类别
        /// </summary>
        [Required]
        [DisplayName("轮胎类别")]
        [StringLength(50)]
        public virtual string TyreType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 轮胎型号
        /// </summary>
        [Required]
        [DisplayName("轮胎型号")]
        [StringLength(50)]
        public virtual string TyreModel
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 轮胎规格
        /// </summary>
        [DisplayName("轮胎规格")]
        [StringLength(50)]
        public virtual string TyreSpec
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 当前所在位置
        /// </summary>
        [DisplayName("当前所在位置")]
        [StringLength(50)]
        public virtual string InstallPlace
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 生产厂家
        /// </summary>
        [DisplayName("生产厂家")]
        [StringLength(50)]
        public virtual string Manufacture
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 轮胎品牌
        /// </summary>
        [DisplayName("轮胎品牌")]
        [StringLength(30)]
        public virtual string BreedCode
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 耐用里程
        /// </summary>
        [DisplayName("耐用里程")]
        [Required]
        public virtual decimal MileageAble
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 使用里程
        /// </summary>
        [DisplayName("使用里程")]
        public virtual decimal? MileageUsed
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 当前状态
        /// </summary>
        [Required]
        [DisplayName("当前状态")]
        [StringLength(50)]
        public virtual string CurrentStatus
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
    }
}
