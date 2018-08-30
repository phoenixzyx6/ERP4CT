
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 矿粉检测原始记录抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Lab_Air2Origin : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(Type);
            sb.Append(Grade);
            sb.Append(Description);
            sb.Append(SuccessDate);
            sb.Append(SuccessTemperature);
            sb.Append(SuccessWet);
            sb.Append(SpecificTemperature);
            sb.Append(SpecificWet);
            sb.Append(SpecificK);
            sb.Append(SpecificVoidage);
            sb.Append(SpecificBarrelVolume);
            sb.Append(SpecificPowderDensity);
            sb.Append(SpecificQuality1);
            sb.Append(SpecificQuality2);
            sb.Append(SpecificArea1);
            sb.Append(SpecificArea2);
            sb.Append(ContrastMortar);
            sb.Append(TestMortar);
            sb.Append(DryBeforeQuality);
            sb.Append(DryAfterQuality);
            sb.Append(MachineRun);
            sb.Append(Version);
            sb.Append(Remark);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("检测主表ID")]
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
        /// 矿粉品种
        /// </summary>
        [DisplayName("矿粉品种")]
        [StringLength(30)]
        public virtual string Type
        {
            get;
            set;
        }
        /// <summary>
        /// 样品级别
        /// </summary>
        [DisplayName("样品级别")]
        [StringLength(30)]
        public virtual string Grade
        {
            get;
            set;
        }
        /// <summary>
        /// 样品说明
        /// </summary>
        [DisplayName("样品说明")]
        [StringLength(30)]
        public virtual string Description
        {
            get;
            set;
        }
        /// <summary>
        /// 成型日期
        /// </summary>
        [DisplayName("成型日期")]
        public virtual System.DateTime? SuccessDate
        {
            get;
            set;
        }
        /// <summary>
        /// 成型室温度
        /// </summary>
        [DisplayName("成型室温度")]
        public virtual decimal? SuccessTemperature
        {
            get;
            set;
        }
        /// <summary>
        /// 成型室湿度
        /// </summary>
        [DisplayName("成型室湿度")]
        public virtual decimal? SuccessWet
        {
            get;
            set;
        }
        /// <summary>
        /// 温度(℃)
        /// </summary>
        [DisplayName("温度(℃)")]
        public virtual decimal? SpecificTemperature
        {
            get;
            set;
        }
        /// <summary>
        /// 湿度
        /// </summary>
        [DisplayName("湿度")]
        public virtual decimal? SpecificWet
        {
            get;
            set;
        }
        /// <summary>
        /// K值
        /// </summary>
        [DisplayName("K值")]
        public virtual decimal? SpecificK
        {
            get;
            set;
        }
        /// <summary>
        /// 空隙率
        /// </summary>
        [DisplayName("空隙率")]
        public virtual decimal? SpecificVoidage
        {
            get;
            set;
        }
        /// <summary>
        /// 容桶体积(cm3)
        /// </summary>
        [DisplayName("容桶体积(cm3)")]
        public virtual decimal? SpecificBarrelVolume
        {
            get;
            set;
        }
        /// <summary>
        /// 矿粉密度(g/cm3)
        /// </summary>
        [DisplayName("矿粉密度(g/cm3)")]
        public virtual decimal? SpecificPowderDensity
        {
            get;
            set;
        }
        /// <summary>
        /// 矿粉质量1(g)
        /// </summary>
        [DisplayName("矿粉质量1(g)")]
        public virtual decimal? SpecificQuality1
        {
            get;
            set;
        }
        /// <summary>
        /// 矿粉质量2(g)
        /// </summary>
        [DisplayName("矿粉质量2(g)")]
        public virtual decimal? SpecificQuality2
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? SpecificArea1
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? SpecificArea2
        {
            get;
            set;
        }
        /// <summary>
        /// 对比砂浆(mm)
        /// </summary>
        [DisplayName("对比砂浆(mm)")]
        public virtual decimal? ContrastMortar
        {
            get;
            set;
        }
        /// <summary>
        /// 试验砂浆(mm)
        /// </summary>
        [DisplayName("试验砂浆(mm)")]
        public virtual decimal? TestMortar
        {
            get;
            set;
        }
        /// <summary>
        /// 烘干前试样质量(g)
        /// </summary>
        [DisplayName("烘干前试样质量(g)")]
        public virtual decimal? DryBeforeQuality
        {
            get;
            set;
        }
        /// <summary>
        /// 烘干后试样质量(g)
        /// </summary>
        [DisplayName("烘干后试样质量(g)")]
        public virtual decimal? DryAfterQuality
        {
            get;
            set;
        }
        /// <summary>
        /// 仪器设备运行状况
        /// </summary>
        [DisplayName("仪器设备运行状况")]
        [StringLength(50)]
        public virtual string MachineRun
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(350)]
        public virtual string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 比表面积平均值(m2/kg)
        /// </summary>
        [DisplayName("比表面积平均值(m2/kg)")]
        [StringLength(350)]
        public virtual decimal? SpecificArea
        {
            get;
            set;
        }
        /// <summary>
        /// 流动度比(%)
        /// </summary>
        [DisplayName("流动度比(%)")]
        [StringLength(350)]
        public virtual decimal? FluidityRatio
        {
            get;
            set;
        }
        /// <summary>
        /// 含水量(%)
        /// </summary>
        [DisplayName("含水量(%)")]
        [StringLength(350)]
        public virtual decimal? ContentWater
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


        [ScriptIgnore]
        public virtual Lab_Record Lab_Record
        {
            get;
            set;
        }


        [ScriptIgnore]
        public virtual IList<Lab_Air2ActiveInfo> Lab_Air2ActiveInfos
        {
            get;
            set;
        }


        [ScriptIgnore]
        public virtual IList<Lab_Air2Density> Lab_Air2Densitys
        {
            get;
            set;
        }


        #endregion
    }
}
