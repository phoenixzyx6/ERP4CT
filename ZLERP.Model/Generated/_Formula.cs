
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  理论配比表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Formula : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(FormulaName);
			sb.Append(FormulaType);
			sb.Append(ConStrength);
			sb.Append(Slump);
            sb.Append(SlumpError);
			sb.Append(CastMode);
			sb.Append(SeasonType);
			sb.Append(CementType);
			sb.Append(Weight);
			sb.Append(TuneWeight);
			sb.Append(WCRate);
			sb.Append(SCRate);
			sb.Append(Mesh);
			sb.Append(ImpGrade);
			sb.Append(FlexStrength);
			sb.Append(CarpRadii);
			sb.Append(BetonTag);
			sb.Append(MixingTime);
			sb.Append(CementBreed);
			sb.Append(Purpose);
			sb.Append(Remark);
			sb.Append(Version);

            sb.Append(Fce);
            sb.Append(DesignStrength);
            sb.Append(CaType);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 理论配比名称
        /// </summary>
        [DisplayName("配比代号")]
        [StringLength(50)]
        [Required]
        public virtual string FormulaName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 混凝土、砂浆 料种
        /// </summary>
        [DisplayName("料种")]
        [StringLength(20)]
        public virtual string FormulaType
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
        /// 坍落度
        /// </summary>
        [DisplayName("坍落度(mm)")]
        [StringLength(20)]
        public virtual string Slump
        {
            get;
			set;			 
        }
        /// <summary>
        /// 坍落度误差
        /// </summary>
        [DisplayName("坍落度误差(mm)")]
        public virtual int? SlumpError
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
        /// 所属季节
        /// </summary>
        [DisplayName("所属季节")]
        [StringLength(50)]
        public virtual string SeasonType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 混凝土种类
        /// </summary>
        [DisplayName("混凝土种类")]
        [StringLength(50)]
        public virtual string CementType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 设计容重
        /// </summary>
        [DisplayName("设计容重")]
        public virtual decimal? Weight
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 调整容重
        /// </summary>
        [DisplayName("调整容重")]
        public virtual decimal? TuneWeight
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 水灰比
        /// </summary>
        [DisplayName("水灰比")]
        public virtual decimal? WCRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砂率
        /// </summary>
        [DisplayName("砂率")]
        public virtual decimal? SCRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 细度模数
        /// </summary>
        [DisplayName("细度模数")]
        public virtual decimal? Mesh
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗渗等级
        /// </summary>
        [DisplayName("抗渗等级")]
        [StringLength(20)]
        public virtual string ImpGrade
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗折强度
        /// </summary>
        [DisplayName("抗折强度")]
        [StringLength(20)]
        public virtual string FlexStrength
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 骨料粒径
        /// </summary>
        [DisplayName("骨料粒径")]
        public virtual decimal? CarpRadii
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砼标记
        /// </summary>
        [DisplayName("砼标记")]
        [StringLength(50)]
        public virtual string BetonTag
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 搅拌时间
        /// </summary>
        [DisplayName("搅拌时间")]
        [Range(10, int.MaxValue, ErrorMessage = "搅拌时间必须大于10秒")]
        [Required]
        public virtual int? MixingTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 水泥等级
        /// </summary>
        [DisplayName("水泥等级")]
        [StringLength(50)]
        [Required]
        public virtual string CementBreed
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 用途
        /// </summary>
        [DisplayName("用途")]
        [StringLength(128)]
        public virtual string Purpose
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
        /// 水泥强度(Mpa)
        /// </summary>
        [DisplayName("水泥强度(Mpa)")]
        public virtual decimal? Fce
        {
            get;
            set;
        }
        /// <summary>
        /// 配合比设计强度(Mpa)
        /// </summary>
        [DisplayName("配合比设计强度(Mpa)")]
        public virtual decimal? DesignStrength
        {
            get;
            set;
        }
        /// <summary>
        /// 石子类型
        /// </summary>
        [DisplayName("石子类型")]
        [StringLength(50)]
        public virtual string CaType
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual IList<FormulaItem> FormulaItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
