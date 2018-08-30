using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 强度评定子表(统计方法一)
    /// </summary>
	public class M1AssessItem : _M1AssessItem
    {
        [DisplayName("强度评定编号")]
        public virtual string ConStrAssessID
        {
            get;
            set;
        }
	}
}