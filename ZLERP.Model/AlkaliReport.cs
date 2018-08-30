using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 碱含量计算书
    /// </summary>
    public class AlkaliReport : _AlkaliReport
    {
        [DisplayName("配比流水号")]
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
        public virtual string TaskID
        {
            get;
            set;
        }

        public virtual string ProjectName
        {
            get { return this.ProduceTask == null ? "" : this.ProduceTask.ProjectName; }
        }

        public virtual string ConstructUnit
        {
            get { return this.ProduceTask == null ? "" : this.ProduceTask.ConstructUnit; }
        }

        public virtual DateTime NeedDate
        {
            get { return this.ProduceTask == null ? DateTime.MaxValue : this.ProduceTask.NeedDate; }
        }
    }
}