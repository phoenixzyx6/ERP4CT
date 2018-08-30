
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  机组信息抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ProductLine : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ProductLineName);
			sb.Append(DishNum);
			sb.Append(HourNum);
			sb.Append(Remark);
			sb.Append(IsUsed);
			sb.Append(IP);
			sb.Append(Port);
			sb.Append(OrderNum);
			sb.Append(Version);
            sb.Append(KZType);
            sb.Append(KZVersion);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 生产线
        /// </summary>
        [DisplayName("生产线")]
        [StringLength(20)]
        public virtual string ProductLineName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 盘容量
        /// </summary>
        [DisplayName("盘容量")]
        public virtual decimal? DishNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 每小时产量
        /// </summary>
        [DisplayName("每小时产量")]
        public virtual decimal? HourNum
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
        /// 是否启用
        /// </summary>
        [Required]
        [DisplayName("是否启用")]
        public virtual bool IsUsed
        {
            get;
			set;			 
        }	
        /// <summary>
        /// IP地址
        /// </summary>
        [DisplayName("IP地址")]
        [StringLength(50)]
        public virtual string IP
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 端口号
        /// </summary>
        [DisplayName("端口号")]
        [StringLength(20)]
        public virtual string Port
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public virtual int? OrderNum
        {
            get;
			set;			 
        }
        [DisplayName("上位机类型")]
        [StringLength(30)]
        public virtual string KZType
        {
            get;
            set;
        }
        [DisplayName("上位机版本")]
        [StringLength(30)]
        public virtual string KZVersion
        {
            get;
            set;
        }
        #endregion
    }
}
