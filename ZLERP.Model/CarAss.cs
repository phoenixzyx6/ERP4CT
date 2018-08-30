using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 司机辅助作业
    /// </summary>
	public class CarAss : _CarAss
    {
        /// <summary>
        /// 车辆代号
        /// </summary>
        [DisplayName("车辆代号")]
        [Required]
        public virtual string CarID
        {
            get;
            set;
        }

        /// <summary>
        /// 司机编号
        /// </summary>
        [DisplayName("司机编号")]
        [Required]
        public virtual string UserID
        {
            get;
            set;
        }

        public virtual string TrueName
        {
            get { return this.User == null ? "" : this.User.TrueName; }
        }

        public virtual string CarNo
        {
            get { return this.Car == null ? "" : this.Car.CarNo; }
        }

        /// <summary>
        /// 任务单号
        /// </summary>
        [DisplayName("任务单号")]        
        public virtual string TaskID
        {
            get;
            set;
        }

        public virtual string ProjectName
        {
            get { return this.ProduceTask == null ? "" : this.ProduceTask.ProjectName; }
        }

        [DisplayName("是否改变车辆状态")]
        public virtual bool IsChangeCar
        {
            get;
            set;
        }

        /// <summary>
        /// 辅助单号
        /// </summary>
        [DisplayName("辅助单号")]
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
	}
}