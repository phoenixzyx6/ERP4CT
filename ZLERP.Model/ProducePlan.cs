using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  生产计划表
    /// </summary>
    public class ProducePlan : _ProducePlan
    {
        [Display(Name = "任务单")]
        public virtual string TaskID
        {
            get;
            set;
        }

        public virtual string ProjectName
        {
            get { return this.ProduceTask == null ? "" : this.ProduceTask.ProjectName; }
        }

        public virtual string ConStrength
        {
            get { return this.ProduceTask == null ? "" : this.ProduceTask.ConStrength; }
        }

        public virtual string ConsPos
        {
            get { return this.ProduceTask == null ? "" : this.ProduceTask.ConsPos; }
        }

        public virtual DateTime? PlanDay
        {
            get { return this.PlanDate.Value.Date; }
        }

        public virtual DateTime? OpenTime
        {
            get { return this.ProduceTask == null ? null : this.ProduceTask.OpenTime; }
        }

    }
}