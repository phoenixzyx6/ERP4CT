
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  运输单历史记录抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ShippingDocumentHistory : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(TaskID);
			sb.Append(ContractID);
			sb.Append(ContractName);
			sb.Append(CustomerID);
			sb.Append(CustName);
			sb.Append(CustMixpropID);
			sb.Append(ConsMixpropID);
			sb.Append(ProjectName);
			sb.Append(ProjectAddr);
			sb.Append(ShipDocType);
			sb.Append(ConStrength);
			sb.Append(CastMode);
			sb.Append(ConsPos);
			sb.Append(PumpName);
			sb.Append(ImpGrade);
			sb.Append(ImyGrade);
			sb.Append(ImdGrade);
			sb.Append(CarpRadii);
			sb.Append(CementBreed);
			sb.Append(RealSlump);
			sb.Append(BetonCount);
			sb.Append(SlurryCount);
			sb.Append(SendCube);
			sb.Append(ParCube);
            sb.Append(ShippingCube);
			sb.Append(SignInCube);
			sb.Append(ProvidedCube);
			sb.Append(PlanCube);
			sb.Append(ScrapCube);
			sb.Append(TransferCube);
			sb.Append(OtherCube);
            sb.Append(RemainCube);
            sb.Append(Cube);
			sb.Append(CarID);
			sb.Append(ProvidedTimes);
			sb.Append(SumPrice);
			sb.Append(DeliveryTime);
			sb.Append(ArriveTime);
			sb.Append(Driver);
			sb.Append(Surveyor);
			sb.Append(Signer);
			sb.Append(ForkLift);
			sb.Append(Operator);
			sb.Append(PlanClass);
			sb.Append(ProduceDate);
            sb.Append(ProductLineName);
            sb.Append(ProductLineID);
			sb.Append(SupplyUnit);
			sb.Append(ConstructUnit);
			sb.Append(EntrustUnit);
			sb.Append(Accepter);
			sb.Append(Distance);
			sb.Append(RegionID);
			sb.Append(LinkMan);
			sb.Append(Tel);
			sb.Append(ProjectID);
			sb.Append(IsEffective);
			sb.Append(IsBack);
			sb.Append(IsAudit);
			sb.Append(PrintCount);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 任务单号
        /// </summary>
        [DisplayName("任务单号")]
        [StringLength(30)]
        [Required]
        public virtual string TaskID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 合同编号
        /// </summary>
        [DisplayName("合同编号")]
        [StringLength(30)]
        public virtual string ContractID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 合同名称
        /// </summary>
        [DisplayName("合同名称")]
        [StringLength(128)]
        public virtual string ContractName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 客户编号
        /// </summary>
        [DisplayName("客户编号")]
        [StringLength(30)]
        public virtual string CustomerID
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
        /// 砂浆配合比编号
        /// </summary>
        [DisplayName("砂浆配比号")]
        [StringLength(30)]
        public virtual string SlurryConsMixpropID
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
        /// 运输单类型
        /// </summary>
        [DisplayName("运输单类型")]
        [StringLength(50)]
        public virtual string ShipDocType
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
        /// 如一楼地面、三层梁板 施工部位
        /// </summary>
        [DisplayName("如一楼地面、三层梁板 施工部位")]
        [StringLength(100)]
        public virtual string ConsPos
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 泵名称
        /// </summary>
        [DisplayName("泵名称")]
        [StringLength(50)]
        public virtual string PumpName
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
        /// 骨料粒径
        /// </summary>
        [DisplayName("骨料粒径")]
        public virtual decimal? CarpRadii
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
        /// 混凝土方量
        /// </summary>
        [Required]
        [DisplayName("混凝土方量")]
        public virtual decimal BetonCount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砂浆方量
        /// </summary>
        [Required]
        [DisplayName("砂浆方量")]
        public virtual decimal SlurryCount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 调度方量
        /// </summary>
        [Required]
        [DisplayName("调度方量")]
        public virtual decimal SendCube
        {
            get;
			set;			 
        }	
 
        /// <summary>
        /// 出票方量
        /// </summary>
        [Required]
        [DisplayName("出票方量")]
        public virtual decimal ParCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 上车余料
        /// </summary>
        [DisplayName("上车余料")]
        public virtual decimal? RemainCube
        {
            get;
			set;			 
        }
        /// <summary>
        /// 运输方量
        /// </summary>
        [Required]
        [DisplayName("运输方量")]
        public virtual decimal ShippingCube 
        { 
            get; 
            set; 
        }
        /// <summary>
        /// 签收方量
        /// </summary>
        [Required]
        [DisplayName("签收方量")]
        public virtual decimal SignInCube
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
        /// 报废方量
        /// </summary>
        [Required]
        [DisplayName("报废方量")]
        public virtual decimal ScrapCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 本车余料
        /// </summary>
        [Required]
        [DisplayName("本车余料")]
        public virtual decimal TransferCube
        {
            get;
			set;			 
        }
        /// <summary>
        /// 其他方量
        /// </summary>
        [Required]
        [DisplayName("其他方量")]
        public virtual decimal OtherCube
        {
            get;
			set;			 
        }

        /// <summary>
        /// 过磅方量
        /// </summary>
        [Required]
        [DisplayName("过磅方量")]
        public virtual decimal Cube
        {
            get;
            set;
        }
        /// <summary>
        /// 过磅毛重
        /// </summary>
        [Required]
        [DisplayName("过磅毛重")]
        public virtual int TotalWeight
        {
            get;
            set;
        }

        /// <summary>
        /// 过磅皮重
        /// </summary>
        [Required]
        [DisplayName("过磅皮重")]
        public virtual int CarWeight
        {
            get;
            set;
        }

        /// <summary>
        /// 过磅净重
        /// </summary>
        [Required]
        [DisplayName("过磅净重")]
        public virtual int Weight
        {
            get;
            set;
        }

        /// <summary>
        /// 换算率
        /// </summary>
        [Required]
        [DisplayName("换算率")]
        public virtual int Exchange
        {
            get;
            set;
        }	

        /// <summary>
        /// 运输车号
        /// </summary>
        [DisplayName("运输车号")]
        [StringLength(30)]
        [Required]
        public virtual string CarID
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
        /// 运费
        /// </summary>
        [DisplayName("运费")]
        public virtual decimal? SumPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 出站时间
        /// </summary>
        [DisplayName("出站时间")]
        public virtual System.DateTime? DeliveryTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 到场时间
        /// </summary>
        [DisplayName("到场时间")]
        public virtual System.DateTime? ArriveTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 当班司机
        /// </summary>
        [DisplayName("当班司机")]
        [StringLength(30)]
        public virtual string Driver
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 质检员
        /// </summary>
        [DisplayName("质检员")]
        [StringLength(30)]
        public virtual string Surveyor
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 发货员
        /// </summary>
        [DisplayName("发货员")]
        [StringLength(30)]
        public virtual string Signer
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
        /// 经办人
        /// </summary>
        [DisplayName("经办人")]
        [StringLength(30)]
        public virtual string Operator
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
        /// 生产日期
        /// </summary>
        [DisplayName("生产日期")]
        public virtual System.DateTime? ProduceDate
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
        /// 审核人
        /// </summary>
        [DisplayName("审核人")]
        [StringLength(30)]
        public virtual string AuditMan
        {
            get;
            set;
        }

        /// <summary>
        /// 审核时间
        /// </summary>
        [DisplayName("审核时间")]
        public virtual DateTime? AuditTime
        {
            get;
            set;
        }

        /// <summary>
        /// 生产线ID
        /// </summary>
        [DisplayName("生产线ID")]
        [StringLength(20)]
        public virtual string ProductLineID
        {
            get;
            set;
        }
        /// <summary>
        /// 供应单位
        /// </summary>
        [DisplayName("供应单位")]
        [StringLength(128)]
        public virtual string SupplyUnit
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
        /// 委托单位
        /// </summary>
        [DisplayName("委托单位")]
        [StringLength(128)]
        public virtual string EntrustUnit
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 现场验收人
        /// </summary>
        [DisplayName("现场验收人")]
        [StringLength(30)]
        public virtual string Accepter
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
        /// 区间范围
        /// </summary>
        [StringLength(30)]
        [DisplayName("区间范围")]
        public virtual string RegionID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 联系人
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
        /// 工程编号
        /// </summary>
        [DisplayName("工程编号")]
        public virtual string ProjectID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否有效
        /// </summary>
        [Required]
        [DisplayName("是否有效")]
        public virtual bool IsEffective
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否带回
        /// </summary>
        [Required]
        [DisplayName("是否带回")]
        public virtual bool IsBack
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否审核
        /// </summary>
        [Required]
        [DisplayName("是否审核")]
        public virtual bool IsAudit
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否生产
        /// </summary>
        [Required]
        [DisplayName("是否生产")]
        public virtual bool IsProduce
        {
            get;
			set;			 
        }
        /// <summary>
        /// 打印次数
        /// </summary>
        [DisplayName("打印次数")]
        public virtual int? PrintCount
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
			 
		
        #endregion
    }
}
