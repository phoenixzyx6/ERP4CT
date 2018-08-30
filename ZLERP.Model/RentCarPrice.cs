using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 外租罐车单价设定
    /// </summary>
	public class RentCarPrice : _RentCarPrice
    {
        /// <summary>
        /// 价格1
        /// </summary>
        [DisplayName("元/方(<=3km)")]
        public override decimal? Price1
        {
            get;
            set;
        }
        /// <summary>
        /// 价格2
        /// </summary>
        [DisplayName("元/方(<=20km)")]
        public override decimal? Price2
        {
            get;
            set;
        }
        /// <summary>
        /// 价格3
        /// </summary>
        [DisplayName("元/方(<=35km)")]
        public override decimal? Price3
        {
            get;
            set;
        }
        /// <summary>
        /// 价格4
        /// </summary>
        [DisplayName("元/方(<=50km)")]
        public override decimal? Price4
        {
            get;
            set;
        }	
	}
}