
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  合同信息抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Contract : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ContractNo);
			sb.Append(ContractName);
			sb.Append(BuildUnit);
			sb.Append(ProjectAddr);
			sb.Append(SignTotalCube);
			sb.Append(SignDate);
			sb.Append(SignTotalMoney);
			sb.Append(ContractType);
			sb.Append(ValuationType);
			sb.Append(PaymentType);
			sb.Append(ContractStatus);
			sb.Append(Remark);
			sb.Append(yuejie);
			sb.Append(Auditor);
			sb.Append(AuditTime);
			sb.Append(AuditStatus);
            sb.Append(AuditInfo);
			sb.Append(Salesman);
			sb.Append(ALinkMan);
			sb.Append(ALinkTel);
			sb.Append(BLinkMan);
			sb.Append(BLinkTel);
			sb.Append(ContractLimitType);
			sb.Append(ContractST);
			sb.Append(ContractET);
			sb.Append(FootDate);
			sb.Append(PaymentDate);
			sb.Append(PayBackMan);
			sb.Append(PayBackTel);
			sb.Append(MatCube);
			sb.Append(PrepayCube);
            sb.Append(IsLimited);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 作为信息参考 合同号
        /// </summary>
        [DisplayName("合同号")]
        [StringLength(50)]
        public virtual string ContractNo
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 合同名称
        /// </summary>
        [DisplayName("合同名称")]
        [Required]
        [StringLength(128)]
        public virtual string ContractName
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
        /// 签订总方量
        /// </summary>
        [Required]
        [DisplayName("签订总方量")]
        public virtual decimal SignTotalCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 签订日期
        /// </summary>
        [DisplayName("签订日期")]
        public virtual System.DateTime? SignDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 签订总金额
        /// </summary>
        [DisplayName("签订总金额")]
        public virtual decimal? SignTotalMoney
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 其他合同、现金合同 合同类型
        /// </summary>
        [DisplayName("合同类型")]
        [StringLength(50)]
        public virtual string ContractType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 合同价、信息价、标准价、建议价 计价方式
        /// </summary>
        [DisplayName("计价方式")]
        [StringLength(50)]
        public virtual string ValuationType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 现金、期付 付款方式
        /// </summary>
        [DisplayName("付款方式")]
        [StringLength(50)]
        public virtual string PaymentType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 进行中、审核中、已完成、锁定 合同状态
        /// </summary>
        [DisplayName("合同状态")]
        [StringLength(50)]
        public virtual string ContractStatus
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
        /// 月结比例（%）
        /// </summary>
        [DisplayName("月结比例（%）")]
        public virtual int? yuejie
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
        /// 业务员
        /// </summary>
        [DisplayName("业务员")]
        [StringLength(30)]
        public virtual string Salesman
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 甲方联系人
        /// </summary>
        [DisplayName("甲方联系人")]
        [StringLength(30)]
        public virtual string ALinkMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 甲方联系电话
        /// </summary>
        [DisplayName("甲方联系电话")]
        [StringLength(20)]
        public virtual string ALinkTel
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 乙方联系人
        /// </summary>
        [DisplayName("乙方联系人")]
        [StringLength(30)]
        public virtual string BLinkMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 乙方联系电话
        /// </summary>
        [DisplayName("乙方联系电话")]
        [StringLength(20)]
        public virtual string BLinkTel
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 合同限制类型
        /// </summary>
        [DisplayName("合同限制类型")]
        [StringLength(50)]
        public virtual string ContractLimitType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 合同开始时间
        /// </summary>
        [DisplayName("合同开始时间")]
        public virtual System.DateTime? ContractST
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 合同结束时间
        /// </summary>
        [DisplayName("合同结束时间")]
        public virtual System.DateTime? ContractET
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 结账日期
        /// </summary>
        [DisplayName("结账日期")]
        public virtual System.DateTime? FootDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 付款日期
        /// </summary>
        [DisplayName("付款日期")]
        public virtual System.DateTime? PaymentDate
        {
            get;
			set;			 
        }
        /// <summary>
        /// 结账日期
        /// </summary>
        [DisplayName("结账日期")]
        public virtual int? FootDate1
        {
            get;
            set;
        }
        /// <summary>
        /// 付款日期
        /// </summary>
        [DisplayName("付款日期")]
        public virtual int? PaymentDate1
        {
            get;
            set;
        }	
        /// <summary>
        /// 回款联系人
        /// </summary>
        [DisplayName("回款联系人")]
        [StringLength(30)]
        public virtual string PayBackMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 回款联系电话
        /// </summary>
        [DisplayName("回款联系电话")]
        [StringLength(20)]
        public virtual string PayBackTel
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 垫资方量
        /// </summary>
        [DisplayName("垫资方量")] 
        public virtual decimal? MatCube
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
        /// 预付方量
        /// </summary>
        [DisplayName("预付方量")]
        public virtual decimal? PrepayCube
        {
            get;
			set;			 
        }
        /// <summary>
        /// 已受限否
        /// </summary>
        [DisplayName("已受限否")]
        public virtual bool IsLimited
        { 
            get; 
            set; 
        }

        /// <summary>
        /// 累计生产金额
        /// </summary>
        [DisplayName("累计生产金额")]
        public virtual decimal? TotalComeMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 累计结算金额
        /// </summary>
        [DisplayName("累计结算金额")]
        public virtual decimal? TotalJSMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 累计付款金额
        /// </summary>
        [DisplayName("累计付款金额")]
        public virtual decimal? PayMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 欠款金额
        /// </summary>
        [DisplayName("欠款金额")]
        public virtual decimal? Balance
        {
            get;
            set;
        }
        /// <summary>
        /// 授信额度
        /// </summary>
        [DisplayName("授信额度")]
        [Range(0,double.MaxValue,ErrorMessage="必须是大于等于0的数字")]
        [Required]
        public virtual decimal? CreditMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 业务类型
        /// </summary>
        [DisplayName("业务类型")]
        public virtual string BusinessType
        {
            get;
            set;
        }
        /// <summary>
        /// 结算量提成系数
        /// </summary>
        [DisplayName("结算量系数")]
        public virtual decimal? JsCoefficient
        {
            get;
            set;
        }
        /// <summary>
        /// 签约量提成系数
        /// </summary>
        [DisplayName("签约量系数")]
        public virtual decimal? QyCoefficient
        {
            get;
            set;
        }
        /// <summary>
        /// 回款提成系数
        /// </summary>
        [DisplayName("回款提成系数")]
        public virtual decimal? HkCoefficient
        {
            get;
            set;
        }
        /// <summary>
        /// 提成单价
        /// </summary>
        [DisplayName("提成单价")]
        public virtual decimal? DeductPerPrice
        {
            get;
            set;
        }
        #endregion

    }
}
