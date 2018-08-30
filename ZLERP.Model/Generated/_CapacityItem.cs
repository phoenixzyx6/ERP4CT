
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  生产记录明细（转换前）抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CapacityItem : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ProductRecID);
			sb.Append(ProductLineID);
			sb.Append(S1);
			sb.Append(S2);
			sb.Append(S3);
			sb.Append(S4);
			sb.Append(S5);
			sb.Append(S6);
			sb.Append(S7);
			sb.Append(S8);
			sb.Append(S9);
			sb.Append(S10);
			sb.Append(S11);
			sb.Append(S12);
			sb.Append(S13);
			sb.Append(S14);
			sb.Append(S15);
			sb.Append(S16);
			sb.Append(S17);
			sb.Append(S18);
			sb.Append(S19);
			sb.Append(S20);
			sb.Append(S21);
			sb.Append(S22);
			sb.Append(S23);
			sb.Append(S24);
			sb.Append(PCRate);
			sb.Append(PotTimes);
			sb.Append(ElectValue);
			sb.Append(IsManual);
			sb.Append(SynID);
			sb.Append(SynStatus);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 生产记录编号
        /// </summary>
        [DisplayName("生产记录编号")]
        [StringLength(30)]
        public virtual string ProductRecID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 搅拌机组编号
        /// </summary>
        [DisplayName("搅拌机组编号")]
        [StringLength(20)]
        public virtual string ProductLineID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料一
        /// </summary>
        [DisplayName("材料一")]
        public virtual decimal? S1
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料二
        /// </summary>
        [DisplayName("材料二")]
        public virtual decimal? S2
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料三
        /// </summary>
        [DisplayName("材料三")]
        public virtual decimal? S3
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料四
        /// </summary>
        [DisplayName("材料四")]
        public virtual decimal? S4
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料五
        /// </summary>
        [DisplayName("材料五")]
        public virtual decimal? S5
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料六
        /// </summary>
        [DisplayName("材料六")]
        public virtual decimal? S6
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料七
        /// </summary>
        [DisplayName("材料七")]
        public virtual decimal? S7
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料八
        /// </summary>
        [DisplayName("材料八")]
        public virtual decimal? S8
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料九
        /// </summary>
        [DisplayName("材料九")]
        public virtual decimal? S9
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十
        /// </summary>
        [DisplayName("材料十")]
        public virtual decimal? S10
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十一
        /// </summary>
        [DisplayName("材料十一")]
        public virtual decimal? S11
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十二
        /// </summary>
        [DisplayName("材料十二")]
        public virtual decimal? S12
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十三
        /// </summary>
        [DisplayName("材料十三")]
        public virtual decimal? S13
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十四
        /// </summary>
        [DisplayName("材料十四")]
        public virtual decimal? S14
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十五
        /// </summary>
        [DisplayName("材料十五")]
        public virtual decimal? S15
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十六
        /// </summary>
        [DisplayName("材料十六")]
        public virtual decimal? S16
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十七
        /// </summary>
        [DisplayName("材料十七")]
        public virtual decimal? S17
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十八
        /// </summary>
        [DisplayName("材料十八")]
        public virtual decimal? S18
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料十九
        /// </summary>
        [DisplayName("材料十九")]
        public virtual decimal? S19
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料二十
        /// </summary>
        [DisplayName("材料二十")]
        public virtual decimal? S20
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料二十一
        /// </summary>
        [DisplayName("材料二十一")]
        public virtual decimal? S21
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料二十二
        /// </summary>
        [DisplayName("材料二十二")]
        public virtual decimal? S22
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料二十三
        /// </summary>
        [DisplayName("材料二十三")]
        public virtual decimal? S23
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料二十四
        /// </summary>
        [DisplayName("材料二十四")]
        public virtual decimal? S24
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 罐容比
        /// </summary>
        [DisplayName("罐容比")]
        public virtual decimal? PCRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 罐次
        /// </summary>
        [DisplayName("罐次")]
        public virtual int? PotTimes
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 电流值
        /// </summary>
        [DisplayName("电流值")]
        public virtual decimal? ElectValue
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否手动
        /// </summary>
        [Required]
        [DisplayName("是否手动")]
        public virtual bool IsManual
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 同步编号
        /// </summary>
        [DisplayName("同步编号")]
        public virtual int? SynID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 同步状态
        /// </summary>
        [DisplayName("同步状态")]
        public virtual int? SynStatus
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual Capacity Capacity
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
