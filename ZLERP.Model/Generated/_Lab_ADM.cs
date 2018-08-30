
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 验收取样记录抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Lab_ADM : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 父ID
        /// </summary>
        [DisplayName("父ID")]
        public virtual int Lab_RecordID
        {
            get;
            set;
        }

        /// <summary>
        /// 品种
        /// </summary>
        [DisplayName("品种")]
        public virtual string Type
        {
            get;
            set;
        }

        /// <summary>
        /// 样品说明
        /// </summary>
        [DisplayName("样品说明")]
        public virtual string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 检测依据
        /// </summary>
        [DisplayName("检测依据")]
        public virtual string One_Depend
        {
            get;
            set;
        }

        /// <summary>
        /// 试验室温度
        /// </summary>
        [DisplayName("试验室温度")]
        public virtual string One_Temperature
        {
            get;
            set;
        }

        /// <summary>
        /// 试验室湿度
        /// </summary>
        [DisplayName("试验室湿度")]
        public virtual string One_Wet
        {
            get;
            set;
        }

        /// <summary>
        /// 检测依据
        /// </summary>
        [DisplayName("检测依据")]
        public virtual string Two_Depend
        {
            get;
            set;
        }

        /// <summary>
        /// 试验室温度
        /// </summary>
        [DisplayName("试验室温度")]
        public virtual string Two_Temperature
        {
            get;
            set;
        }

        /// <summary>
        /// 试验室湿度
        /// </summary>
        [DisplayName("试验室湿度")]
        public virtual string Two_Wet
        {
            get;
            set;
        }

        /// <summary>
        /// 检测日期
        /// </summary>
        [DisplayName("检测日期")]
        public virtual DateTime? Two_Date
        {
            get;
            set;
        }

        /// <summary>
        /// 检测日期
        /// </summary>
        [DisplayName("检测日期")]
        public virtual DateTime? One_Date
        {
            get;
            set;
        }

        /// <summary>
        /// 砂（kg）
        /// </summary>
        [DisplayName("砂（kg）")]
        public virtual decimal? SHA
        {
            get;
            set;
        }

        /// <summary>
        /// 石子（kg）
        /// </summary>
        [DisplayName("石子（kg）")]
        public virtual decimal? SHI
        {
            get;
            set;
        }

        /// <summary>
        /// 外加剂（kg）
        /// </summary>
        [DisplayName("外加剂（kg）")]
        public virtual decimal? W
        {
            get;
            set;
        }

        /// <summary>
        /// 水（kg）
        /// </summary>
        [DisplayName("水（kg）")]
        public virtual decimal? SHUI
        {
            get;
            set;
        }
        #endregion

        [ScriptIgnore]
        public virtual Lab_Record Lab_Record
        {
            get;
            set;
        }
    }
}
