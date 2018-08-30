using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 
    /// </summary>
	public class TrainRecord : _TrainRecord
    {
        [DisplayName("培训项目编号")]
        [Required]
        public virtual string TrainingID
        {
            get;
            set;
        }

        /// <summary>
        /// 培训项目名称
        /// </summary>
        [DisplayName("培训项目名称")]
        public virtual string TrainName
        {
            get {
                return Training == null ? "" : Training.TrainName;
            }
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        [DisplayName("部门名称")]
        public virtual string DepartmentName
        {
            get { return User == null ? "" : User.Department.DepartmentName; }
        }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [DisplayName("真实姓名")]
        public virtual string TrueName
        {
            get { return User == null ? "" : User.TrueName; }
        }

        /// <summary>
        /// 职务
        /// </summary>
        [DisplayName("职务")]
        public virtual string UserType
        {
            get { return User == null ? "" : User.UserType; }
        }

	}
}