using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Enums
{
    /// <summary>
    /// 供应商类型
    /// </summary>
    public class SupplyType
    {
        /// <summary>
        /// 供货商
        /// </summary>
        public static string Supply = "Su1";
        /// <summary>
        /// 原料供货商
        /// </summary>
        public static string MaterialSupply = "Su2";
        /// <summary>
        /// 运输厂商
        /// </summary>
        public static string Transport = "Su3";
        /// <summary>
        /// 配件供货商
        /// </summary>
        public static string PartSupply = "Su4";

        /// <summary>
        /// 供货/运输商
        /// </summary>
        public static string SupplyTransport = "Su5";

    }
}
