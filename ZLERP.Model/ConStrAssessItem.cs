using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 强度等级评定子表
    /// </summary>
	public class ConStrAssessItem : _ConStrAssessItem
    {
        [DisplayName("主表编号")]
        public virtual string ConStrAssessID
        {
            get;
            set;
        }
	}
}