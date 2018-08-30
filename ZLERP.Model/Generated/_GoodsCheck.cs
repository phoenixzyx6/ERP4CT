
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 物资盘点记录抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _GoodsCheck : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(CheckMan);
			sb.Append(CheckNum);
			sb.Append(CheckTime);
			sb.Append(Operator);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 盘点人
        /// </summary>
        [DisplayName("盘点人")]
        [StringLength(50)]
        public virtual string CheckMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 盘点数量
        /// </summary>
        [Required]
        [DisplayName("盘点数量")]
        [Range(0, int.MaxValue, ErrorMessage = "请输入大于等于0的数。")]
        public virtual decimal CheckNum
        {
            get;
			set;			 
        }
        /// <summary>
        /// 账面数量
        /// </summary>
        [Required]
        [DisplayName("账面数量")]
        
        public virtual decimal SystemNum
        {
            get;
            set;
        }
        /// <summary>
        /// 盘盈亏
        /// </summary>
        [Required]
        [DisplayName("盘盈亏")]       
        public virtual decimal ProfitAndLoss
        {
            get;
            set;
        }	
        /// <summary>
        /// 盘点时间
        /// </summary>
        [Required]
        [DisplayName("盘点时间")]
        public virtual System.DateTime CheckTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 经办人
        /// </summary>
        [DisplayName("经办人")]
        [StringLength(50)]
        public virtual string Operator
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
        [ScriptIgnore]
		public virtual GoodsInfo GoodsInfo
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
