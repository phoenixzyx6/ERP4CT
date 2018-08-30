
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 粉煤灰检测原始记录抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Lab_AirOrigin : EntityBase<string>
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
            sb.Append(Weight);
            sb.Append(AfterWeight);
            sb.Append(Alignment);
            sb.Append(SPercent);
            sb.Append(AddWaterThan);
            sb.Append(AddWater);
            sb.Append(NeedWater);
            sb.Append(DryBefore);
            sb.Append(DryAfter);
            sb.Append(ContentWater);
            sb.Append(A1);
            sb.Append(A2);
            sb.Append(C1);
            sb.Append(C2);
            sb.Append(C1subA1);
            sb.Append(C2subA2);
            sb.Append(CsubAAve);
            sb.Append(Result);
            sb.Append(MachineRun);
            sb.Append(Version);

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
        /// 煤灰品种
        /// </summary>
        [DisplayName("煤灰品种")]
        [StringLength(30)]
        public virtual string Type
        {
            get;
            set;
        }
        /// <summary>
        /// 等   级
        /// </summary>
        [DisplayName("等   级")]
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
        /// 试样质量(g)
        /// </summary>
        [DisplayName("试样质量(g)")]
        public virtual decimal? Weight
        {
            get;
            set;
        }
        /// <summary>
        /// 筛后样品质量(g)
        /// </summary>
        [DisplayName("筛后样品质量(g)")]
        public virtual decimal? AfterWeight
        {
            get;
            set;
        }
        /// <summary>
        /// 校正系数
        /// </summary>
        [DisplayName("校正系数")]
        public virtual decimal? Alignment
        {
            get;
            set;
        }
        /// <summary>
        /// 筛余百分数(%)
        /// </summary>
        [DisplayName("筛余百分数(%)")]
        public virtual decimal? SPercent
        {
            get;
            set;
        }
        /// <summary>
        /// 对比胶砂的加水量(ml)
        /// </summary>
        [DisplayName("对比胶砂的加水量(ml)")]
        public virtual decimal? AddWaterThan
        {
            get;
            set;
        }
        /// <summary>
        /// 试验胶砂加水量(ml)
        /// </summary>
        [DisplayName("试验胶砂加水量(ml)")]
        public virtual decimal? AddWater
        {
            get;
            set;
        }
        /// <summary>
        /// 需水量比(%)
        /// </summary>
        [DisplayName("需水量比(%)")]
        public virtual decimal? NeedWater
        {
            get;
            set;
        }
        /// <summary>
        /// 烘干前试样质量(g)
        /// </summary>
        [DisplayName("烘干前试样质量(g)")]
        public virtual decimal? DryBefore
        {
            get;
            set;
        }
        /// <summary>
        /// 烘干后试样质量(g)
        /// </summary>
        [DisplayName("烘干后试样质量(g)")]
        public virtual decimal? DryAfter
        {
            get;
            set;
        }
        /// <summary>
        /// 含水量（%）
        /// </summary>
        [DisplayName("含水量（%）")]
        public virtual decimal? ContentWater
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? A1
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? A2
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? C1
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? C2
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? C1subA1
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? C2subA2
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? CsubAAve
        {
            get;
            set;
        }
        /// <summary>
        /// 结果判断
        /// </summary>
        [DisplayName("结果判断")]
        [StringLength(50)]
        public virtual string Result
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
        public virtual IList<Lab_BurntInfo> Lab_BurntInfos
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<Lab_ActiveInfo> Lab_ActiveInfos
        {
            get;
            set;
        }
        #endregion
    }
}
