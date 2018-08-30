
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
    public abstract class _Lab_Record : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(Date);
            sb.Append(Time);
            sb.Append(Carno);
            sb.Append(Stuffid);
            sb.Append(Supplyid);
            sb.Append(StartDate);
            sb.Append(EndDate);
            sb.Append(IsWhole);
            sb.Append(ShowpeieNo);
            sb.Append(Weight);
            sb.Append(SumWeight);
            sb.Append(Siloid);
            sb.Append(InMan);
            sb.Append(Remark);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 日期
        /// </summary>
        [DisplayName("日期")]
        public virtual System.DateTime? Date
        {
            get;
            set;
        }
        /// <summary>
        /// 时间
        /// </summary>
        [DisplayName("时间")]
        [StringLength(10)]
        public virtual string Time
        {
            get;
            set;
        }
        /// <summary>
        /// 运输车号
        /// </summary>
        [DisplayName("运输车号")]
        [StringLength(20)]
        public virtual string Carno
        {
            get;
            set;
        }
        /// <summary>
        /// 材料id
        /// </summary>
        [Required]
        [DisplayName("材料")]
        [StringLength(30)]
        public virtual string Stuffid
        {
            get;
            set;
        }
        /// <summary>
        /// 生产厂家id
        /// </summary>
        [DisplayName("生产厂家")]
        [StringLength(30)]
        public virtual string Supplyid
        {
            get;
            set;
        }
        /// <summary>
        /// 发车时间
        /// </summary>
        [DisplayName("发车时间")]
        public virtual System.DateTime? StartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 到站时间
        /// </summary>
        [DisplayName("到站时间")]
        public virtual System.DateTime? EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 铅封是否完好
        /// </summary>
        [DisplayName("铅封是否完好")]
        public virtual bool IsWhole
        {
            get;
            set;
        }
        /// <summary>
        /// 样品编号(自动生成)
        /// </summary>
        [DisplayName("样品编号")]
        [StringLength(50)]
        public virtual string ShowpeieNo
        {
            get;
            set;
        }
        /// <summary>
        /// 单车车重(T)
        /// </summary>
        [DisplayName("单车车重(T)")]
        public virtual decimal? Weight
        {
            get;
            set;
        }
        /// <summary>
        /// 累计重量(T)
        /// </summary>
        [DisplayName("累计重量(T)")]
        public virtual decimal? SumWeight
        {
            get;
            set;
        }
        /// <summary>
        /// 罐编号
        /// </summary>
        [DisplayName("罐编号")]
        [StringLength(30)]
        public virtual string Siloid
        {
            get;
            set;
        }
        /// <summary>
        /// 收料人员
        /// </summary>
        [DisplayName("收料人员")]
        [StringLength(15)]
        public virtual string InMan
        {
            get;
            set;
        }
        /// <summary>
        /// 进出库ID
        /// </summary>
        [DisplayName("进出库ID")]
        public virtual string stuffinid
        {
            get;
            set;
        }
        /// <summary>
        /// 材料大类id
        /// </summary>
        [DisplayName("材料大类id")]
        public virtual string FinalStuffTypeID
        {
            get;
            set;
        }
        /// <summary>
        /// 材料id
        /// </summary>
        [DisplayName("材料id")]
        public virtual string StuffTypeID
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(100)]
        public virtual string Remark
        {
            get;
            set;
        }
        /// <summary>
        /// 品种规格
        /// </summary>
        [DisplayName("品种规格")]
        [StringLength(30)]
        public virtual string Spec
        {
            get;
            set;
        }
        

        #endregion

        [ScriptIgnore]
        public virtual Silo Silo
        {
            get;
            set;
        }


        [ScriptIgnore]
        public virtual StuffInfo StuffInfo
        {
            get;
            set;
        }


        [ScriptIgnore]
        public virtual SupplyInfo SupplyInfo
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<Lab_AirOrigin> Lab_AirOrigins
        {
            get;
            set;
        }


        [ScriptIgnore]
        public virtual IList<Lab_AirReport> Lab_AirReports
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<Lab_Air2Origin> Lab_Air2Origins
        {
            get;
            set;
        }


        [ScriptIgnore]
        public virtual IList<Lab_Air2Report> Lab_Air2Reports
        {
            get;
            set;
        }
    }
}
