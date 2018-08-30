using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 委托单
    /// </summary>
	public class Commission : _Commission
    {
        [DisplayName("配比代号")]
        public virtual string FormulaID
        {
            get;
            set;
        }

        public virtual string FormulaName
        {
            get { return this.Formula == null ? "" : this.Formula.FormulaName; }
        }

        [DisplayName("配比代号")]
        public virtual string CustMixpropID
        {
            get;
            set;
        }

        public virtual string MixpropCode
        {
            get { return this.CustMixprop == null ? "" : this.CustMixprop.MixpropCode; }
        }

        [DisplayName("任务单号")]
        [Required]
        public virtual string TaskID
        {
            get;
            set;
        }

        [DisplayName("工程名称")]
        public virtual string ProjectName
        {
            get { return this.ProduceTask == null ? "" : this.ProduceTask.ProjectName; }
        }

        [DisplayName("建设单位")]
        public virtual string ConstructUnit
        {
            get { return this.ProduceTask == null ? "" : this.ProduceTask.ConstructUnit; }
        }

	}
}