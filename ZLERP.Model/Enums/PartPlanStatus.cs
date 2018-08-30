using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Enums
{
    /// <summary>
    /// 车辆状态
    /// </summary>
    public class PartPlanStatus
    {
        /// <summary>
        /// 未处理
        /// </summary>
        public static string NotProcess = "未处理";
        /// <summary>
        /// 采购
        /// </summary>
        public static string Stock = "采购";
        /// <summary>
        /// 已处理
        /// </summary>
        public static string Completed = "已处理";
    }
}
