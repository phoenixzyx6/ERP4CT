using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 品质试块明细
    /// </summary>
	public class QualityExamItem : _QualityExamItem
    {
        [DisplayName("品质试块")]
        public virtual string QualityExamID
        {
            get;
            set;
        }
	}
}