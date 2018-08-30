using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 工具归还明细
    /// </summary>
	public class PartBorrowReturn : _PartBorrowReturn
    {

        public virtual string PartName
        {
            get { return this.PartInfo == null ? "" : this.PartInfo.PartName; }
        }
	}
}