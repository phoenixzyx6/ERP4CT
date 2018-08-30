
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  施工配比抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ConsMixprop : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ConStrength);
			sb.Append(ImpGrade);
			sb.Append(FlexStrength);
			sb.Append(SCRate);
			sb.Append(WCRate);
			sb.Append(Weight);
			sb.Append(SeasonType);
			sb.Append(Auditor);
			sb.Append(AuditTime);
            sb.Append(AuditStatus);
			sb.Append(IsSlurry);
			sb.Append(MixingTime);
			sb.Append(SynStatus);
			sb.Append(ProductLineID);
			sb.Append(Remark);
			sb.Append(S1_wet);
			sb.Append(S2_wet);
			sb.Append(S3_wet);
			sb.Append(S4_wet);
			sb.Append(S5_wet);
			sb.Append(S6_wet);
			sb.Append(S7_wet);
			sb.Append(S8_wet);
			sb.Append(S9_wet);
			sb.Append(S10_wet);
			sb.Append(S11_wet);
			sb.Append(S12_wet);
			sb.Append(S13_wet);
			sb.Append(S14_wet);
			sb.Append(S15_wet);
			sb.Append(S16_wet);
			sb.Append(S17_wet);
			sb.Append(S18_wet);
			sb.Append(S19_wet);
			sb.Append(S20_wet);
			sb.Append(S21_wet);
			sb.Append(S22_wet);
			sb.Append(S23_wet);
			sb.Append(S24_wet);
            sb.Append(IsCompleted);
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
        /// 砂率
        /// </summary>
        [DisplayName("砂率")]
        public virtual decimal? SCRate
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
        /// 设计容重
        /// </summary>
        [DisplayName("设计容重")]
        public virtual decimal? Weight
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
        /// 搅拌时间
        /// </summary>
        [DisplayName("搅拌时间")]
        public virtual int? MixingTime
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
        /// 材料一湿料用量
        /// </summary>
        [DisplayName("材料一湿料用量")]
        public virtual decimal? S1_wet
        {
            get;
			set;			 
        }		
        /// <summary>
        /// 材料二湿料用量
        /// </summary>
        [DisplayName("材料二湿料用量")]
        public virtual decimal? S2_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料三湿料用量
        /// </summary>
        [DisplayName("材料三湿料用量")]
        public virtual decimal? S3_wet
        {
            get;
			set;			 
        }		
        /// <summary>
        /// 材料四湿料用量
        /// </summary>
        [DisplayName("材料四湿料用量")]
        public virtual decimal? S4_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料五湿料用量
        /// </summary>
        [DisplayName("材料五湿料用量")]
        public virtual decimal? S5_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料六湿料用量
        /// </summary>
        [DisplayName("材料六湿料用量")]
        public virtual decimal? S6_wet
        {
            get;
			set;			 
        }		
        /// <summary>
        /// 材料七湿料用量
        /// </summary>
        [DisplayName("材料七湿料用量")]
        public virtual decimal? S7_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料八湿料用量
        /// </summary>
        [DisplayName("材料八湿料用量")]
        public virtual decimal? S8_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料九湿料用量
        /// </summary>
        [DisplayName("材料九湿料用量")]
        public virtual decimal? S9_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十湿料用量
        /// </summary>
        [DisplayName("材料十湿料用量")]
        public virtual decimal? S10_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十一湿料用量
        /// </summary>
        [DisplayName("材料十一湿料用量")]
        public virtual decimal? S11_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十二湿料用量
        /// </summary>
        [DisplayName("材料十二湿料用量")]
        public virtual decimal? S12_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十三湿料用量
        /// </summary>
        [DisplayName("材料十三湿料用量")]
        public virtual decimal? S13_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十四湿料用量
        /// </summary>
        [DisplayName("材料十四湿料用量")]
        public virtual decimal? S14_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十五湿料用量
        /// </summary>
        [DisplayName("材料十五湿料用量")]
        public virtual decimal? S15_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十六湿料用量
        /// </summary>
        [DisplayName("材料十六湿料用量")]
        public virtual decimal? S16_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十七湿料用量
        /// </summary>
        [DisplayName("材料十七湿料用量")]
        public virtual decimal? S17_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十八湿料用量
        /// </summary>
        [DisplayName("材料十八湿料用量")]
        public virtual decimal? S18_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十九湿料用量
        /// </summary>
        [DisplayName("材料十九湿料用量")]
        public virtual decimal? S19_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料二十湿料用量
        /// </summary>
        [DisplayName("材料二十湿料用量")]
        public virtual decimal? S20_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料二十一湿料用量
        /// </summary>
        [DisplayName("材料二十一湿料用量")]
        public virtual decimal? S21_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料二十二湿料用量
        /// </summary>
        [DisplayName("材料二十二湿料用量")]
        public virtual decimal? S22_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料二十三湿料用量
        /// </summary>
        [DisplayName("材料二十三湿料用量")]
        public virtual decimal? S23_wet
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料二十四湿料用量
        /// </summary>
        [DisplayName("材料二十四湿料用量")]
        public virtual decimal? S24_wet
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
        [ScriptIgnore]
		public virtual ProduceTask ProduceTask
        {
            get;
			set;
        }
        [ScriptIgnore]
        public virtual Formula Formula
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
        public virtual ProductLine ProductLine
        {
            get;
            set;
        }	
        [ScriptIgnore]
		public virtual IList<ConsMixpropItem> ConsMixpropItems
        {
            get;
            set;
        }

        [DisplayName("最后确定容重")]
        public virtual int LastWeight
        {
            set;
            get;
        }
		
        #endregion

    }
}
