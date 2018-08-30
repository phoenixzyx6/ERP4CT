
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  工程信息抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Project : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ProjectAddr);
			sb.Append(ProjectName);
			sb.Append(Distance);
			sb.Append(BuildUnit);
			sb.Append(ConstructUnit);
			sb.Append(LinkMan);
			sb.Append(Tel);
			sb.Append(Remark);
			sb.Append(Longitude);
			sb.Append(Latitude);
			sb.Append(PlaceRange);
			sb.Append(ErrorValue);
			sb.Append(CPOrder);
			sb.Append(IsShow);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 项目地址
        /// </summary>
        [DisplayName("项目地址")]
        [StringLength(128)]
        public virtual string ProjectAddr
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 工程名称
        /// </summary>
        [DisplayName("工程名称")]
        [Required]
        [StringLength(128)]
        public virtual string ProjectName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 工程运距
        /// </summary>
        [DisplayName("工程运距")]
        public virtual decimal? Distance
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 建设单位
        /// </summary>
        [DisplayName("建设单位")]
        [StringLength(256)]
        public virtual string BuildUnit
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 施工单位
        /// </summary>
        [DisplayName("施工单位")]
        [StringLength(256)]
        public virtual string ConstructUnit
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 工地联系人
        /// </summary>
        [DisplayName("工地联系人")]
        [StringLength(30)]
        public virtual string LinkMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 工地电话
        /// </summary>
        [DisplayName("工地电话")]
        [StringLength(20)]
        public virtual string Tel
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
        /// 经度
        /// </summary>
        [DisplayName("经度")]
        public virtual double? Longitude
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 纬度
        /// </summary>
        [DisplayName("纬度")]
        public virtual double? Latitude
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 工地范围
        /// </summary>
        [DisplayName("工地范围")]
        public virtual decimal? PlaceRange
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 误差值
        /// </summary>
        [DisplayName("误差值")]
        public virtual decimal? ErrorValue
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 顺序
        /// </summary>
        [DisplayName("顺序")]
        public virtual int? CPOrder
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否显示
        /// </summary>
        [Required]
        [DisplayName("是否显示")]
        public virtual bool IsShow
        {
            get;
			set;			 
        }
        [Required]
        [DisplayName("结算方式")]
        public virtual string PayType
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual Contract Contract
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
