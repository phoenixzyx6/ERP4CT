
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 矿粉检测报告抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Lab_Air2Report : EntityBase<string>
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
            sb.Append(DensityResult);
            sb.Append(DensityConclusion);
            sb.Append(SpecificResult);
            sb.Append(SpecificConclusion);
            sb.Append(Active7dResult);
            sb.Append(Active7dConclusion);
            sb.Append(Active28dResult);
            sb.Append(Active28dConclusion);
            sb.Append(FluidityResult);
            sb.Append(FluidityConclusion);
            sb.Append(WaterResult);
            sb.Append(WaterConclusion);
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
        /// 检测依据
        /// </summary>
        [DisplayName("检测依据")]
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
        /// 密度(g/m3)结果
        /// </summary>
        [DisplayName("密度(g/m3)结果")]
        [StringLength(50)]
        public virtual string DensityResult
        {
            get;
            set;
        }
        /// <summary>
        /// 密度(g/m3)结论
        /// </summary>
        [DisplayName("密度(g/m3)结论")]
        [StringLength(50)]
        public virtual string DensityConclusion
        {
            get;
            set;
        }
        /// <summary>
        /// 比表面积(㎡/kg)结果
        /// </summary>
        [DisplayName("比表面积(㎡/kg)结果")]
        [StringLength(50)]
        public virtual string SpecificResult
        {
            get;
            set;
        }
        /// <summary>
        /// 比表面积(㎡/kg)结论
        /// </summary>
        [DisplayName("比表面积(㎡/kg)结论")]
        [StringLength(50)]
        public virtual string SpecificConclusion
        {
            get;
            set;
        }
        /// <summary>
        /// 活性指数7d结果
        /// </summary>
        [DisplayName("活性指数7d结果")]
        [StringLength(50)]
        public virtual string Active7dResult
        {
            get;
            set;
        }
        /// <summary>
        /// 活性指数7d结论
        /// </summary>
        [DisplayName("活性指数7d结论")]
        [StringLength(50)]
        public virtual string Active7dConclusion
        {
            get;
            set;
        }
        /// <summary>
        /// 活性指数28d结果
        /// </summary>
        [DisplayName("活性指数28d结果")]
        [StringLength(50)]
        public virtual string Active28dResult
        {
            get;
            set;
        }
        /// <summary>
        /// 活性指数28d结论
        /// </summary>
        [DisplayName("活性指数28d结论")]
        [StringLength(50)]
        public virtual string Active28dConclusion
        {
            get;
            set;
        }
        /// <summary>
        /// 流动度比结果
        /// </summary>
        [DisplayName("流动度比结果")]
        [StringLength(50)]
        public virtual string FluidityResult
        {
            get;
            set;
        }
        /// <summary>
        /// 流动度比结论
        /// </summary>
        [DisplayName("流动度比结论")]
        [StringLength(50)]
        public virtual string FluidityConclusion
        {
            get;
            set;
        }
        /// <summary>
        /// 含水量结果
        /// </summary>
        [DisplayName("含水量结果")]
        [StringLength(50)]
        public virtual string WaterResult
        {
            get;
            set;
        }
        /// <summary>
        /// 含水量结论
        /// </summary>
        [DisplayName("含水量结论")]
        [StringLength(50)]
        public virtual string WaterConclusion
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


        #endregion
    }
}
