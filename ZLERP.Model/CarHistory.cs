using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    ///  车辆信息
    /// </summary>
    public class CarHistory : _CarHistory
    {
        [Required]
        [Display(Name = "历史ID")]
        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }
        [Required]
        [Display(Name = "车辆ID")]
        public override string CarID
        {
            get
            {
                return base.CarID;
            }
            set
            {
                base.CarID = value;
            }
        }
        [Required]
        public override string CarNo
        {
            get
            {
                return base.CarNo;
            }
            set
            {
                base.CarNo = value;
            }
        }
        [Required]
        public override string CarTypeID
        {
            get
            {
                return base.CarTypeID;
            }
            set
            {
                base.CarTypeID = value;
            }
        }

        /// <summary>
        /// 车种
        /// </summary>
        [DisplayName("车种")]
        public virtual string CarClassID
        {
            get;
            set;
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public virtual string CompanyName
        {
            get { return this.Company == null ? "" : this.Company.CompName; }
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        [DisplayName("公司编号")]
        [Required]
        public virtual int CompanyID
        {
            get;
            set;
        }
        public virtual string CarClassName
        {
            get { return this.CarClass == null ? "" : this.CarClass.CarClassName; }
        }
         
	}
}