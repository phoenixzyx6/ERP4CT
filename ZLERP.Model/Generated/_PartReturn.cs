
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 配件进货退回抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _PartReturn : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ReturnDate);
			sb.Append(Recipientor);
			sb.Append(FactoryID);
			sb.Append(InvoiceNum);
			sb.Append(OperateStatus);
			sb.Append(Purchaser);
			sb.Append(Sender);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 退回日期
        /// </summary>
        [DisplayName("退回日期")]
        public virtual System.DateTime? ReturnDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 接收人
        /// </summary>
        [DisplayName("接收人")]
        [StringLength(30)]
        public virtual string Recipientor
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [StringLength(30)]
        public virtual string FactoryID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 发票张数
        /// </summary>
        [DisplayName("发票张数")]
        public virtual decimal? InvoiceNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 收票方式
        /// </summary>
        [DisplayName("收票方式")]
        [StringLength(50)]
        public virtual string OperateStatus
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 物资采购
        /// </summary>
        [DisplayName("物资采购")]
        [StringLength(30)]
        public virtual string Purchaser
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 送料人
        /// </summary>
        [DisplayName("送料人")]
        [StringLength(30)]
        public virtual string Sender
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(50)]
        public virtual string Remark
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual IList<PartReturnDetail> PartReturnDetails
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
