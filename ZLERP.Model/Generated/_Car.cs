
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  车辆信息抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Car : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(CarNo);
			sb.Append(CarTypeID);
			sb.Append(PumpTypeID);
			sb.Append(BelongTo);
			sb.Append(Owner);
			sb.Append(RentCarName);
			sb.Append(LastBackTime);
			sb.Append(CarStatus);
            sb.Append(MaxCube);
			sb.Append(CarWeight);
			sb.Append(StuffWeight);
			sb.Append(Factory);
			sb.Append(BuyDate);
			sb.Append(LeaveFacDate);
			sb.Append(FacInnerID);
			sb.Append(EngineID);
			sb.Append(ChassisID);
			sb.Append(OilConsume);
			sb.Append(Remark);
			sb.Append(OrderNum);
			sb.Append(IsUsed);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 车牌号
        /// </summary>
        [DisplayName("车牌号码")]
        [StringLength(50)]
        public virtual string CarNo
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 车辆类型编号
        /// </summary>
        [DisplayName("车辆类型")]
        [StringLength(50)]
        public virtual string CarTypeID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 泵车类型编号
        /// </summary>
        [DisplayName("泵车类型")]
        [StringLength(50)]
        public virtual string PumpTypeID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 车别
        /// </summary>
        [DisplayName("车别")]
        [StringLength(50)]
        public virtual string BelongTo
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 车主
        /// </summary>
        [DisplayName("车主")]
        [StringLength(30)]
        public virtual string Owner
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 外租车队名
        /// </summary>
        [DisplayName("外租车队名")]
        [StringLength(128)]
        public virtual string RentCarName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 最近一次回厂时间
        /// </summary>
        [DisplayName("最近一次回厂时间")]
        public virtual System.DateTime? LastBackTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 车辆状态
        /// </summary>
        [DisplayName("车辆状态")]
        [StringLength(50)]
        public virtual string CarStatus
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砼容重
        /// </summary>
        [DisplayName("砼容量")]
        public virtual decimal? MaxCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 空重
        /// </summary>
        [DisplayName("空重(KG)")]
        public virtual decimal? CarWeight
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 原料容量
        /// </summary>
        [DisplayName("原料容量")]
        public virtual decimal? StuffWeight
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 厂商
        /// </summary>
        [DisplayName("厂商")]
        [StringLength(256)]
        public virtual string Factory
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 购买日期
        /// </summary>
        [DisplayName("购买日期")]
        public virtual System.DateTime? BuyDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 出厂日期
        /// </summary>
        [DisplayName("出厂日期")]
        public virtual System.DateTime? LeaveFacDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// GPS设备号
        /// </summary>
        [DisplayName("GPS设备号")]
        public virtual string TerminalID
        {
            get;
            set;
        }
        /// <summary>
        /// 厂内编号
        /// </summary>
        [DisplayName("厂内编号")]
        [StringLength(30)]
        public virtual string FacInnerID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 发动机号
        /// </summary>
        [DisplayName("发动机号")]
        [StringLength(30)]
        public virtual string EngineID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 底盘号
        /// </summary>
        [DisplayName("底盘号")]
        [StringLength(30)]
        public virtual string ChassisID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 耗油量
        /// </summary>
        [DisplayName("耗油量")]
        [StringLength(20)]
        public virtual string OilConsume
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(256)]
        public virtual string Remark
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public virtual int? OrderNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        [DisplayName("是否启用")]
        public virtual bool IsUsed
        {
            get;
			set;			 
        }

        [DisplayName("SIM卡号")]
        public virtual string SIM
        {
            get;
            set;
        }

        [DisplayName("GPS状态")]
        public virtual string GPSStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 车架号
        /// </summary>
        [DisplayName("车架号")]
        [StringLength(30)]
        public virtual string CarVINo
        {
            get;
            set;
        }
        /// <summary>
        /// 注册日期
        /// </summary>
        [DisplayName("注册日期")]
        public virtual System.DateTime? RegDate
        {
            get;
            set;
        }
        /// <summary>
        /// 发证日期
        /// </summary>
        [DisplayName("发证日期")]
        public virtual System.DateTime? IssuingDate
        {
            get;
            set;
        }
        /// <summary>
        /// 保险
        /// </summary>
        [DisplayName("保险")]
        [StringLength(30)]
        public virtual string Insurance
        {
            get;
            set;
        }
        /// <summary>
        /// 运营证
        /// </summary>
        [DisplayName("运营证")]
        [StringLength(50)]
        public virtual string OperCertificate
        {
            get;
            set;
        }

		public virtual IList<DriverForCar> DriverForCars
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual CarClass CarClass
        {
            get;
			set;
        }
        [ScriptIgnore]
        public virtual Company Company
        {
            get;
            set;
        }
        #endregion
    }
}
