
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using System.Web;

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  原料入库记录抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _StuffIn : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
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

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 客户
        /// </summary>
        [DisplayName("客户")]
        [StringLength(128)]
        public virtual string CustName
        {
            get;
			set;			 
        }
        /// <summary>
        /// 计量单位
        /// </summary>
        [DisplayName("计量单位")]
        [StringLength(20)]
        public virtual string GageUnit
        {
            get;
			set;			 
        }
        /// <summary>
        /// 运送数量
        /// </summary>
        [DisplayName("运送数量")]
        public virtual decimal? TransportNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 厂商数量
        /// </summary>
        [Required]
        [DisplayName("厂商数量")]
        public virtual decimal SupplyNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 总重
        /// </summary>
        [DisplayName("总重")]
        [Range(0, 999999,ErrorMessage = "入库数量必须是0 到 999 吨之间")]
        public virtual decimal? TotalNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 空车重
        /// </summary>
        [DisplayName("空车重")]
        [Range(0, 999999,ErrorMessage = "入库数量必须是0 到 999 吨之间")]
        public virtual decimal? CarWeight
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 进货数量
        /// </summary>
        [DisplayName("进货数量")]
        public virtual decimal StockNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 含水率
        /// </summary>
        [Required]
        [DisplayName("明扣重量")]        
        public virtual decimal WRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 暗扣
        /// </summary>
        [DisplayName("暗扣")]
        public virtual decimal DarkWeight
        {
            get;
            set;
        }	
        /// <summary>
        /// 入库数量
        /// </summary>
        [DisplayName("入库数量")]
        [Required]
        [Range(0, 999999, ErrorMessage = "入库数量必须是0 到 999 吨之间")]
        public virtual decimal InNum
        {
            get;
            set;
        }	
        /// <summary>
        /// 比重
        /// </summary>
        [DisplayName("换算系数")]
        [Required]
        public virtual decimal Proportion
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 结算数量
        /// </summary>
        [DisplayName("结算数量")]
        public virtual decimal? FootNum
        {
            get;
			set;			 
        }
        /// <summary>
        /// 最终结算数量
        /// </summary>
        [DisplayName("最终结算数量")]
        public virtual decimal? FinalFootNum
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(30)]
        [DisplayName("司机")]
        public virtual string Driver
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 原料来源地
        /// </summary>
        [DisplayName("原料来源地")]
        [StringLength(128)]
        public virtual string SourceAddr
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 进厂时间
        /// </summary>
        [Required,DisplayName("进厂时间")]
        public virtual System.DateTime? InDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 出厂时间
        /// </summary>
        [DisplayName("出厂时间")]
        public virtual System.DateTime? OutDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// A/H
        /// </summary>
        [DisplayName("A/H")]
        [StringLength(50)]
        public virtual string AH
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 标记为退货
        /// </summary>
        [DisplayName("标记为退货")]
        public virtual bool IsBack
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
        /// 运输车号
        /// </summary>
        [DisplayName("运输车号")]
        [StringLength(50)]
        public virtual string CarNo
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 经办人
        /// </summary>
        [DisplayName("经办人")]
        [StringLength(30)]
        public virtual string Operator
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 结算状态
        /// </summary>
        [DisplayName("结算状态")]
        public virtual int? FootStatus
        {
            get;
			set;			 
        }
        /// <summary>
        /// 原料单价
        /// </summary>
        //[Required]
        [DisplayName("原料单价")]
        //[Range(0.000001,float.MaxValue,ErrorMessage="单价必须是大于0的数")]
        public virtual decimal? UnitPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 规格
        /// </summary>
        [DisplayName("规格")]
        public virtual string Guige
        {
            get;
            set;
        }
        /// <summary>
        /// 磅差
        /// </summary>
        [DisplayName("磅差")]
        public virtual decimal? Bangcha
        {
            get;
            set;
        }
        /// <summary>
        /// 运输单价
        /// </summary>
        [DisplayName("运输单价")]
        public virtual decimal? TransUnitPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 材料总金额
        /// </summary>
        [DisplayName("材料总金额")]
        public virtual decimal? TotalPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 总运费
        /// </summary>
        [DisplayName("总运费")]
        public virtual decimal? TotalTransPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 是否月结
        /// </summary>
        [DisplayName("是否月结")]
        public virtual bool? IsMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 磅差2
        /// </summary>
        [DisplayName("磅差2")]
        public virtual decimal? Bangcha2
        {
            get;
            set;
        }

        [ScriptIgnore]
		public virtual StuffInfo StuffInfo
        {
            get;
			set;
        }
        [ScriptIgnore]
		public virtual StockPact StockPact
        {
            get;
			set;
        }
        [ScriptIgnore]
		public virtual Silo Silo
        {
            get;
			set;
        }

        [ScriptIgnore]
		public virtual SupplyInfo SupplyInfo
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual SupplyInfo TransportInfo
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
