
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _TranAcc : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(AccTime);
			sb.Append(AccAddr);
			sb.Append(AccClass);
			sb.Append(AccReason);
			sb.Append(AccDes);
			sb.Append(AccMan);
			sb.Append(InsureCorp);
			sb.Append(InsureNo);
			sb.Append(AccCarID);
			sb.Append(LossPart);
			sb.Append(LossAmount);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 事故时间
        /// </summary>
        [DisplayName("事故时间")]
        [Required]
        public virtual System.DateTime? AccTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 事故地点
        /// </summary>
        [DisplayName("事故地点")]
        [StringLength(256)]
        public virtual string AccAddr
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 事故种类
        /// </summary>
        [DisplayName("事故种类")]
        [StringLength(128)]
        public virtual string AccClass
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 事故原因
        /// </summary>
        [DisplayName("事故原因")]
        [StringLength(256)]
        public virtual string AccReason
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 事故描述
        /// </summary>
        [DisplayName("事故描述")]
        [StringLength(256)]
        public virtual string AccDes
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 当事人
        /// </summary>
        [DisplayName("当事人")]
        [StringLength(128)]
        public virtual string AccMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 保险公司
        /// </summary>
        [DisplayName("保险公司")]
        [StringLength(256)]
        public virtual string InsureCorp
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 保险单号
        /// </summary>
        [DisplayName("保险单号")]
        [StringLength(256)]
        public virtual string InsureNo
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 事故车辆
        /// </summary>
        [DisplayName("事故车辆")]
        [StringLength(128)]
        public virtual string AccCarID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 损失部分
        /// </summary>
        [DisplayName("损失部分")]
        [StringLength(256)]
        public virtual string LossPart
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 损失金额
        /// </summary>
        [DisplayName("损失金额")]
        public virtual decimal? LossAmount
        {
            get;
			set;			 
        }	
        #endregion
    }
}
