
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  采购合同抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _StockPact : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(StockPactNo);
			sb.Append(PactName);
			sb.Append(Amount);
			sb.Append(GageUnit);
			sb.Append(StockPrice);
			sb.Append(QualityNeed);
			sb.Append(EstablishTime);
			sb.Append(EstablishMan);
			sb.Append(ValidFrom);
			sb.Append(ValidTo);
			sb.Append(TaxRate);
			sb.Append(WarmPercent);
			sb.Append(WeighBy);
			sb.Append(SourceAddr);
			sb.Append(FootMode);
			sb.Append(Auditor);
			sb.Append(AuditTime);
			sb.Append(AuditStatus);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        [DisplayName("合同号")]
        [Required]
        public virtual string StockPactNo
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 合同名称
        /// </summary>
        [DisplayName("合同名称")]
        [StringLength(128)]
        [Required]
        public virtual string PactName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 数量
        /// </summary>
        [DisplayName("数量(吨)")]
        public virtual decimal Amount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 计量单位
        /// </summary>
        [DisplayName("计量单位")]
        [StringLength(20)]
        public virtual string GageUnit
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 采购单价(元/吨)
        /// </summary>
        [DisplayName("单价1")]
        public virtual decimal? StockPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 质量要求
        /// </summary>
        [DisplayName("质量要求")]
        [StringLength(256)]
        public virtual string QualityNeed
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 录入时间
        /// </summary>
        [DisplayName("录入时间")]
        public virtual System.DateTime? EstablishTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 录入人
        /// </summary>
        [DisplayName("录入人")]
        [StringLength(30)]
        public virtual string EstablishMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 开始有效日期
        /// </summary>
        [DisplayName("开始有效日期")]
        public virtual System.DateTime? ValidFrom
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 结束有效日期
        /// </summary>
        [DisplayName("结束有效日期")]
        public virtual System.DateTime? ValidTo
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 税率
        /// </summary>
        [DisplayName("税率")]
        public virtual decimal? TaxRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 报警百分比
        /// </summary>
        [DisplayName("报警百分比%")]
        [Range(0,100)]
        public virtual decimal WarmPercent
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 称重依据
        /// </summary>
        [DisplayName("称重依据")]
        [StringLength(20)]
        public virtual string WeighBy
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 来源地
        /// </summary>
        [DisplayName("来源地")]
        [StringLength(50)]
        public virtual string SourceAddr
        {
            get;
			set;			 
        }
        /// <summary>
        /// 认磅差比例
        /// </summary>
        [DisplayName("认磅差比例")]
        public virtual decimal? BangchaRate
        {
            get;
            set;
        }
        /// <summary>
        /// 低于情况
        /// </summary>
        [DisplayName("低于情况")]
        public virtual string lowbangcha
        {
            get;
            set;
        }
        /// <summary>
        /// 高于情况
        /// </summary>
        [DisplayName("高于情况")]
        public virtual string highbangcha
        {
            get;
            set;
        }
        /// <summary>
        /// 垫资字段一
        /// </summary>
        [DisplayName("垫资字段一")]
        public virtual decimal? Dianzi1
        {
            get;
            set;
        }
        /// <summary>
        /// 垫资字段二
        /// </summary>
        [DisplayName("垫资字段二")]
        public virtual decimal? Dianzi2
        {
            get;
            set;
        }
        /// <summary>
        /// 垫资字段三
        /// </summary>
        [DisplayName("垫资字段三")]
        public virtual decimal? Dianzi3
        {
            get;
            set;
        }
        /// <summary>
        /// 垫资字段四
        /// </summary>
        [DisplayName("垫资字段四")]
        public virtual decimal? Dianzi4
        {
            get;
            set;
        }
        /// <summary>
        /// 垫资字段五
        /// </summary>
        [DisplayName("垫资字段五")]
        public virtual decimal? Dianzi5
        {
            get;
            set;
        }
        /// <summary>
        /// 垫资字段六
        /// </summary>
        [DisplayName("垫资字段六")]
        public virtual decimal? Dianzi6
        {
            get;
            set;
        }
        /// <summary>
        /// 垫资字段七
        /// </summary>
        [DisplayName("垫资字段七")]
        public virtual decimal? Dianzi7
        {
            get;
            set;
        }
        /// <summary>
        /// 垫资字符串
        /// </summary>
        [DisplayName("垫资字符串")]
        public virtual string DianziString
        {
            get;
            set;
        }
        /// <summary>
        /// 结算方式
        /// </summary>
        [DisplayName("结算方式")]
        [StringLength(20)]
        public virtual string FootMode
        {
            get;
			set;			 
        }
        /// <summary>
        /// 垫资类型
        /// </summary>
        [DisplayName("垫资类型")]
        public virtual string DianziType
        {
            get;
            set;
        }
        /// <summary>
        /// 垫资数量
        /// </summary>
        [DisplayName("垫资数量")]
        public virtual int? DianziNum
        {
            get;
            set;
        }
        /// <summary>
        /// 垫资金额
        /// </summary>
        [DisplayName("垫资金额")]
        public virtual decimal? DianziMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 垫资情况
        /// </summary>
        [DisplayName("垫资情况")]
        public virtual int IsDianzi
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
        public virtual int? AuditStatus
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
        /// 采购材料1
        /// </summary>
        [DisplayName("单价2")]
        public virtual decimal? StockPrice1
        {
            get;
            set;
        }
        /// <summary>
        /// 采购材料2
        /// </summary>
        [DisplayName("单价3")]
        public virtual decimal? StockPrice2
        {
            get;
            set;
        }
        /// <summary>
        /// 采购材料3
        /// </summary>
        [DisplayName("单价4")]
        public virtual decimal? StockPrice3
        {
            get;
            set;
        }
        /// <summary>
        /// 采购材料4
        /// </summary>
        [DisplayName("单价5")]
        public virtual decimal? StockPrice4
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual SupplyInfo SupplyInfo
        {
            get;
			set;
        }
        [ScriptIgnore]
		public virtual StuffInfo StuffInfo
        {
            get;
			set;
        }

        [ScriptIgnore]
        public virtual StuffInfo StuffInfo1
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual StuffInfo StuffInfo2
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual StuffInfo StuffInfo3
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual StuffInfo StuffInfo4
        {
            get;
            set;
        } 
		
        [ScriptIgnore]
		public virtual IList<StockPlan> StockPlans
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<StuffIn> StuffIns
        {
            get;
            set;
        }
        #endregion
    }
}
