using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  原料入库记录
    /// </summary>
	public class StuffIn : _StuffIn
    {
        
        [Required]
        public override string CarNo
        {
            get
            {
                return base.CarNo;
            }
            set
            {
                base.CarNo = value;
            }
        }
        [DisplayName("入库原料"), Required]
        public virtual string StuffID
        {
            get;
            set;
        }
        public virtual string StuffName
        {
            get
            {
                return this.StuffInfo == null ? "" : this.StuffInfo.StuffName;
            }
        }
        [DisplayName("入库筒仓"), Required]
        public virtual string SiloID
        {
            get;
            set;
        }
        public virtual string SiloName
        {
            get
            {
                return this.Silo == null ? "" : this.Silo.SiloName;
            }
        }
        [DisplayName("采购单号")]
        public virtual string StockPactID
        {
            get;
            set;
        }
        public virtual string StockPactNo
        {
            get
            {
                return this.StockPact == null ? "" : this.StockPact.StockPactNo;
            }
        }
        [DisplayName("供货厂商"), Required]
        public virtual string SupplyID
        {
            get;
            set;
        }

        [DisplayName("毛重图片1")]
        public virtual string pic1
        {
            get;
            set;
        }

        [DisplayName("毛重图片2")]
        public virtual string pic2
        {
            get;
            set;
        }

        [DisplayName("毛重图片3")]
        public virtual string pic3
        {
            get;
            set;
        }

        [DisplayName("毛重图片4")]
        public virtual string pic4
        {
            get;
            set;
        }

        public virtual string SupplyName
        {
            get
            {
                return this.SupplyInfo == null ? "" : this.SupplyInfo.SupplyName;
            }
        }

        [DisplayName("运输公司")]
        public virtual string TransportID
        {
            get;
            set;
        }

        public virtual string TransportName
        {
            get
            {
                return this.TransportInfo == null ? "" : this.TransportInfo.SupplyName;
            }
        }

        [DisplayName("是否委托试验")]
        public virtual bool IsExam
        {
            get;
            set;
        }



        [DisplayName("规格")]
        public virtual string Spec { get; set; }
    }

    /// <summary>
    /// InNum验证信息
    /// 便于学习使用
    /// </summary>
    public class InNumValidationAttribute :System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        public InNumValidationAttribute()
            : base()
        {

        }
        protected override ValidationResult IsValid(object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            object obj = validationContext.ObjectInstance;

            StuffIn newobj = (StuffIn)obj;
            if (newobj.InNum > newobj.TotalNum - newobj.CarWeight)
                return new ValidationResult(validationContext.DisplayName + "的数量大于了净重值！");

            return ValidationResult.Success;
        }

       
    }


  

}