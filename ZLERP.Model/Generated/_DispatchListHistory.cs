
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _DispatchListHistory : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(DispatchID);
			sb.Append(ShipDocID);
			sb.Append(TaskID);
			sb.Append(DispatchOrder);
			sb.Append(ProductLineID);
			sb.Append(BetonFormula);
			sb.Append(SlurryFormula);
			sb.Append(ProduceCube);
			sb.Append(BetonCount);
			sb.Append(SlurryCount);
			sb.Append(IsSlurry);
			sb.Append(PCRate);
			sb.Append(StartupTime);
			sb.Append(IsRunning);
			sb.Append(IsCompleted);
			sb.Append(RemainCount);
			sb.Append(IsAverage);
			sb.Append(OneCube);
			sb.Append(OnePCRate);
			sb.Append(TwoCube);
			sb.Append(TwoPCRate);
			sb.Append(BTotalPot);
			sb.Append(BNextPot);
			sb.Append(OneSlurryCube);
			sb.Append(OneSlurryPCRate);
			sb.Append(TwoSlurryCube);
			sb.Append(TwoSlurryPCRate);
			sb.Append(STotalPot);
			sb.Append(SNextPot);
			sb.Append(SynStatus);
			sb.Append(Remark);
			sb.Append(Act);
			sb.Append(ExecTime);
			sb.Append(ExecMan);
			sb.Append(Version);
			sb.Append(CarID);
			sb.Append(Driver);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [StringLength(30)]
        public virtual string DispatchID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [StringLength(30)]
        public virtual string ShipDocID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [StringLength(30)]
        public virtual string TaskID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual int? DispatchOrder
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [StringLength(20)]
        public virtual string ProductLineID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public virtual string BetonFormula
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public virtual string SlurryFormula
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? ProduceCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? BetonCount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? SlurryCount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual bool? IsSlurry
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? PCRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? StartupTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual bool? IsRunning
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual bool? IsCompleted
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual int? RemainCount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual bool? IsAverage
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? OneCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? OnePCRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? TwoCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? TwoPCRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual int? BTotalPot
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual int? BNextPot
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? OneSlurryCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? OneSlurryPCRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? TwoSlurryCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? TwoSlurryPCRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual int? STotalPot
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual int? SNextPot
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual int? SynStatus
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [StringLength(256)]
        public virtual string Remark
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [StringLength(20)]
        public virtual string Act
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? ExecTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [StringLength(30)]
        public virtual string ExecMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [StringLength(30)]
        public virtual string CarID
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
        #endregion
    }
}
