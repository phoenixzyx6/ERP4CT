using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Enums
{
    /// <summary>
    /// 车辆状态
    /// </summary>
    public class GpsCarStatus
    {
        /// <summary>
        /// 未知
        /// </summary>
        public const string UnKnown = "005001";
        /// <summary>
        /// 休息
        /// </summary>
        public const string Rest = "005002";
        /// <summary>
        /// 养护
        /// </summary>
        public const string Repair = "005003";
        /// <summary>
        /// 待命
        /// </summary>
        public const string Waitting = "005004";

        /// <summary>
        /// 出厂
        /// </summary>
        public const string Go = "005005";

        /// <summary>
        /// 到达工地
        /// </summary>
        public const string Arrived = "005006";

        /// <summary>
        /// 回场
        /// </summary>
        public const string Back = "005007";

        /// <summary>
        /// 进站进料
        /// </summary>
        public const string Burden = "005008";
    }
}
