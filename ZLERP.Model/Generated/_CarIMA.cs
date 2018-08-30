using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CarIMA : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);;
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        /// <summary>
        /// 车辆号
        /// </summary>
        [DisplayName("车辆号")]
        public virtual string CarID
        {
            get;
            set;
        }
        /// <summary>
        /// 车辆牌照
        /// </summary>
        [DisplayName("车辆牌照")]
        public virtual string CarNo
        {
            get;
            set;
        }
        /// <summary>
        /// 保险日期限
        /// </summary>
        [DisplayName("保险日期限")]
        public virtual DateTime? InsuranceDate
        {
            get;
            set;
        }
        /// <summary>
        /// 商业保险限
        /// </summary>
        [DisplayName("商险日期限")]
        public virtual DateTime? BusInsuranceDate
        {
            get;
            set;
        }
        /// <summary>
        /// 保险是否办理
        /// </summary>
        [DisplayName("保险是否办理")]
        public virtual bool InsuranceIsHandle
        {
            get;
            set;
        }
        /// <summary>
        /// 车辆年审日期
        /// </summary>
        [DisplayName("车辆年审日期")]
        public virtual DateTime? CarAnnualVerDate
        {
            get;
            set;
        }
        /// <summary>
        /// 车辆年审否
        /// </summary>
        [DisplayName("车辆年审否")]
        public virtual bool CarAnnualVerIsHandle
        {
            get;
            set;
        }
        /// <summary>
        /// 运营证年审日
        /// </summary>
        [DisplayName("运营证年审日")]
        public virtual DateTime? CerAnnualVerDate
        {
            get;
            set;
        }
        /// <summary>
        /// 运营证年审否
        /// </summary>
        [DisplayName("运营证年审否")]
        public virtual bool CerAnnualVerIsHandle
        {
            get;
            set;
        }
        /// <summary>
        /// 第一次维护否
        /// </summary>
        [DisplayName("第一次维护否")]
        public virtual bool FMaintainIsHandle
        {
            get;
            set;
        }
        /// <summary>
        /// 第一次下次时间
        /// </summary>
        [DisplayName("一次下次时间")]
        public virtual DateTime? FMaintainNextTime
        {
            get;
            set;
        }
        /// <summary>
        /// 第二次维护否
        /// </summary>
        [DisplayName("第二次维护否")]
        public virtual bool SMaintainIsHandle
        {
            get;
            set;
        }
        /// <summary>
        /// 第二次下次时间
        /// </summary>
        [DisplayName("二次下次时间")]
        public virtual DateTime? SMaintainNextTime
        {
            get;
            set;
        }
        /// <summary>
        /// 路桥
        /// </summary>
        [DisplayName("路桥")]
        public virtual string RoadBridge
        {
            get;
            set;
        }  
    }
}
