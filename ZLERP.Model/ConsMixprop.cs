using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    ///  施工配比
    /// </summary>
	public class ConsMixprop : _ConsMixprop
    {
        /// <summary>
        /// 任务单号
        /// </summary>
        [Required]
        [DisplayName("任务单号")]
        [StringLength(30)]
        public virtual string TaskID
        {
            get;
            set;
        }

        public virtual string CustName
        {
            get { return this.ProduceTask == null ? "" : this.ProduceTask.Contract.CustName; }
        }

        public virtual string ProjectName
        {
            get { return this.ProduceTask == null ? "" : this.ProduceTask.ProjectName; }
        }

        public virtual string ConsPos
        {
            get { return this.ProduceTask == null ? "" : this.ProduceTask.ConsPos; }
        }

        /// <summary>
        /// 理论配比号
        /// </summary>
        [DisplayName("理论配比号")]
        [StringLength(30)]
        public virtual string FormulaID
        {
            get;
            set;
        }

        /// <summary>
        /// 理论配比名称
        /// </summary>
        [DisplayName("理论配比名称")]
        public virtual string FormulaName
        {
            get
            {
                return this.Formula == null ? "" : this.Formula.FormulaName;
            }
        }

        /// <summary>
        /// 理论配比容重
        /// </summary>
        [DisplayName("理论配比容重")]
        public virtual decimal? TuneWeight
        {
            get
            {
                return this.Formula == null ? 0 : this.Formula.TuneWeight;
            }
        }

        /// <summary>
        /// 生产线名称
        /// </summary>
        [DisplayName("生产线名称")]
        public virtual string ProductLineName
        {
            get
            {
                return this.ProductLine == null ? "" : this.ProductLine.ProductLineName;
            }
        }

        /// <summary>
        /// 客户配比号
        /// </summary>
        [DisplayName("客户配比号")]
        [StringLength(30)]
        public virtual string CustMixpropID
        {
            get;
            set;
        }

        /// <summary>
        /// 同步是否完成
        /// </summary>
        [DisplayName("同步是否完成")]
        public virtual bool CompletedSyn
        {
            get;
            set;
        }
	}
}