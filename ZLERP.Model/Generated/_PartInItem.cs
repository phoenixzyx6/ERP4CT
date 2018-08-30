
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 配件入库明细抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _PartInItem : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(InNum);
			sb.Append(UnitPrice);
			sb.Append(BarCode);
			sb.Append(Remark);
			sb.Append(Version);
			sb.Append(ContractID);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 入库数量
        /// </summary>
        [DisplayName("入库数量")]
        public virtual decimal InNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        public virtual decimal? UnitPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 条码
        /// </summary>
        [DisplayName("条码")]
        [StringLength(50)]
        public virtual string BarCode
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
        /// <summary>
        /// 采购合同编号
        /// </summary>
        [DisplayName("采购合同编号")]
        [StringLength(30)]
        public virtual string ContractID
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual PartIn PartIn
        {
            get;
			set;
        }
        [ScriptIgnore]
        public virtual PartInfo PartInfo
        {
            get;
            set;
        }
		
        #endregion
    }
}
