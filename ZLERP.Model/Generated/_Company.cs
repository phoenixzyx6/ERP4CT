
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  公司表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Company : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(CompName);
			sb.Append(CompAddr);
			sb.Append(Country);
			sb.Append(Area);
			sb.Append(Province);
			sb.Append(Principal);
			sb.Append(LinkMan);
			sb.Append(Tel);
			sb.Append(Email);
			sb.Append(Fax);
			sb.Append(WebSite);
			sb.Append(PostCode);
			sb.Append(RegAsset);
			sb.Append(Employees);
			sb.Append(Remark);
			sb.Append(Latitude);
			sb.Append(Longtide);
			sb.Append(Range);
			sb.Append(Excursion);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 公司名称
        /// </summary>
        [DisplayName("公司名称")]
        [StringLength(128)]
        public virtual string CompName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 公司地址
        /// </summary>
        [DisplayName("公司地址")]
        [StringLength(128)]
        public virtual string CompAddr
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 国家
        /// </summary>
        [DisplayName("国家")]
        [StringLength(50)]
        public virtual string Country
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 区域
        /// </summary>
        [DisplayName("区域")]
        [StringLength(50)]
        public virtual string Area
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 省份
        /// </summary>
        [DisplayName("省份")]
        [StringLength(50)]
        public virtual string Province
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
        /// 电话
        /// </summary>
        [DisplayName("电话")]
        [StringLength(20)]
        public virtual string Tel
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        [StringLength(50)]
        public virtual string Email
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 传真
        /// </summary>
        [DisplayName("传真")]
        [StringLength(20)]
        public virtual string Fax
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 网址
        /// </summary>
        [DisplayName("网址")]
        [StringLength(128)]
        public virtual string WebSite
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 邮编
        /// </summary>
        [DisplayName("邮编")]
        [StringLength(20)]
        public virtual string PostCode
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 注册资金
        /// </summary>
        [DisplayName("注册资金")]
        public virtual decimal? RegAsset
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 员工数
        /// </summary>
        [DisplayName("员工数")]
        public virtual int? Employees
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
        /// 位置纬度
        /// </summary>
        [DisplayName("位置纬度")]
        public virtual double? Latitude
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 位置经度
        /// </summary>
        [DisplayName("位置经度")]
        public virtual double? Longtide
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 公司范围
        /// </summary>
        [DisplayName("公司范围")]
        public virtual decimal? Range
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 误差
        /// </summary>
        [DisplayName("误差")]
        public virtual decimal? Excursion
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual IList<Department> Departments
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<Car> Cars 
        {
            get;
            set;
        } 
		
        #endregion
    }
}
