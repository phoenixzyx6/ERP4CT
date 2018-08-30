
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

using ZLERP.Model.Enums;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 客户表 客户信息抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Customer : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(CustName);
			sb.Append(ShortName);
			sb.Append(CustType);
			sb.Append(RegFund);
			sb.Append(CachetPath);
			sb.Append(BusinessAddr);
			sb.Append(InvoiceAddr);
			sb.Append(BusinessTel);
			sb.Append(BusinessFax);
			sb.Append(Email);
			sb.Append(PostCode);
			sb.Append(PrincipalSex);
			sb.Append(Principal);
			sb.Append(PrincipalTel);
			sb.Append(LinkMan);
			sb.Append(LinkTel);
			sb.Append(Buyer);
			sb.Append(AddrCode);
			sb.Append(CreditQuota);
			sb.Append(IsAudit);
            sb.Append(IsUse);
			sb.Append(TaxCode);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 客户名称
        /// </summary>
        [Required]
        [DisplayName("客户名称")]
        [StringLength(128)]
        public virtual string CustName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 简称
        /// </summary>
        [DisplayName("简称")]
        [StringLength(128)]
        public virtual string ShortName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 客户类型
        /// </summary>
        [DisplayName("客户类型")]
        [StringLength(50)]
        public virtual string CustType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 注册资金
        /// </summary>
        [DisplayName("注册资金")]
        public virtual decimal? RegFund
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 企业印鉴
        /// </summary>
        [DisplayName("企业印鉴")]
        [StringLength(256)]
        public virtual string CachetPath
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 营业地址
        /// </summary>
        [DisplayName("营业地址")]
        [StringLength(256)]
        public virtual string BusinessAddr
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 发票地址
        /// </summary>
        [DisplayName("发票地址")]
        [StringLength(256)]
        public virtual string InvoiceAddr
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
        [StringLength(128)]
        public virtual string BusinessFax
        {
            get;
			set;			 
        }	
        /// <summary>
        /// Email
        /// </summary>
        [DisplayName("Email")]
        [StringLength(50)]
        //[Email(ErrorMessage = "请输入正确的Email格式\n示例：abc@123.com")]
        [RegularExpression(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$", ErrorMessage =Consts.ValidatEmail )]
        public virtual string Email
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 邮政编码
        /// </summary>
        [DisplayName("邮政编码")]
        [StringLength(50)]
        public virtual string PostCode
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 负责人性别
        /// </summary>
        [DisplayName("负责人性别")]
        [StringLength(50)]
        public virtual string PrincipalSex
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
        /// 联系人
        /// </summary>
        [DisplayName("联系人")]
        [StringLength(30)]
        public virtual string LinkMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 联系人电话
        /// </summary>
        [DisplayName("联系人电话")]
        [StringLength(20)]
        public virtual string LinkTel
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 负责采购人
        /// </summary>
        [DisplayName("负责采购人")]
        [StringLength(30)]
        public virtual string Buyer
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 地点代码
        /// </summary>
        [DisplayName("地点代码")]
        [StringLength(50)]
        public virtual string AddrCode
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 信用额度
        /// </summary>
        [DisplayName("信用额度")]
        public virtual decimal? CreditQuota
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
        /// 税务代码
        /// </summary>
        [DisplayName("税务代码")]
        [StringLength(50)]
        public virtual string TaxCode
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
        /// 是否启用
        /// </summary>
        [Required]
        [DisplayName("是否启用")]
        public virtual bool IsUse
        {
            get;
            set;
        }	
        [ScriptIgnore]
		public virtual IList<Bank> Banks
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<Contract> Contracts
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
		
        #endregion
    }
}
