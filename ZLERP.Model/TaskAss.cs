using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 出货辅助作业
    /// </summary>
	public class TaskAss : _TaskAss
    {
        /// <summary>
        /// 任务单号
        /// </summary>
        [DisplayName("任务单号")]
        [Required]
        public virtual string TaskID
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

        public virtual string ProjectName
        {
            get { return this.ProduceTask == null ? "" : this.ProduceTask.ProjectName; }
        }
	}
}