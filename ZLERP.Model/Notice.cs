using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
namespace ZLERP.Model
{
    /// <summary>
    /// 公告
    /// </summary>
	public class Notice : _Notice
    {
        public virtual IList<Attachment> Attachments { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual User CreateUser { get; set; }
	}
}