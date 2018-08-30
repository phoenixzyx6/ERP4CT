
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  生产任务单抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ProduceTask : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ProjectAddr);
            sb.Append(ProjectName);
            sb.Append(Seller);
			sb.Append(ConstructUnit);
			sb.Append(ConsPosType);
			sb.Append(ConsPos);
			//sb.Append(ContractItemsID);
			sb.Append(PlanCube);
			sb.Append(NeedTimes);
            sb.Append(PumpType);
			sb.Append(NeedDate);
            sb.Append(OpenTime);
            sb.Append(BuildUnit);
			sb.Append(SupplyUnit);
			sb.Append(SupplyUnitTel);
			sb.Append(IsCommission);
			sb.Append(CompPrincipal);
			sb.Append(LinkMan);
			sb.Append(Tel);
			sb.Append(IsDatum);
			sb.Append(CarpGrade);
			sb.Append(Carper);
			sb.Append(CarpRadii);
			sb.Append(BetonTag);
			sb.Append(CementBreed);
			sb.Append(ConStrength);
			sb.Append(WCRate);
			sb.Append(MixpropRate);
			sb.Append(Slump);
			sb.Append(CastMode);
			sb.Append(OtherDemand);
			sb.Append(Auditor);
			sb.Append(AuditTime);
			sb.Append(AuditStatus);
            sb.Append(AuditInfo);
			sb.Append(IsCompleted);
			sb.Append(CompleteDate);
			sb.Append(Remark);
			sb.Append(Distance);
			sb.Append(IsSlurry);
			sb.Append(SlurryFormula);
			sb.Append(TaskType);
			sb.Append(CementType);
			sb.Append(BetonFormula);
			sb.Append(IsFormulaSend);
			sb.Append(CustMixpropID);
			sb.Append(ImpGrade);
			sb.Append(ImyGrade);
			sb.Append(ImdGrade);
            sb.Append(ImyCriterion);
            sb.Append(ImzCriterion);
            sb.Append(IsImpExp);
            sb.Append(IsImzExp);
            sb.Append(AdditiveType);
			sb.Append(IdentityValue);
			sb.Append(PlanClass);
			sb.Append(ProductPrincipal);
			sb.Append(ForkLift);
			sb.Append(Field1);
			sb.Append(Field2);
			sb.Append(Field3);
			sb.Append(Field4);
			sb.Append(Field5);
			sb.Append(Version);
            sb.Append(SIWRate);
            sb.Append(RIWRate);
            sb.Append(SIRRate);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

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
        /// 销售员
        /// </summary>
        [DisplayName("销售员")]
        [StringLength(128)]
        public virtual string Seller
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
        /// 如地面、梁板 施工部位类型
        /// </summary>
        [DisplayName("施工部位类型")]
        [StringLength(50)]
        public virtual string ConsPosType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 如一楼地面、三层梁板 施工部位
        /// </summary>
        [DisplayName("施工部位")]
        [StringLength(100)]
        public virtual string ConsPos
        {
            get;
			set;			 
        }		
        /// <summary>
        /// 计划方量
        /// </summary>
        [Required]
        [DisplayName("计划方量")]
        public virtual decimal PlanCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 计划车数
        /// </summary>
        [DisplayName("计划车数")]
        public virtual int? NeedTimes
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 泵名称
        /// </summary>
        [DisplayName("泵车类型")]
        [StringLength(50)]
        public virtual string PumpType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 计划时间
        /// </summary>
        [DisplayName("计划时间")]
        [Required]
        public virtual System.DateTime NeedDate
        {
            get;
			set;			 
        }
        /// <summary>
        /// 开盘时间
        /// </summary>
        [DisplayName("开盘时间")] 
        public virtual System.DateTime? OpenTime
        {
            get;
            set;
        }
        /// <summary>
        /// 供应单位
        /// </summary>
        [Required]
        [DisplayName("供应单位")]
        [StringLength(128)]
        public virtual string SupplyUnit
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 供应单位电话
        /// </summary>
        [DisplayName("供应单位电话")]
        [StringLength(20)]
        public virtual string SupplyUnitTel
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否代外生产
        /// </summary>
        [Required]
        [DisplayName("是否代外生产")]
        public virtual bool IsCommission
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 公司负责人
        /// </summary>
        [DisplayName("公司负责人")]
        [StringLength(30)]
        public virtual string CompPrincipal
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 工地联系人(前场工长)
        /// </summary>
        [DisplayName("前场工长")]
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
        /// 出资料否
        /// </summary>
        [Required]
        [DisplayName("出资料否")]
        public virtual bool IsDatum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 级配
        /// </summary>
        [DisplayName("级配")]
        [StringLength(50)]
        public virtual string CarpGrade
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 骨料
        /// </summary>
        [DisplayName("骨料")]
        [StringLength(50)]
        public virtual string Carper
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
        /// 水泥品种
        /// </summary>
        [DisplayName("水泥品种")]
        [StringLength(50)]
        public virtual string CementBreed
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
        /// 水灰比
        /// </summary>
        [DisplayName("水灰比")]
        public virtual decimal WCRate
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
        /// 其他技术要求
        /// </summary>
        [DisplayName("其他技术要求")]
        [StringLength(256)]
        public virtual string OtherDemand
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 审核人
        /// </summary>
        [DisplayName("审核人")]
        [StringLength(30)]
        public virtual string Auditor
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 审核时间
        /// </summary>
        [DisplayName("审核时间")]
        public virtual System.DateTime? AuditTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 审核状态
        /// </summary>
        [DisplayName("审核状态")]
        public virtual int AuditStatus
        {
            get;
			set;			 
        }
        /// <summary>
        /// 审核意见
        /// </summary>
        [DisplayName("审核意见")]
        [StringLength(128)]
        public virtual string AuditInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 是否完成
        /// </summary>
        [Required]
        [DisplayName("是否完成")]
        public virtual bool IsCompleted
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 完成日期
        /// </summary>
        [DisplayName("完成日期")]
        public virtual System.DateTime? CompleteDate
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
        /// 工程运距
        /// </summary>
        [DisplayName("工程运距")]
        public virtual decimal? Distance
        {
            get;
			set;			 
        }		
        /// <summary>
        /// 是否含砂浆
        /// </summary>
        [Required]
        [DisplayName("是否含砂浆")]
        public virtual bool IsSlurry
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砂浆配比
        /// </summary>
        [DisplayName("砂浆配比")]
        [StringLength(50)]
        public virtual string SlurryFormula
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 任务单类型
        /// </summary>
        [DisplayName("任务单类型")]
        [StringLength(50)]
        public virtual string TaskType
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
        /// 混凝土配比
        /// </summary>
        [DisplayName("混凝土配比")]
        [StringLength(50)]
        public virtual string BetonFormula
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 配比是否下发
        /// </summary>
        [Required]
        [DisplayName("配比是否下发")]
        public virtual bool IsFormulaSend
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 客户配比号
        /// </summary>
        [DisplayName("客户配比号")]
        [StringLength(30)]
        public virtual string CustMixpropID
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
        /// 抗压等级
        /// </summary>
        [DisplayName("抗压等级")]
        [StringLength(20)]
        public virtual string ImyGrade
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗冻等级
        /// </summary>
        [DisplayName("抗冻等级")]
        [StringLength(20)]
        public virtual string ImdGrade
        {
            get;
			set;			 
        }
        /// <summary>
        /// 抗压规范
        /// </summary>
        [DisplayName("抗压规范")]
        [StringLength(20)]
        public virtual string ImyCriterion
        {
            get;
            set;
        }
        /// <summary>
        /// 抗折规范
        /// </summary>
        [DisplayName("抗折规范")]
        [StringLength(20)]
        public virtual string ImzCriterion
        {
            get;
            set;
        }
        /// <summary>
        /// 是否产生抗压实验
        /// </summary>
        [Required]
        [DisplayName("是否产生抗压实验")]
        public virtual bool IsImpExp
        {
            get;
            set;
        }
        /// <summary>
        /// 是否产生抗折实验
        /// </summary>
        [Required]
        [DisplayName("是否产生抗折实验")]
        public virtual bool IsImzExp
        {
            get;
            set;
        }
        /// <summary>
        /// 外加剂品种
        /// </summary>
        [DisplayName("外加剂品种")]
        [StringLength(20)]
        public virtual string AdditiveType
        {
            get;
            set;
        }
        /// <summary>
        /// 特性参数
        /// </summary>
        [DisplayName("特性参数")]
        [StringLength(128)]
        public virtual string IdentityValue
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 计划班组
        /// </summary>
        [DisplayName("计划班组")]
        [StringLength(20)]
        public virtual string PlanClass
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 生产负责人
        /// </summary>
        [DisplayName("生产负责人")]
        [StringLength(30)]
        public virtual string ProductPrincipal
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 上料员
        /// </summary>
        [DisplayName("上料员")]
        [StringLength(30)]
        public virtual string ForkLift
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 扩展字段1
        /// </summary>
        [DisplayName("扩展字段1")]
        [StringLength(128)]
        public virtual string Field1
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 扩展字段2
        /// </summary>
        [DisplayName("扩展字段2")]
        [StringLength(128)]
        public virtual string Field2
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 扩展字段3
        /// </summary>
        [DisplayName("扩展字段3")]
        [StringLength(128)]
        public virtual string Field3
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 扩展字段4
        /// </summary>
        [DisplayName("扩展字段4")]
        [StringLength(128)]
        public virtual string Field4
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 扩展字段5
        /// </summary>
        [DisplayName("扩展字段5")]
        [StringLength(128)]
        public virtual string Field5
        {
            get;
			set;			 
        }

        /// <summary>
        /// 是否已出开盘
        /// </summary>
        [Required]
        [DisplayName("已出开盘")]
        public virtual bool IsOut
        {
            get;
            set;
        }

        /// <summary>
        /// 砂含水率
        /// </summary>
        [DisplayName("砂含水%")]
        [Range(0, 100)]
        public virtual decimal SIWRate
        {
            get;
            set;
        }
        /// <summary>
        /// 石含水率
        /// </summary>
        [DisplayName("小石含水%")]
        [Range(0, 100)]
        public virtual decimal RIWRate
        {
            get;
            set;
        }
        /// <summary>
        /// 石含水率
        /// </summary>
        [DisplayName("大石含水%")]
        [Range(0, 100)]
        public virtual decimal RIWRate1
        {
            get;
            set;
        }
        /// <summary>
        /// 砂含石率
        /// </summary>
        [DisplayName("砂含石%")]
        [Range(0, 100)]
        public virtual decimal SIRRate
        {
            get;
            set;
        }	
       
		public virtual Contract Contract
        {
            get;
			set;
        }

        public virtual ContractItem ContractItem
        {
            get;
            set;
        }
       
        public virtual Region Region
        {
            get;
            set;
        }

        public virtual CustMixprop CustMixprop
        {
            get;
            set;
        }

        public virtual Formula BetonFormulaInfo
        {
            get;
            set;
        }

        public virtual Formula SlurryFormulaInfo
        {
            get;
            set;
        }
		
        [ScriptIgnore]
		public virtual IList<ConsMixprop> ConsMixprops
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<DispatchList> DispatchLists
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<ProducePlan> ProducePlans
        {
            get;
            set;
        }
		 
		
       
		public virtual IList<IdentitySetting> TaskIdentities
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
		public virtual IList<PumpWork> PumpWorks
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
        public virtual IList<ConStrAssess> ConStrAssesses
        {
            get;
            set;
        }

        
        [ScriptIgnore]
        public virtual IList<ShippingDocument> ShippingDocuments
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<InsteadProduct> InsteadProducts
        {
            get;
            set;
        }
		
		
        #endregion
    }
}
