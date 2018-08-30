
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 即登录系统的用户帐号及其他信息 用户表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _User : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(TrueName);
			sb.Append(Password);
			sb.Append(Nation);
			sb.Append(Birthday);
			sb.Append(CommAddr);
			sb.Append(NationAddr);
			sb.Append(IDCard);
			sb.Append(PostCode);
			sb.Append(BloodType);
			sb.Append(Sex);
			sb.Append(IsMarried);
			sb.Append(MaxEducate);
			sb.Append(BankID);
			sb.Append(Team);　
			sb.Append(DigitSign);
			sb.Append(DriverIdentity);
			sb.Append(Email);
			sb.Append(Tel);
			sb.Append(Mobile);
			sb.Append(ManagerID);
			sb.Append(UserType);
			sb.Append(IsUsed);
			sb.Append(IsVisited);
			sb.Append(ValidFrom);
			sb.Append(ValidTo);
			sb.Append(Remark);
			sb.Append(Version);
			sb.Append(UserCode);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 真实姓名
        /// </summary>
        [DisplayName("真实姓名")]
        [StringLength(30)]
        public virtual string TrueName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 登录密码
        /// </summary>
        [DisplayName("登录密码")]
        [StringLength(50, MinimumLength=6)]
        public virtual string Password
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 籍贯
        /// </summary>
        [DisplayName("籍贯")]
        [StringLength(50)]
        public virtual string Nation
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 出生日期
        /// </summary>
        [DisplayName("出生日期")]
        public virtual System.DateTime? Birthday
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 通讯地址
        /// </summary>
        [DisplayName("通讯地址")]
        [StringLength(256)]
        public virtual string CommAddr
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 户籍地址
        /// </summary>
        [DisplayName("户籍地址")]
        [StringLength(256)]
        public virtual string NationAddr
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 身份证号
        /// </summary>
        [DisplayName("身份证号")]
        [StringLength(20)]
        public virtual string IDCard
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
        /// 血型
        /// </summary>
        [DisplayName("血型")]
        [StringLength(20)]
        public virtual string BloodType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        [StringLength(20)]
        public virtual string Sex
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 已婚否
        /// </summary>
        [DisplayName("已婚否")]
        public virtual bool IsMarried
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 最高学历
        /// </summary>
        [DisplayName("最高学历")]
        [StringLength(20)]
        public virtual string MaxEducate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 金融机构代码
        /// </summary>
        [DisplayName("金融机构代码")]
        [StringLength(30)]
        public virtual string BankID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 班次
        /// </summary>
        [DisplayName("班次")]
        [StringLength(50)]
        public virtual string Team
        {
            get;
			set;			 
        }	
        
        /// <summary>
        /// 数字签名
        /// </summary>
        [DisplayName("数字签名")]
        [StringLength(128)]
        public virtual string DigitSign
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 司机身份
        /// </summary>
        [DisplayName("司机身份")]
        [StringLength(50)]
        public virtual string DriverIdentity
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
        /// 工地电话
        /// </summary>
        [DisplayName("联系电话")]
        [StringLength(20)]
        public virtual string Tel
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 手机
        /// </summary>
        [DisplayName("手机")]
        [StringLength(20)]
        public virtual string Mobile
        {
            get;
			set;			 
        }	

        //UNDONE:xyl:废除该字段
        /// <summary>
        /// 所属公司
        /// </summary>
        ///[DisplayName("所属公司")]
        ///[StringLength(128)]
        ///public virtual string CompanyID
        ///{
            ///get;
			///set;			 
        ///}	
        /// <summary>
        /// 直接上级
        /// </summary>
        [DisplayName("直接上级")]
        [StringLength(30)]
        public virtual string ManagerID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 用户类型
        /// </summary>
        [DisplayName("用户类型")]
        [StringLength(50)]
        public virtual string UserType
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
        /// 允许外网访问
        /// </summary>
        [Required]
        [DisplayName("允许外网访问")]
        public virtual bool IsVisited
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 到职日期
        /// </summary>
        [DisplayName("到职日期")]
        public virtual System.DateTime? ValidFrom
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 离职日期
        /// </summary>
        [DisplayName("离职日期")]
        public virtual System.DateTime? ValidTo
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
        /// 用户编号
        /// </summary>
        [DisplayName("用户编号")]
        [StringLength(20)]
        public virtual string UserCode
        {
            get;
			set;			 
        }
        /// <summary>
        /// 班类型
        /// </summary>
        [DisplayName("班类型")]
        [StringLength(30)]
        public virtual string ClassType
        {
            get;
            set;
        }	
		public virtual Department Department
        {
            get;
			set;
        }
        [ScriptIgnore]
		public virtual IList<DriverForCar> DriverForCars
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual IList<SysFunc> SysFuncs
        {
            get;
			set;
        }
        [ScriptIgnore]
		public virtual IList<_EquipMtLyItem> EquipMtLyItems
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<EquipMtLyReturn> EquipMtLyReturns
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<EquipMtLyReturnItem> EquipMtLyReturnItems
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<EquipTermlyMtItem> EquipTermlyMtItems
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual IList<MntZl> MntZls
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<UserRole> UserRoles
        {
            get;
            set;
        }
        #endregion
    }
}
