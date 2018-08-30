
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 活性指数抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Lab_ActiveInfo : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(D7_1);
            sb.Append(D7_2);
            sb.Append(D7_3);
            sb.Append(D28_1);
            sb.Append(D28_2);
            sb.Append(D28_3);
            sb.Append(S7_1);
            sb.Append(S7_2);
            sb.Append(S7_3);
            sb.Append(S28_1);
            sb.Append(S28_2);
            sb.Append(S28_3);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties
        /// <summary>
        /// 检测编号
        /// </summary>
        [DisplayName("检测编号")]
        public virtual string Lab_AirOriginId
        {
            get;
            set;
        }
        /// <summary>
        /// 对比胶砂7d 荷载
        /// </summary>
        [DisplayName("对比胶砂7d 荷载")]
        public virtual decimal? D7_1
        {
            get;
            set;
        }
        /// <summary>
        /// 对比胶砂7d 单块值
        /// </summary>
        [DisplayName("对比胶砂7d 单块值")]
        public virtual decimal? D7_2
        {
            get;
            set;
        }
        /// <summary>
        /// 对比胶砂7d 代表值
        /// </summary>
        [DisplayName("对比胶砂7d 代表值")]
        public virtual decimal? D7_3
        {
            get;
            set;
        }
        /// <summary>
        /// 对比胶砂28d 荷载
        /// </summary>
        [DisplayName("对比胶砂28d 荷载")]
        public virtual decimal? D28_1
        {
            get;
            set;
        }
        /// <summary>
        /// 对比胶砂28d 单块值
        /// </summary>
        [DisplayName("对比胶砂28d 单块值")]
        public virtual decimal? D28_2
        {
            get;
            set;
        }
        /// <summary>
        /// 对比胶砂28d 代表值
        /// </summary>
        [DisplayName("对比胶砂28d 代表值")]
        public virtual decimal? D28_3
        {
            get;
            set;
        }
        /// <summary>
        /// 试样胶砂7d 荷载
        /// </summary>
        [DisplayName("试样胶砂7d 荷载")]
        public virtual decimal? S7_1
        {
            get;
            set;
        }
        /// <summary>
        /// 试样胶砂7d 单块值
        /// </summary>
        [DisplayName("试样胶砂7d 单块值")]
        public virtual decimal? S7_2
        {
            get;
            set;
        }
        /// <summary>
        /// 试样胶砂7d 代表值
        /// </summary>
        [DisplayName("试样胶砂7d 代表值")]
        public virtual decimal? S7_3
        {
            get;
            set;
        }
        /// <summary>
        /// 试样胶砂28d 荷载
        /// </summary>
        [DisplayName("试样胶砂28d 荷载")]
        public virtual decimal? S28_1
        {
            get;
            set;
        }
        /// <summary>
        /// 试样胶砂28d 单块值
        /// </summary>
        [DisplayName("试样胶砂28d 单块值")]
        public virtual decimal? S28_2
        {
            get;
            set;
        }
        /// <summary>
        /// 试样胶砂28d 代表值
        /// </summary>
        [DisplayName("试样胶砂28d 代表值")]
        public virtual decimal? S28_3
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual Lab_AirOrigin Lab_AirOrigin
        {
            get;
            set;
        }


        #endregion
    }
}
