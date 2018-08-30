
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  供货厂家信息抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _SupplyInfo : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ShortName);
			sb.Append(SupplyKind);
			sb.Append(SupplyName);
			sb.Append(Principal);
			sb.Append(SupplyAddr);
			sb.Append(InvoiceAddr);
			sb.Append(BankName);
			sb.Append(BankAccount);
			sb.Append(BusinessTel);
			sb.Append(BusinessFax);
			sb.Append(PostCode);
			sb.Append(PrincipalTel);
			sb.Append(LinkMan);
			sb.Append(LinkTel);
			sb.Append(SupplyType);
			sb.Append(CreditWorthiness);
			sb.Append(Email);
			sb.Append(IsUsed);
			sb.Append(IsTax);
			sb.Append(Remark);
			sb.Append(Version);
			sb.Append(SupplyID);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 简称
        /// </summary>
        
        [DisplayName("简称")]
        [StringLength(50)]
        public virtual string ShortName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 厂商类型
        /// </summary>
        [DisplayName("厂商类型")]
        [StringLength(50)]
        public virtual string SupplyKind
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 供货厂家
        /// </summary>
        [Required]
        [DisplayName("厂家全称")]
        [StringLength(128)]
        public virtual string SupplyName
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
        /// 厂家地址
        /// </summary>
        [DisplayName("厂家地址")]
        [StringLength(128)]
        public virtual string SupplyAddr
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 发票地址
        /// </summary>
        [DisplayName("发票地址")]
        [StringLength(128)]
        public virtual string InvoiceAddr
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 开户银行
        /// </summary>
        [DisplayName("开户银行")]
        [StringLength(128)]
        public virtual string BankName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 开户银行帐号
        /// </summary>
        [DisplayName("开户银行帐号")]
        [StringLength(50)]
        public virtual string BankAccount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 营业电话
        /// </summary>
        [DisplayName("营业电话")]
        [StringLength(20)]
        public virtual string BusinessTel
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 营业传真
        /// </summary>
        [DisplayName("营业传真")]
        [StringLength(20)]
        public virtual string BusinessFax
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 邮政编码
        /// </summary>
        [DisplayName("邮政编码")]
        [StringLength(20)]
        public virtual string PostCode
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 负责人电话
        /// </summary>
        [DisplayName("负责人电话")]
        [StringLength(20)]
        public virtual string PrincipalTel
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 联络人
        /// </summary>
        [DisplayName("联络人")]
        [StringLength(30)]
        public virtual string LinkMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 联络人手机
        /// </summary>
        [DisplayName("联络人手机")]
        [StringLength(20)]
        public virtual string LinkTel
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 供货类型
        /// </summary>
        [DisplayName("供货类型")]
        [StringLength(50)]
        public virtual string SupplyType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 信誉等级
        /// </summary>
        [DisplayName("信誉等级")]
        [StringLength(50)]
        public virtual string CreditWorthiness
        {
            get;
			set;			 
        }	
        /// <summary>
        /// Email
        /// </summary>
        [DisplayName("Email")]
        [StringLength(50)]
        public virtual string Email
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        [DisplayName("是否启用")]
        public virtual bool IsUsed
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 含税否
        /// </summary>
        [Required]
        [DisplayName("含税否")]
        public virtual bool IsTax
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
        /// 供货厂家编号
        /// </summary>
        [DisplayName("供货厂家编号")]
        [StringLength(30)]
        public virtual string SupplyID
        {
            get;
			set;			 
        }
        /// <summary>
        /// 是否内转
        /// </summary>
        [Required]
        [DisplayName("是否内转")]
        public virtual bool IsNz
        {
            get;
            set;
        }

        [ScriptIgnore]
		public virtual IList<StockPact> StockPacts
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
		public virtual IList<StuffIn> StuffIns1
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<StuffIn> StuffIns2
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual IList<StuffInfo> StuffInfos
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual IList<Equipment> Equipments
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<StuffPayableAdjust> StuffPayableAdjusts
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<Lab_Record> Lab_Records
        {
            get;
            set;
        }
        #endregion 
    }
}
