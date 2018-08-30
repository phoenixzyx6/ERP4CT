
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 开盘鉴定表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _OpenCheck : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ReSlump);
			sb.Append(DeSlump);
            sb.Append(ConStrength);
			sb.Append(WCRate);
			sb.Append(SCRate);
			sb.Append(SIWRate);
			sb.Append(RIWRate);
			sb.Append(SIRRate);
            sb.Append(WAStuffID);
            sb.Append(WAAmount);
			sb.Append(CEStuffID);
			sb.Append(CEExamID);
			sb.Append(CESupplyID);
			sb.Append(CEAmount);
			sb.Append(FAStuffID);
			sb.Append(FAExamID);
			sb.Append(FASupplyID);
			sb.Append(FAAmount);
			sb.Append(CAStuffID);
			sb.Append(CAExamID);
			sb.Append(CASupplyID);
			sb.Append(CAAmount);
			sb.Append(ADM1StuffID);
			sb.Append(ADM1ExamID);
			sb.Append(ADM1SupplyID);
			sb.Append(ADM1Amount);
			sb.Append(AIR1StuffID);
			sb.Append(AIR1ExamID);
			sb.Append(AIR1SupplyID);
			sb.Append(AIR1Amount);
			sb.Append(AIR2StuffID);
			sb.Append(AIR2ExamID);
			sb.Append(AIR2SupplyID);
			sb.Append(AIR2Amount);
			sb.Append(ADM2StuffID);
			sb.Append(ADM2ExamID);
			sb.Append(ADM2SupplyID);
			sb.Append(ADM2Amount);
			sb.Append(ADM3StuffID);
			sb.Append(ADM3ExamID);
			sb.Append(ADM3SupplyID);
			sb.Append(ADM3Amount);
			sb.Append(Slump);
			sb.Append(MixGrade);
			sb.Append(OtherCap);
			sb.Append(CheckResult);
			sb.Append(AdjustMode);
			sb.Append(Remark);
			sb.Append(Principal);
			sb.Append(CheckMan);
			sb.Append(IsOut);
			sb.Append(OpenTime);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 要求坍落度
        /// </summary>
        [DisplayName("要求坍落度")]
        [StringLength(20)]
        public virtual string ReSlump
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 设计坍落度
        /// </summary>
        [DisplayName("设计坍落度")]
        [StringLength(20)]
        public virtual string DeSlump
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
        /// 大石含水率
        /// </summary>
        [DisplayName("大石含水率")]
        public virtual decimal? RIWRate1
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
        /// <summary>
        /// 水原料
        /// </summary>
        [DisplayName("水原料")]
        [StringLength(50)]
        public virtual string WAStuffID
        {
            get;
            set;
        }
        /// <summary>
        /// 水用量
        /// </summary>
        [DisplayName("水用量")]
        public virtual decimal? WAAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 水检验
        /// </summary>
        [DisplayName("水检验")]
        [StringLength(50)]
        public virtual string WAExamID
        {
            get;
            set;
        }
        /// <summary>
        /// 水泥原料
        /// </summary>
        [DisplayName("水泥原料")]
        [StringLength(50)]
        public virtual string CEStuffID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 水泥试验
        /// </summary>
        [DisplayName("水泥试验")]
        [StringLength(50)]
        public virtual string CEExamID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 水泥厂商
        /// </summary>
        [DisplayName("水泥厂商")]
        [StringLength(50)]
        public virtual string CESupplyID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 水泥用量
        /// </summary>
        [DisplayName("水泥用量")]
        public virtual decimal? CEAmount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砂原料
        /// </summary>
        [DisplayName("砂原料")]
        [StringLength(50)]
        public virtual string FAStuffID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砂试验
        /// </summary>
        [DisplayName("砂试验")]
        [StringLength(50)]
        public virtual string FAExamID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砂厂商
        /// </summary>
        [DisplayName("砂厂商")]
        [StringLength(50)]
        public virtual string FASupplyID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砂用量
        /// </summary>
        [DisplayName("砂用量")]
        public virtual decimal? FAAmount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 石原料
        /// </summary>
        [DisplayName("石原料")]
        [StringLength(50)]
        public virtual string CAStuffID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 石试验
        /// </summary>
        [DisplayName("石试验")]
        [StringLength(50)]
        public virtual string CAExamID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 石厂商
        /// </summary>
        [DisplayName("石厂商")]
        [StringLength(50)]
        public virtual string CASupplyID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 石用量
        /// </summary>
        [DisplayName("石用量")]
        public virtual decimal? CAAmount
        {
            get;
			set;			 
        }

        /// <summary>
        /// 小石原料
        /// </summary>
        [DisplayName("小石原料")]
        [StringLength(50)]
        public virtual string CA1StuffID
        {
            get;
            set;
        }
        /// <summary>
        /// 小石试验
        /// </summary>
        [DisplayName("小石试验")]
        [StringLength(50)]
        public virtual string CA1ExamID
        {
            get;
            set;
        }
        /// <summary>
        /// 小石厂商
        /// </summary>
        [DisplayName("小石厂商")]
        [StringLength(50)]
        public virtual string CA1SupplyID
        {
            get;
            set;
        }
        /// <summary>
        /// 小石用量
        /// </summary>
        [DisplayName("小石用量")]
        public virtual decimal? CA1Amount
        {
            get;
            set;
        }	
        /// <summary>
        /// 外加剂原料
        /// </summary>
        [DisplayName("外加剂原料")]
        [StringLength(50)]
        public virtual string ADM1StuffID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 外加剂试验
        /// </summary>
        [DisplayName("外加剂试验")]
        [StringLength(50)]
        public virtual string ADM1ExamID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 外加剂厂商
        /// </summary>
        [DisplayName("外加剂厂商")]
        [StringLength(50)]
        public virtual string ADM1SupplyID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 外加剂用量
        /// </summary>
        [DisplayName("外加剂用量")]
        public virtual decimal? ADM1Amount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 煤灰原料
        /// </summary>
        [DisplayName("煤灰原料")]
        [StringLength(50)]
        public virtual string AIR1StuffID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 煤灰试验
        /// </summary>
        [DisplayName("煤灰试验")]
        [StringLength(50)]
        public virtual string AIR1ExamID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 煤灰厂商
        /// </summary>
        [DisplayName("煤灰厂商")]
        [StringLength(50)]
        public virtual string AIR1SupplyID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 煤灰用量
        /// </summary>
        [DisplayName("煤灰用量")]
        public virtual decimal? AIR1Amount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 矿粉原料
        /// </summary>
        [DisplayName("矿粉原料")]
        [StringLength(50)]
        public virtual string AIR2StuffID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 矿粉试验
        /// </summary>
        [DisplayName("矿粉试验")]
        [StringLength(50)]
        public virtual string AIR2ExamID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 矿粉厂商
        /// </summary>
        [DisplayName("矿粉厂商")]
        [StringLength(50)]
        public virtual string AIR2SupplyID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 矿粉用量
        /// </summary>
        [DisplayName("矿粉用量")]
        public virtual decimal? AIR2Amount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 膨胀剂原料
        /// </summary>
        [DisplayName("膨胀剂原料")]
        [StringLength(50)]
        public virtual string ADM2StuffID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 膨胀剂试验
        /// </summary>
        [DisplayName("膨胀剂试验")]
        [StringLength(50)]
        public virtual string ADM2ExamID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 膨胀剂厂商
        /// </summary>
        [DisplayName("膨胀剂厂商")]
        [StringLength(50)]
        public virtual string ADM2SupplyID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 膨胀剂用量
        /// </summary>
        [DisplayName("膨胀剂用量")]
        public virtual decimal? ADM2Amount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 其他外加剂原料
        /// </summary>
        [DisplayName("其他外加剂原料")]
        [StringLength(50)]
        public virtual string ADM3StuffID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 其他外加剂试验
        /// </summary>
        [DisplayName("其他外加剂试验")]
        [StringLength(50)]
        public virtual string ADM3ExamID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 其他外加剂厂商
        /// </summary>
        [DisplayName("其他外加剂厂商")]
        [StringLength(50)]
        public virtual string ADM3SupplyID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 其他外加剂用量
        /// </summary>
        [DisplayName("其他外加剂用量")]
        public virtual decimal? ADM3Amount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 出机坍落度
        /// </summary>
        [DisplayName("出机坍落度")]
        [StringLength(50)]
        public virtual string Slump
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 和易性
        /// </summary>
        [DisplayName("和易性")]
        [StringLength(50)]
        public virtual string MixGrade
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 其他性能
        /// </summary>
        [DisplayName("其他性能")]
        [StringLength(128)]
        public virtual string OtherCap
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 鉴定结果
        /// </summary>
        [DisplayName("鉴定结果")]
        [StringLength(50)]
        public virtual string CheckResult
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 调整方式
        /// </summary>
        [DisplayName("调整方式")]
        [StringLength(128)]
        public virtual string AdjustMode
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
        /// 负责人
        /// </summary>
        [DisplayName("负责人")]
        [StringLength(30)]
        public virtual string Principal
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 鉴定人
        /// </summary>
        [DisplayName("鉴定人")]
        [StringLength(30)]
        public virtual string CheckMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 已出开盘
        /// </summary>
        [Required]
        [DisplayName("已出开盘")]
        public virtual bool IsOut
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
        [ScriptIgnore]
		public virtual CustMixprop CustMixprop
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual ProduceTask ProduceTask
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
