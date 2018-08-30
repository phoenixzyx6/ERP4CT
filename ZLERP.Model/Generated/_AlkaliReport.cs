
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 碱含量计算书抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _AlkaliReport : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ConStrength);
			sb.Append(CEStuffID);
			sb.Append(CEExamID);
			sb.Append(CESupplyID);
			sb.Append(CEAmount);
			sb.Append(CEAlkPer);
			sb.Append(ADM1StuffID);
			sb.Append(ADM1ExamID);
			sb.Append(ADM1SupplyID);
			sb.Append(ADM1Amount);
			sb.Append(ADM1AlkPer);
			sb.Append(AIR1StuffID);
			sb.Append(AIR1ExamID);
			sb.Append(AIR1SupplyID);
			sb.Append(AIR1Amount);
			sb.Append(AIR1AlkPer);
			sb.Append(AIR2StuffID);
			sb.Append(AIR2ExamID);
			sb.Append(AIR2SupplyID);
			sb.Append(AIR2Amount);
			sb.Append(AIR2AlkPer);
			sb.Append(ADM2StuffID);
			sb.Append(ADM2ExamID);
			sb.Append(ADM2SupplyID);
			sb.Append(ADM2Amount);
			sb.Append(ADM2AlkPer);
			sb.Append(ADM3StuffID);
			sb.Append(ADM3ExamID);
			sb.Append(ADM3SupplyID);
			sb.Append(ADM3Amount);
			sb.Append(ADM3AlkPer);
			sb.Append(AlkAmount);
			sb.Append(ExamResult);
			sb.Append(Assessor);
			sb.Append(Principal);
			sb.Append(Accountor);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

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
        /// 水泥碱含量%
        /// </summary>
        [DisplayName("水泥碱含量%")]
        public virtual decimal? CEAlkPer
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
        /// 外加剂碱含量%
        /// </summary>
        [DisplayName("外加剂碱含量%")]
        public virtual decimal? ADM1AlkPer
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
        /// 煤灰碱含量%
        /// </summary>
        [DisplayName("煤灰碱含量%")]
        public virtual decimal? AIR1AlkPer
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
        /// 矿粉碱含量%
        /// </summary>
        [DisplayName("矿粉碱含量%")]
        public virtual decimal? AIR2AlkPer
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
        /// 膨胀剂碱含量%
        /// </summary>
        [DisplayName("膨胀剂碱含量%")]
        public virtual decimal? ADM2AlkPer
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
        /// 其他外加剂碱含量%
        /// </summary>
        [DisplayName("其他外加剂碱含量%")]
        public virtual decimal? ADM3AlkPer
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 碱总含量KG
        /// </summary>
        [DisplayName("碱总含量KG")]
        public virtual decimal? AlkAmount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 检测结论
        /// </summary>
        [DisplayName("检测结论")]
        [StringLength(256)]
        public virtual string ExamResult
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 审批人
        /// </summary>
        [DisplayName("审批人")]
        [StringLength(30)]
        public virtual string Assessor
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
        /// 计算
        /// </summary>
        [DisplayName("计算")]
        [StringLength(30)]
        public virtual string Accountor
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
