using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 开盘鉴定表
    /// </summary>
    public class OpenCheck : _OpenCheck
    {
        [Required, DisplayName("客户配比")]
        public virtual string CustMixpropID
        {
            get;
            set;
        }
        public virtual string MixpropCode
        {
            get
            {
                return this.CustMixprop == null ? "" : this.CustMixprop.MixpropCode;
            }
        }

        [Required, DisplayName("生产任务单")]
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


    }
}