using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Enums
{
    /// <summary>
    /// 车辆状态
    /// </summary>
    public class CarStatus
    {
        /// <summary>
        /// 可调度状态
        /// </summary>
        public const string AllowShipCar = "0";
        /// <summary>
        /// 出车状态
        /// </summary>
        public const string ShippingCar = "1";
        /// <summary>
        /// 休息状态
        /// </summary>
        public const string RestCar = "2";
        /// <summary>
        /// 维修状态
        /// </summary>
        public const string RepairCar = "3";

        /// <summary>
        /// 出租状态
        /// </summary>
        public const string RentCar = "4";
    }
}
