using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 品质试块试验
    /// </summary>
	public class QualityExam : _QualityExam
    {
        [DisplayName("配比代号")]
        public virtual string CustMixpropID
        {
            get;
            set;
        }
        [DisplayName("任务单")]
        public virtual string TaskID
        {
            get;
            set;
        }
        [DisplayName("委托单")]
        public virtual string CommissionID
        {
            get;
            set;
        }
	}
}