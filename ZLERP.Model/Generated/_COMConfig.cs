
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// COM配置抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _COMConfig : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(COMName);
			sb.Append(PortName);
			sb.Append(BaudRate);
			sb.Append(Parity);
			sb.Append(DataBits);
			sb.Append(StopBits);
			sb.Append(BeginCode);
			sb.Append(EndCode);
			sb.Append(DataModel);
			sb.Append(Version);
            sb.Append(IsOpen);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// COM名称
        /// </summary>
        [DisplayName("COM名称")]
        [StringLength(50)]
        public virtual string COMName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// COM口
        /// </summary>
        [DisplayName("COM口")]
        [StringLength(50)]
        public virtual string PortName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 波特率
        /// </summary>
        [DisplayName("波特率")]
        public virtual int? BaudRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 校验位
        /// </summary>
        [DisplayName("校验位")]
        public virtual int? Parity
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 数据位
        /// </summary>
        [DisplayName("数据位")]
        public virtual int? DataBits
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 停止位
        /// </summary>
        [DisplayName("停止位")]
        public virtual int? StopBits
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 起始字符
        /// </summary>
        [DisplayName("起始字符")]
        [StringLength(20)]
        public virtual string BeginCode
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 截止字符
        /// </summary>
        [DisplayName("截止字符")]
        [StringLength(20)]
        public virtual string EndCode
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 数据模式
        /// </summary>
        [DisplayName("数据模式")]
        [StringLength(20)]
        public virtual string DataModel
        {
            get;
			set;			 
        }
        /// <summary>
        /// 是否打开
        /// </summary>
        [DisplayName("是否打开")]
        public virtual bool IsOpen
        {
            get;
            set;
        }
        #endregion
    }
}
