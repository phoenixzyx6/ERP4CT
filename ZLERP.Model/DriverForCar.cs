using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    ///  车辆司机关系
    /// </summary>
	public class DriverForCar : _DriverForCar
    {
        [Required, Display(Name = "司机")]
        public virtual string UserID
        {
            get
            {
                if (this.User == null) { return string.Empty; }
                else { return this.User.ID; }
            }
            set
            {
                if (this.User == null)
                    this.User = new User();
                this.User.ID = value;
            }
        }

        [Required, Display(Name = "车辆")]
        public virtual string CarID
        {
            get
            {
                if (this.Car == null) { return string.Empty; }
                else { return this.Car.ID; }
            }
            set{
                if(this.Car == null)
                    this.Car = new Car();
                this.Car.ID = value;
            }
        }

        /// <summary>
        /// 计划班组
        /// </summary>
        [DisplayName("班次")]
        [StringLength(20)]
        public override string PlanClass
        {
            get
            {
                return base.PlanClass;
            }
            set
            {
                base.PlanClass = value;
            }
        }
	}
}