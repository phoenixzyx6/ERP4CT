using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
namespace ZLERP.Model
{
    /// <summary>
    /// 委托单明细
    /// </summary>
	public class CommissionItem : _CommissionItem
    {
        public virtual string CommissionID
        {
            get;
            set;
        }
	}
}