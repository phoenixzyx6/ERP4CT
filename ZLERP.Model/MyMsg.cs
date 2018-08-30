using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 
    /// </summary>
	public class MyMsg : _MyMsg
    {
        /// <summary>
        /// 主体消息编码
        /// </summary>
        [DisplayName("主体消息编码")]
        
        public virtual string SystemMsgID
        {
            get;
            set;
        }
	}
}