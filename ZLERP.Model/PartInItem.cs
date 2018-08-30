using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 配件入库明细
    /// </summary>
	public class PartInItem : _PartInItem
    {
        public virtual string PartInID
        {
            set;
            get;
        }
        /// <summary>
        /// 配件名称
        /// </summary>
        [DisplayName("配件名称")]
        public virtual string PartInfoID
        {
            set;
            get;
        }
        public virtual string PartName
        {
            get
            {
                if (this.PartInfo != null)
                {
                    return this.PartInfo.PartName;
                }
                else
                    return string.Empty;
                ;
            }
        }
	}
}