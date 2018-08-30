using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _StuffInHistory : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(StuffInID);
            sb.Append(StuffID);
            sb.Append(SiloID);
            sb.Append(StockPactID);
            sb.Append(SupplyID);
            sb.Append(TransportID);
            sb.Append(CustName);
            sb.Append(GageUnit);
            sb.Append(TransportNum);
            sb.Append(SupplyNum);
            sb.Append(TotalNum);
            sb.Append(CarWeight);
            sb.Append(StockNum);
            sb.Append(WRate);
            sb.Append(InNum);
            sb.Append(Proportion);
            sb.Append(FootNum);
            sb.Append(Driver);
            sb.Append(SourceAddr);
            sb.Append(InDate);
            sb.Append(OutDate);
            sb.Append(AH);
            sb.Append(IsBack);
            sb.Append(Remark);
            sb.Append(CarNo);
            sb.Append(Operator);
            sb.Append(FootStatus);
            sb.Append(UnitPrice);
            sb.Append(TransUnitPrice);
            sb.Append(TotalPrice);
            sb.Append(TotalTransPrice);
            sb.Append(Version);
            sb.Append(ParentID);
            sb.Append(OrderNum);
            sb.Append(pic1);
            sb.Append(pic2);
            sb.Append(pic3);
            sb.Append(pic4);
            sb.Append(DarkWeight);
            sb.Append(Spec);
            sb.Append(FastMetage);
            sb.Append(ActionTime);
            sb.Append(Acter);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [StringLength(20)]
        public virtual string StuffInID
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(30)]
        public virtual string StuffID
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(30)]
        public virtual string SiloID
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(30)]
        public virtual string StockPactID
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(30)]
        public virtual string SupplyID
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(30)]
        public virtual string TransportID
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public virtual string CustName
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(20)]
        public virtual string GageUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? TransportNum
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? SupplyNum
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? TotalNum
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? CarWeight
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? StockNum
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? WRate
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? InNum
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(20)]
        public virtual string Proportion
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? FootNum
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(30)]
        public virtual string Driver
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public virtual string SourceAddr
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? InDate
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? OutDate
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public virtual string AH
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual bool? IsBack
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public virtual string Remark
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public virtual string CarNo
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(30)]
        public virtual string Operator
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual int? FootStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? UnitPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? TransUnitPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? TotalPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? TotalTransPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(20)]
        public virtual string ParentID
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual int? OrderNum
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public virtual string pic1
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public virtual string pic2
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public virtual string pic3
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public virtual string pic4
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual int? DarkWeight
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public virtual string Spec
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual bool? FastMetage
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? ActionTime
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public virtual string Acter
        {
            get;
            set;
        }
        #endregion
    }
}
