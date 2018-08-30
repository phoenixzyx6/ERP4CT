
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
    public abstract class _Lab_CA : EntityBase<int?>
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
        /// 样品名称
        /// </summary>
        [DisplayName("样品名称")]
        public virtual string StuffName
        {
            get;
            set;
        }



        /// <summary>
        /// 样品规格
        /// </summary>
        [DisplayName("样品规格")]
        public virtual string GUIGE
        {
            get;
            set;
        }

        /// <summary>
        /// 生产厂家
        /// </summary>
        [DisplayName("生产厂家")]
        public virtual string SupplyName
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
        public virtual string Depend
        {
            get;
            set;
        }


        /// <summary>
        /// 检测日期
        /// </summary>
        [DisplayName("检测日期")]
        public virtual DateTime? Date
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
