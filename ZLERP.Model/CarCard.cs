using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 车辆卡片
    /// </summary>
	public class CarCard : _CarCard
    {
        [Required]
        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }
        /// <summary>
        /// 车号
        /// </summary>
        [Required,DisplayName("车号"), StringLength(50)]
        public virtual string CarID
        {
            set;
            get;
        }
        [Required]
        public override string CardType
        {
            get
            {
                return base.CardType;
            }
            set
            {
                base.CardType = value;
            }
        }
        /// <summary>
        /// 供应商
        /// </summary>
        [DisplayName("供应商")]
        [StringLength(50)]
        public virtual string SupplyID
        {
            set;
            get;
        }

        /// <summary>
        /// 供应商
        /// </summary>
        [DisplayName("供应商名称")]
        [StringLength(50)]
        public virtual string SupplyName
        {
            get { return this.SupplyInfo == null ? "" : this.SupplyInfo.SupplyName; }
        }

        public virtual string CarNo
        {
            get { return this.Car == null ? "" : this.Car.CarNo; }
        }
	}
}