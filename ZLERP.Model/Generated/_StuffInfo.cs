
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  原料信息表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _StuffInfo : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(StuffName);
            sb.Append(StuffShortName);
            sb.Append(RootIn);
            sb.Append(Spec);
            sb.Append(Sizex);
            sb.Append(SWConstantX);
            sb.Append(SWConstantY);
            sb.Append(SopRate);
            sb.Append(Chlorin);
            sb.Append(Alkali);
            sb.Append(Proportion);
            sb.Append(Price);
            sb.Append(Inventory);
            sb.Append(StuffTypeID);
            sb.Append(MinWarmContent);
            sb.Append(MaxWarmContent);
            sb.Append(IsUsed);
            sb.Append(IsMetage);
            sb.Append(BrightRate);
            sb.Append(DarkRate);
            sb.Append(TestType);
            sb.Append(OrderNum);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 原料名称
        /// </summary>
        [DisplayName("原料名称")]
        [StringLength(20)]
        public virtual string StuffName
        {
            get;
            set;
        }
        /// <summary>
        /// 原料简码
        /// </summary>
        [DisplayName("原料简码")]
        [StringLength(20)]
        public virtual string StuffShortName
        {
            get;
            set;
        }
        /// <summary>
        /// 来源地
        /// </summary>
        [DisplayName("来源地")]
        [StringLength(50)]
        public virtual string RootIn
        {
            get;
            set;
        }
        /// <summary>
        /// 材料品种
        /// </summary>
        [DisplayName("材料品种")]
        [StringLength(50)]
        public virtual string Spec
        {
            get;
            set;
        }
        /// <summary>
        /// 尺寸
        /// </summary>
        [DisplayName("尺寸")]
        [StringLength(20)]
        public virtual string Sizex
        {
            get;
            set;
        }
        /// <summary>
        /// 砂水常数X
        /// </summary>
        [DisplayName("砂水常数X")]
        public virtual decimal? SWConstantX
        {
            get;
            set;
        }
        /// <summary>
        /// 砂水常数Y
        /// </summary>
        [DisplayName("砂水常数Y")]
        public virtual decimal? SWConstantY
        {
            get;
            set;
        }
        /// <summary>
        /// 吸水率
        /// </summary>
        [DisplayName("换算系数")]
        [Required]
        public virtual decimal? SopRate
        {
            get;
            set;
        }
        /// <summary>
        /// 氯含量
        /// </summary>
        [DisplayName("氯含量")]
        public virtual decimal? Chlorin
        {
            get;
            set;
        }
        /// <summary>
        /// 碱含量
        /// </summary>
        [DisplayName("碱含量")]
        public virtual decimal? Alkali
        {
            get;
            set;
        }
        /// <summary>
        /// 比重
        /// </summary>
        [DisplayName("比重")]
        [StringLength(20)]
        public virtual string Proportion
        {
            get;
            set;
        }
        /// <summary>
        /// 当前价格
        /// </summary>
        [DisplayName("当前价格")]
        public virtual decimal? Price
        {
            get;
            set;
        }
        /// <summary>
        /// 库存量
        /// </summary>
        [DisplayName("库存量(KG)")]
        public virtual decimal Inventory
        {
            get;
            set;
        }
        /// <summary>
        /// 材料类型编号
        /// </summary>
        [DisplayName("材料类型编号")]
        [StringLength(50)]
        public virtual string StuffTypeID
        {
            get;
            set;
        }
        /// <summary>
        /// 最小报警库存
        /// </summary>
        [DisplayName("最小报警库存(KG)")]
        public virtual decimal? MinWarmContent
        {
            get;
            set;
        }
        /// <summary>
        /// 最大报警库存
        /// </summary>
        [DisplayName("最大报警库存(KG)")]
        public virtual decimal? MaxWarmContent
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
        /// 是否过磅
        /// </summary>
        [Required]
        [DisplayName("是否过磅")]
        public virtual bool IsMetage
        {
            get;
            set;
        }
        /// <summary>
        /// 明扣率
        /// </summary>
        [Required]
        [DisplayName("明扣率%")]
        public virtual decimal BrightRate
        {
            get;
            set;
        }
        /// <summary>
        /// 暗扣率
        /// </summary>
        [Required]
        [DisplayName("暗扣率%")]
        public virtual decimal DarkRate
        {
            get;
            set;
        }
        /// <summary>
        /// 试验类别
        /// </summary>
        [DisplayName("试验类别")]
        [StringLength(50)]
        public virtual string TestType
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
        [ScriptIgnore]
        public virtual IList<Silo> Silos
        {
            get;
            set;
        }


        [ScriptIgnore]
        public virtual IList<StuffIn> StuffIns
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual StuffType StuffType
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<CustMixpropItem> CustMixpropItems
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
        public virtual IList<StockPact> StockPacts
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<StockPlan> StockPlans
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<StuffPrice> StuffPrices
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<ProductRecordItem> ProductRecordItems
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<FAExam> FAExams
        {
            get;
            set;
        }

        public virtual IList<CEExam> CEExams
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<CAExam> CAExams
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<AIR2Exam> AIR2Exams
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<AIR1Exam> AIR1Exams
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<ADMExam> ADMExams
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<ADM2Exam> ADM2Exams
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<MonthAccount> MonthAccounts
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<StuffAccount> StuffAccounts
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<ManualProductItems> ManualProductItems
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<Lab_Record> Lab_Records
        {
            get;
            set;
        }
        #endregion
    }
}
