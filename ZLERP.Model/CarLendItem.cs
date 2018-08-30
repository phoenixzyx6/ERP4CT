using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 出租车辆明细表
    /// </summary>
	public class CarLendItem : _CarLendItem
    {
        /// <summary>
        /// 主表编号
        /// </summary>
        [DisplayName("车辆出租编号")]
        public virtual string CarLendID
        {
            get;
            set;
        }

        /// <summary>
        /// 车辆编号
        /// </summary>
        [DisplayName("车辆编号")]
        public virtual string CarID
        {
            get;
            set;
        }

        public override Car Car
        {
            get;
            set;
        }

        [DisplayName("车牌号码")]
        public virtual string CarNo
        {
            get { return this.Car == null ? "" : this.Car.CarNo; }
        }
        
        public virtual string CarTypeID
        {
            get { return this.Car == null ? "" : this.Car.CarTypeID; }
        }

        public virtual decimal? LendPrice
        {
            get { return this.CarLend == null ? 0 : this.CarLend.LendPrice; }
        }

        public virtual string PriceType
        {
            get { return this.CarLend == null ? "" : this.CarLend.PriceType; }
        }
	}
}