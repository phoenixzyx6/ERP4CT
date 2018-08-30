using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 砼强度评定
    /// </summary>
	public class ConStrAssess : _ConStrAssess
    {
        [DisplayName("任务单号")]
        public virtual string TaskID
        {
            get;
            set;
        }

        [DisplayName("配比编号")]
        public virtual string CustMixpropID
        {
            get;
            set;
        }
	}
}