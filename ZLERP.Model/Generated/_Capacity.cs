
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  生产记录主表（转换前）抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Capacity : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ProductRecID);
			sb.Append(ProduceDate);
			sb.Append(ProjectName);
			sb.Append(CustName);
			sb.Append(TaskID);
			sb.Append(ConsMixpropID);
			sb.Append(ProvidedCube);
			sb.Append(ProvidedTimes);
			sb.Append(ProduceCube);
			sb.Append(ConStrength);
			sb.Append(RealSlump);
			sb.Append(ConsPos);
			sb.Append(CastMode);
			sb.Append(ProductLineName);
			sb.Append(TechId);
			sb.Append(CarID);
			sb.Append(PotCount);
			sb.Append(DispatchID);
			sb.Append(SWRate);
			sb.Append(RWRate);
			sb.Append(ProductLineID);
			sb.Append(SynStatus);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

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
        /// 生产日期
        /// </summary>
        [DisplayName("生产日期")]
        public virtual System.DateTime? ProduceDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 工程名称
        /// </summary>
        [DisplayName("工程名称")]
        [StringLength(128)]
        public virtual string ProjectName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 客户名称
        /// </summary>
        [DisplayName("客户名称")]
        [StringLength(128)]
        public virtual string CustName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 任务单号
        /// </summary>
        [DisplayName("任务单号")]
        [StringLength(30)]
        public virtual string TaskID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 配合比编号
        /// </summary>
        [DisplayName("配合比编号")]
        [StringLength(30)]
        public virtual string ConsMixpropID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 已供方量
        /// </summary>
        [Required]
        [DisplayName("已供方量")]
        public virtual decimal ProvidedCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 累计车数
        /// </summary>
        [DisplayName("累计车数")]
        public virtual int? ProvidedTimes
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 生产方量（实际生产方量）
        /// </summary>
        [Required]
        [DisplayName("生产方量（实际生产方量）")]
        public virtual decimal ProduceCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砼强度
        /// </summary>
        [DisplayName("砼强度")]
        [StringLength(50)]
        public virtual string ConStrength
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 真实坍落度
        /// </summary>
        [DisplayName("真实坍落度")]
        [StringLength(20)]
        public virtual string RealSlump
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 如一楼地面、三层梁板 施工部位
        /// </summary>
        [DisplayName("如一楼地面、三层梁板 施工部位")]
        [StringLength(50)]
        public virtual string ConsPos
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 浇筑方式
        /// </summary>
        [DisplayName("浇筑方式")]
        [StringLength(50)]
        public virtual string CastMode
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 生产线
        /// </summary>
        [DisplayName("生产线")]
        [StringLength(20)]
        public virtual string ProductLineName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 技术参数编号
        /// </summary>
        [DisplayName("技术参数编号")]
        public virtual int? TechId
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 运输车号
        /// </summary>
        [DisplayName("运输车号")]
        [StringLength(30)]
        public virtual string CarID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 罐数
        /// </summary>
        [DisplayName("罐数")]
        public virtual int? PotCount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 登记编号
        /// </summary>
        [DisplayName("登记编号")]
        [StringLength(30)]
        public virtual string DispatchID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砂含水率
        /// </summary>
        [DisplayName("砂含水率")]
        public virtual decimal? SWRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 石含水率
        /// </summary>
        [DisplayName("石含水率")]
        public virtual decimal? RWRate
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
        /// <summary>
        /// 同步状态
        /// </summary>
        [DisplayName("同步状态")]
        public virtual int? SynStatus
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual IList<CapacityItem> CapacityItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
