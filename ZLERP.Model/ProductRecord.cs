using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
namespace ZLERP.Model
{
    /// <summary>
    ///  生产记录罐数（转换后）
    /// </summary>
	public class ProductRecord : _ProductRecord
    {
        /// <summary>
        /// 生产线编号
        /// </summary>
        public virtual string ProductLineID { get; set; }
	}
}