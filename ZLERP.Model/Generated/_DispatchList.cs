
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  生产调度抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _DispatchList : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
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
			sb.Append(Field1);
			sb.Append(Field2);
			sb.Append(Field3);
			sb.Append(Field4);
			sb.Append(Field5);
            sb.Append(SendStatus);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 登记排序
        /// </summary>
        [DisplayName("登记排序")]
        public virtual int? DispatchOrder
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
        /// 混凝土配比
        /// </summary>
        [DisplayName("混凝土配比")]
        [StringLength(50)]
        public virtual string BetonFormula
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砂浆配比
        /// </summary>
        [DisplayName("砂浆配比")]
        [StringLength(50)]
        public virtual string SlurryFormula
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 调度方量
        /// </summary>
        [Required]
        [DisplayName("生产方量")]
        public virtual decimal ProduceCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 混凝土方量
        /// </summary>
        [Required]
        [DisplayName("混凝土方量")]
        public virtual decimal BetonCount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砂浆方量
        /// </summary>
        [Required]
        [DisplayName("砂浆方量")]
        public virtual decimal SlurryCount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否含砂浆
        /// </summary>
        [Required]
        [DisplayName("是否含砂浆")]
        public virtual bool IsSlurry
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
        /// 启动时间
        /// </summary>
        [DisplayName("启动时间")]
        public virtual System.DateTime? StartupTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否正生产
        /// </summary>
        [Required]
        [DisplayName("是否正生产")]
        public virtual bool IsRunning
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否完成
        /// </summary>
        [Required]
        [DisplayName("是否完成")]
        public virtual bool IsCompleted
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 剩余盘数
        /// </summary>
        [DisplayName("剩余盘数")]
        public virtual int? RemainCount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否平均分配
        /// </summary>
        [Required]
        [DisplayName("平均分配")]
        public virtual bool IsAverage
        {
            get;
			set;			 
        }	

        /// <summary>
        /// 方式一（方量）
        /// </summary>
        [Required]
        [DisplayName("方式一（方量）")]
        public virtual decimal OneCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 方式一（罐容比）
        /// </summary>
        [DisplayName("方式一（罐容比）")]
        public virtual decimal? OnePCRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 方式二（方量）
        /// </summary>
        [Required]
        [DisplayName("方式二（方量）")]
        public virtual decimal TwoCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 方式二（罐容比）
        /// </summary>
        [DisplayName("方式二（罐容比）")]
        public virtual decimal? TwoPCRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 总罐次（混凝土）
        /// </summary>
        [DisplayName("总罐次（混凝土）")]
        public virtual int? BTotalPot
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 后罐次（混凝土）
        /// </summary>
        [DisplayName("后罐次（混凝土）")]
        public virtual int? BNextPot
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 方式一（砂浆方量）
        /// </summary>
        [Required]
        [DisplayName("方式一（砂浆方量）")]
        public virtual decimal OneSlurryCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 方式一（砂浆罐容比）
        /// </summary>
        [DisplayName("方式一（砂浆罐容比）")]
        public virtual decimal? OneSlurryPCRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 方式二（砂浆方量）
        /// </summary>
        [Required]
        [DisplayName("方式二（砂浆方量）")]
        public virtual decimal TwoSlurryCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 方式二（砂浆罐容比）
        /// </summary>
        [DisplayName("方式二（砂浆罐容比）")]
        public virtual decimal? TwoSlurryPCRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 总罐次（砂浆）
        /// </summary>
        [DisplayName("总罐次（砂浆）")]
        public virtual int? STotalPot
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 后罐次（砂浆）
        /// </summary>
        [DisplayName("后罐次（砂浆）")]
        public virtual int? SNextPot
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
        /// 扩展字段1
        /// </summary>
        [DisplayName("扩展字段1")]
        public virtual bool Field1
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 扩展字段2
        /// </summary>
        [DisplayName("扩展字段2")]
        [StringLength(128)]
        public virtual string Field2
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否拖泵
        /// </summary>
        [DisplayName("是否拖泵")]
        public virtual bool Field3
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 扩展字段4(剩余盘数)
        /// </summary>
        [DisplayName("扩展字段4")]
        [StringLength(128)]
        public virtual string Field4
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 扩展字段5
        /// </summary>
        [DisplayName("扩展字段5")]
        [StringLength(128)]
        public virtual string Field5
        {
            get;
			set;			 
        }
        /// <summary>
        /// 同步状态
        /// </summary>
        [DisplayName("发送状态")]
        public virtual int? SendStatus
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual ProduceTask ProduceTask
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual ShippingDocument ShippingDocument
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
