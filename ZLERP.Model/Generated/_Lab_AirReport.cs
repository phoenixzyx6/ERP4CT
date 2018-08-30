
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 粉煤灰检测报告抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Lab_AirReport : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(DependInfoID);
            sb.Append(Date);
            sb.Append(ReportDate);
            sb.Append(Unit);
            sb.Append(Grade);
            sb.Append(Type);
            sb.Append(GoDate);
            sb.Append(Radix);
            sb.Append(ThinResult);
            sb.Append(ThinConclusion);
            sb.Append(NeedWaterResult);
            sb.Append(NeedWaterConclusion);
            sb.Append(BurntResult);
            sb.Append(BurntConclusion);
            sb.Append(WaterResult);
            sb.Append(WaterConclusion);
            sb.Append(SafeResult);
            sb.Append(SafeConclusion);
            sb.Append(ActiveResult);
            sb.Append(ActiveConclusion);
            sb.Append(Conclusion);
            sb.Append(Remark);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public virtual int? Lab_RecordID
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("检验依据")]
        public virtual int? DependInfoID
        {
            get;
            set;
        }
        /// <summary>
        /// 检测日期
        /// </summary>
        [DisplayName("检测日期")]
        public virtual System.DateTime? Date
        {
            get;
            set;
        }
        /// <summary>
        /// 报告日期
        /// </summary>
        [DisplayName("报告日期")]
        public virtual System.DateTime? ReportDate
        {
            get;
            set;
        }
        /// <summary>
        /// 单位
        /// </summary>
        [DisplayName("单位")]
        [StringLength(30)]
        public virtual string Unit
        {
            get;
            set;
        }
        /// <summary>
        /// 样品等级
        /// </summary>
        [DisplayName("样品等级")]
        [StringLength(30)]
        public virtual string Grade
        {
            get;
            set;
        }
        /// <summary>
        /// 样品类别
        /// </summary>
        [DisplayName("样品类别")]
        [StringLength(30)]
        public virtual string Type
        {
            get;
            set;
        }
        /// <summary>
        /// 出厂日期
        /// </summary>
        [DisplayName("出厂日期")]
        public virtual System.DateTime? GoDate
        {
            get;
            set;
        }
        /// <summary>
        /// 取样基数(T)
        /// </summary>
        [DisplayName("取样基数(T)")]
        public virtual decimal? Radix
        {
            get;
            set;
        }
        /// <summary>
        /// 细度结果
        /// </summary>
        [DisplayName("细度结果")]
        [StringLength(30)]
        public virtual string ThinResult
        {
            get;
            set;
        }
        /// <summary>
        /// 细度结论
        /// </summary>
        [DisplayName("细度结论")]
        [StringLength(100)]
        public virtual string ThinConclusion
        {
            get;
            set;
        }
        /// <summary>
        /// 需水量比结果
        /// </summary>
        [DisplayName("需水量比结果")]
        [StringLength(30)]
        public virtual string NeedWaterResult
        {
            get;
            set;
        }
        /// <summary>
        /// 需水量比结论
        /// </summary>
        [DisplayName("需水量比结论")]
        [StringLength(100)]
        public virtual string NeedWaterConclusion
        {
            get;
            set;
        }
        /// <summary>
        /// 烧失量结果
        /// </summary>
        [DisplayName("烧失量结果")]
        [StringLength(30)]
        public virtual string BurntResult
        {
            get;
            set;
        }
        /// <summary>
        /// 烧失量结论
        /// </summary>
        [DisplayName("烧失量结论")]
        [StringLength(100)]
        public virtual string BurntConclusion
        {
            get;
            set;
        }
        /// <summary>
        /// 含水量结果
        /// </summary>
        [DisplayName("含水量结果")]
        [StringLength(30)]
        public virtual string WaterResult
        {
            get;
            set;
        }
        /// <summary>
        /// 含水量结论
        /// </summary>
        [DisplayName("含水量结论")]
        [StringLength(100)]
        public virtual string WaterConclusion
        {
            get;
            set;
        }
        /// <summary>
        /// 安定性结果
        /// </summary>
        [DisplayName("安定性结果")]
        [StringLength(30)]
        public virtual string SafeResult
        {
            get;
            set;
        }
        /// <summary>
        /// 安定性结论
        /// </summary>
        [DisplayName("安定性结论")]
        [StringLength(100)]
        public virtual string SafeConclusion
        {
            get;
            set;
        }
        /// <summary>
        /// 活性指数（%）结果
        /// </summary>
        [DisplayName("活性指数（%）结果")]
        [StringLength(30)]
        public virtual string ActiveResult
        {
            get;
            set;
        }
        /// <summary>
        /// 活性指数（%）结论
        /// </summary>
        [DisplayName("活性指数（%）结论")]
        [StringLength(100)]
        public virtual string ActiveConclusion
        {
            get;
            set;
        }
        /// <summary>
        /// 检验结论
        /// </summary>
        [DisplayName("检验结论")]
        [StringLength(100)]
        public virtual string Conclusion
        {
            get;
            set;
        }
        /// <summary>
        /// 备  注
        /// </summary>
        [DisplayName("备  注")]
        [StringLength(200)]
        public virtual string Remark
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual Lab_Record Lab_Record
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual Lab_DependInfo Lab_DependInfo
        {
            get;
            set;
        }

        #endregion
    }
}
