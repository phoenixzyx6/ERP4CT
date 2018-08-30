using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 工具借用明细
    /// </summary>
	public class PartBorrowItem : _PartBorrowItem
    {
        
         /// <summary>
        /// 工具借用ID
        /// </summary>
        public virtual string BorrowID
        {
            get;
            set;
        }


        public virtual string  PartName
        {
            get { return this.PartInfo == null?"":this.PartInfo.PartName; }
        }
         
    
	}
}