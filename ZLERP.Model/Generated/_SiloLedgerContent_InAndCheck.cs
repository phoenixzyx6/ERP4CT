
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 设备数据维护抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _SiloLedgerContent_InAndCheck : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(SiloID);
			sb.Append(StuffID);
			sb.Append(Action);
			sb.Append(BaseStore);
			sb.Append(Num);
            sb.Append(Remaining);
            sb.Append(ActionTime);
			sb.Append(UserName);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 筒仓编号
        /// </summary>
        [Required]
        [DisplayName("筒仓编号")]
        [StringLength(50)]
        public virtual string SiloID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料编号
        /// </summary>
        [DisplayName("材料编号")]
        [StringLength(50)]
        public virtual string StuffID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 操作
        /// </summary>
        [DisplayName("操作")]
        [StringLength(50)]
        public virtual string Action
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 操作前库存
        /// </summary>
        [Required]
        [DisplayName("操作前库存")]
        [StringLength(30)]
        public virtual decimal BaseStore
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 修改量
        /// </summary>
        [DisplayName("修改量")]
        [StringLength(30)]
        public virtual decimal Num
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 修改后剩余
        /// </summary>
        [DisplayName("修改后剩余")]
        [StringLength(30)]
        public virtual decimal Remaining
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 执行时间
        /// </summary>
        [DisplayName("执行时间")]
        public virtual System.DateTime? ActionTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 设备状态
        /// </summary>
        [DisplayName("操作人")]
        [StringLength(50)]
        public virtual string UserName
        {
            get;
			set;			 
        }	
        
		
        #endregion
    }
}
