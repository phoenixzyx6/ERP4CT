using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Enums
{
    /// <summary>
    /// 剩退料处理方式
    /// </summary>
    public class ActionType
    {
        /// <summary>
        /// 转发
        /// </summary>
        public static string Transfer = "AT1";
        /// <summary>
        /// 报废
        /// </summary>
        public static string Reject = "AT2";
    }
}
