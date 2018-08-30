
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  客户配比库抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CustMixprop : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(MixpropCode);
			sb.Append(ConStrength);
			sb.Append(Slump);
			sb.Append(ImpGrade);
			sb.Append(CarpRadii);
			sb.Append(Weight);
			sb.Append(WCRate);
			sb.Append(OXY);
			sb.Append(Mesh);
			sb.Append(MixpropRate);
			sb.Append(SCRate);
			sb.Append(AdmixtureType);
			sb.Append(CementType);
			sb.Append(SIWRate);
			sb.Append(RIWRate);
			sb.Append(SIRRate);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 配比代号
        /// </summary>
        [DisplayName("配比代号")]
        [StringLength(50)]
        [Required]
        public virtual string MixpropCode
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
        [DisplayName("坍落度")]
        [StringLength(20)]
        public virtual string Slump
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
        /// 骨料粒径
        /// </summary>
        [DisplayName("骨料粒径")]
        public virtual decimal? CarpRadii
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
        /// 水灰比
        /// </summary>
        [DisplayName("水灰比")]
        public virtual decimal? WCRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 含氧量
        /// </summary>
        [DisplayName("含氧量")]
        public virtual decimal? OXY
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 细度目数
        /// </summary>
        [DisplayName("细度目数")]
        public virtual decimal? Mesh
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 配合比例
        /// </summary>
        [DisplayName("配合比例")]
        [StringLength(20)]
        public virtual string MixpropRate
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
        /// 外加剂种类
        /// </summary>
        [DisplayName("外加剂种类")]
        [StringLength(50)]
        public virtual string AdmixtureType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 混凝土类别
        /// </summary>
        [DisplayName("混凝土类别")]
        [StringLength(50)]
        public virtual string CementType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砂含水率
        /// </summary>
        [DisplayName("砂含水率")]
        public virtual decimal? SIWRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 石含水率
        /// </summary>
        [DisplayName("石含水率")]
        public virtual decimal? RIWRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砂含石率
        /// </summary>
        [DisplayName("砂含石率")]
        public virtual decimal? SIRRate
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual IList<CustMixpropItem> CustMixpropItems
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<OpenCheck> OpenChecks
        {
            get;
            set;
        }


        [ScriptIgnore]
        public virtual IList<QualityExam> QualityExams
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<AlkaliReport> AlkaliReports
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<ConStrAssess> ConStrAssesses
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
