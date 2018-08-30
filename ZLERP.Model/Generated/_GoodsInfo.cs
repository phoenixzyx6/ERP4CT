
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 物资信息表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _GoodsInfo : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(GoodsName);
			sb.Append(TotalNum);
			sb.Append(ContentNum);
            sb.Append(InNum);
			sb.Append(OutNum);
            sb.Append(BorrowNum);
            sb.Append(RevertNum);
			sb.Append(InvalidateNum);
			sb.Append(MinWarmContent);
			sb.Append(MaxWarmContent);
			sb.Append(Remark);
            sb.Append(ClassBID);
            sb.Append(ClassMID);
            sb.Append(ClassSID);

			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 设备大类
        /// </summary>
        [DisplayName("设备大类")]
        [StringLength(30)]
        //[Required]
        public virtual string ClassBID
        {
            get;
            set;
        }
        public virtual string ClassBName
        {
            get { return ClassB == null ? string.Empty : ClassB.ClassBName; }
        }
        /// <summary>
        /// 设备中类
        /// </summary>
        [DisplayName("设备中类")]
        [StringLength(30)]
        //[Required]
        public virtual string ClassMID
        {
            get;
            set;
        }
        public virtual string ClassMName
        {
            get { return ClassM == null ? string.Empty : ClassM.ClassMName; }
        }
        /// <summary>
        /// 设备细类
        /// </summary>
        [DisplayName("设备细类")]
        [StringLength(30)]
        //[Required]
        public virtual string ClassSID
        {
            get;
            set;
        }
        public virtual string ClassSName
        {
            get { return Classs == null ? string.Empty : Classs.ClassSName; }
        }



        [ScriptIgnore]
        public virtual ClassB ClassB
        {
            get;
            set;
        }


        [ScriptIgnore]
        public virtual ClassM ClassM
        {
            get;
            set;
        }


        [ScriptIgnore]
        public virtual Classs Classs
        {
            get;
            set;
        }

        //阀值
        //1超过 0正常
        [DisplayName("是否超过阀值")]
        public virtual int IsM
        {
            get;
            set;
        }



        /// <summary>
        /// 物资名称
        /// </summary>
        [Required]
        [DisplayName("物资名称")]
        [StringLength(50)]
        public virtual string GoodsName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 总量
        /// </summary>
        [DisplayName("总量")]
        public virtual decimal? TotalNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 库存数量
        /// </summary>
        [DisplayName("库存数量")]
        public virtual decimal? ContentNum
        {
            get;
			set;			 
        }
        /// <summary>
        /// 入库数量
        /// </summary>
        [DisplayName("入库数量")]
        public virtual decimal? InNum
        {
            get;
            set;
        }
        /// <summary>
        /// 领用数量
        /// </summary>
        [DisplayName("领用数量")]
        public virtual decimal? OutNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 借用数量
        /// </summary>
        [DisplayName("借用数量")]
        public virtual decimal? BorrowNum
        {
            get;
			set;			 
        }
        /// <summary>
        /// 归还数量
        /// </summary>
        [DisplayName("归还数量")]
        public virtual decimal? RevertNum
        {
            get;
            set;
        }
        /// <summary>
        /// 报废数量
        /// </summary>
        [DisplayName("报废数量")]
        public virtual decimal? InvalidateNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 最小报警库存数量
        /// </summary>
        [DisplayName("最小报警库存")]
        public virtual decimal? MinWarmContent
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 最大报警库存数量
        /// </summary>
        [DisplayName("最大报警库存")]
        public virtual decimal? MaxWarmContent
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(128)]
        public virtual string Remark
        {
            get;
			set;			 
        }

        /// <summary>
        /// 单价
        /// </summary>
        [Required]
        [DisplayName("单价")]
        [Range(0, float.MaxValue, ErrorMessage = "单价必须大于等于0")]
        public virtual decimal uPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 单位
        /// </summary>
        [DisplayName("单位")]
        public virtual string unit
        {
            get;
            set;
        }
        /// <summary>
        /// 型号
        /// </summary>
        [DisplayName("型号")]
        public virtual string GoodsModel
        {
            get;
            set;
        }
        /// <summary>
        /// 状态 1 启用 0 停用
        /// </summary>
        [DisplayName("状态")]
        public virtual bool IsUsed
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual GoodsType GoodsType
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<GoodsBorrow> GoodsBorrows
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<GoodsCheck> GoodsChecks
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<GoodsIn> GoodsIns
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<GoodsInvalidate> GoodsInvalidates
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<GoodsOut> GoodsOuts
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<GoodsRevert> GoodsReverts
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<GoodsAccount> GoodsAccounts
        {
            get;
            set;
        }
        

        #endregion
    }
}
