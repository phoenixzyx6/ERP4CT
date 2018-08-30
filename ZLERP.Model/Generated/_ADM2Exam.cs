
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 膨胀剂试验抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ADM2Exam : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ExamTime);
			sb.Append(ReportTime);
			sb.Append(DeputyNum);
			sb.Append(OutID);
			sb.Append(OutDate);
			sb.Append(InDate);
			sb.Append(SampDate);
			sb.Append(MoldDate);
			sb.Append(CementBreed);
			sb.Append(ConStrength);
			sb.Append(BatchNo);
			sb.Append(SurArea);
			sb.Append(Method);
			sb.Append(SampWeight);
			sb.Append(SuDryWeight);
			sb.Append(SuPer);
			sb.Append(AddWaTime);
			sb.Append(BeginFreTime);
			sb.Append(EndFreTime);
			sb.Append(BoTime);
			sb.Append(PinBoTime);
			sb.Append(Wa7L0);
			sb.Append(Wa7L);
			sb.Append(Wa7L1);
			sb.Append(Wa7Lim);
			sb.Append(Air21L0);
			sb.Append(Air21L);
			sb.Append(Air21L1);
			sb.Append(Air21Lim);
			sb.Append(Wa28L0);
			sb.Append(Wa28L);
			sb.Append(Wa28L1);
			sb.Append(Wa28Lim);
			sb.Append(Other);
			sb.Append(LimCon);
			sb.Append(Temp3);
			sb.Append(Hum3);
			sb.Append(ExamTime3);
			sb.Append(Imz3);
			sb.Append(Str3);
			sb.Append(Imy3);
			sb.Append(Temp7);
			sb.Append(Hum7);
			sb.Append(ExamTime7);
			sb.Append(Imz7);
			sb.Append(Str7);
			sb.Append(Imy7);
			sb.Append(Temp28);
			sb.Append(Hum28);
			sb.Append(ExamTime28);
			sb.Append(Imz28);
			sb.Append(Str28);
			sb.Append(Imy28);
			sb.Append(TempQe);
			sb.Append(HumQe);
			sb.Append(ExamTimeQe);
			sb.Append(ImzQe);
			sb.Append(StrQe);
			sb.Append(ImyQe);
			sb.Append(AvaTime);
			sb.Append(StandJudge);
			sb.Append(Assessor);
			sb.Append(Principal);
			sb.Append(IsUse);
			sb.Append(ExamGist);
			sb.Append(ExamResult);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 试验时间
        /// </summary>
        [DisplayName("试验时间")]
        public virtual System.DateTime? ExamTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 报告时间
        /// </summary>
        [DisplayName("报告时间")]
        public virtual System.DateTime? ReportTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 代表数量(T)
        /// </summary>
        [DisplayName("代表数量(T)")]
        public virtual decimal? DeputyNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 出厂编号
        /// </summary>
        [DisplayName("出厂编号")]
        [StringLength(50)]
        public virtual string OutID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 出厂日期
        /// </summary>
        [DisplayName("出厂日期")]
        public virtual System.DateTime? OutDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 进厂日期
        /// </summary>
        [DisplayName("进厂日期")]
        public virtual System.DateTime? InDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 采样日期
        /// </summary>
        [DisplayName("采样日期")]
        public virtual System.DateTime? SampDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 成型日期
        /// </summary>
        [DisplayName("成型日期")]
        public virtual System.DateTime? MoldDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 水泥品种
        /// </summary>
        [DisplayName("水泥品种")]
        [StringLength(50)]
        public virtual string CementBreed
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 强度等级
        /// </summary>
        [DisplayName("强度等级")]
        [StringLength(50)]
        public virtual string ConStrength
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 批号
        /// </summary>
        [DisplayName("批号")]
        [StringLength(50)]
        public virtual string BatchNo
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 比表面积
        /// </summary>
        [DisplayName("比表面积")]
        public virtual decimal? SurArea
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 方法
        /// </summary>
        [DisplayName("方法")]
        [StringLength(20)]
        public virtual string Method
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 试样重g
        /// </summary>
        [DisplayName("试样重g")]
        public virtual decimal? SampWeight
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 筛余干重g
        /// </summary>
        [DisplayName("筛余干重g")]
        public virtual decimal? SuDryWeight
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 筛余百分比%
        /// </summary>
        [DisplayName("筛余百分比%")]
        public virtual decimal? SuPer
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 加水时间
        /// </summary>
        [DisplayName("加水时间")]
        public virtual System.DateTime? AddWaTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 始凝时间
        /// </summary>
        [DisplayName("始凝时间")]
        public virtual System.DateTime? BeginFreTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 终凝时间
        /// </summary>
        [DisplayName("终凝时间")]
        public virtual System.DateTime? EndFreTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 针距底板2~3mm时间
        /// </summary>
        [DisplayName("针距底板2~3mm时间")]
        public virtual System.DateTime? BoTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 针沉入净浆1~0.5mm时间
        /// </summary>
        [DisplayName("针沉入净浆1~0.5mm时间")]
        public virtual System.DateTime? PinBoTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 水中7天基准长度L0
        /// </summary>
        [DisplayName("水中7天基准长度L0")]
        public virtual decimal? Wa7L0
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 水中7天初始长度L
        /// </summary>
        [DisplayName("水中7天初始长度L")]
        public virtual decimal? Wa7L
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 水中7天龄期长度L1
        /// </summary>
        [DisplayName("水中7天龄期长度L1")]
        public virtual decimal? Wa7L1
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 水中7天限制膨胀率
        /// </summary>
        [DisplayName("水中7天限制膨胀率")]
        public virtual decimal? Wa7Lim
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 空中21天基准长度L0
        /// </summary>
        [DisplayName("空中21天基准长度L0")]
        public virtual decimal? Air21L0
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 空中21天初始长度L
        /// </summary>
        [DisplayName("空中21天初始长度L")]
        public virtual decimal? Air21L
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 空中21天龄期长度L1
        /// </summary>
        [DisplayName("空中21天龄期长度L1")]
        public virtual decimal? Air21L1
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 空中21天限制膨胀率
        /// </summary>
        [DisplayName("空中21天限制膨胀率")]
        public virtual decimal? Air21Lim
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 水中28天基准长度L0
        /// </summary>
        [DisplayName("水中28天基准长度L0")]
        public virtual decimal? Wa28L0
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 水中28天初始长度L
        /// </summary>
        [DisplayName("水中28天初始长度L")]
        public virtual decimal? Wa28L
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 水中28天龄期长度L1
        /// </summary>
        [DisplayName("水中28天龄期长度L1")]
        public virtual decimal? Wa28L1
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 水中28天限制膨胀率
        /// </summary>
        [DisplayName("水中28天限制膨胀率")]
        public virtual decimal? Wa28Lim
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 其他
        /// </summary>
        [DisplayName("其他")]
        [StringLength(128)]
        public virtual string Other
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 限制膨胀率结论
        /// </summary>
        [DisplayName("限制膨胀率结论")]
        [StringLength(256)]
        public virtual string LimCon
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 温度(3天)
        /// </summary>
        [DisplayName("温度(3天)")]
        public virtual decimal? Temp3
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 湿度(3天)%
        /// </summary>
        [DisplayName("湿度(3天)%")]
        public virtual decimal? Hum3
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 试验时间(3天)
        /// </summary>
        [DisplayName("试验时间(3天)")]
        public virtual System.DateTime? ExamTime3
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗折强度平均值(3天)
        /// </summary>
        [DisplayName("抗折强度平均值(3天)")]
        public virtual decimal? Imz3
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 强度平均值(3天)
        /// </summary>
        [DisplayName("强度平均值(3天)")]
        public virtual decimal? Str3
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压强度平均值(3天)
        /// </summary>
        [DisplayName("抗压强度平均值(3天)")]
        public virtual decimal? Imy3
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 温度(7天)
        /// </summary>
        [DisplayName("温度(7天)")]
        [StringLength(10)]
        public virtual string Temp7
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 湿度(7天)%
        /// </summary>
        [DisplayName("湿度(7天)%")]
        [StringLength(10)]
        public virtual string Hum7
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 试验时间(7天)
        /// </summary>
        [DisplayName("试验时间(7天)")]
        [StringLength(10)]
        public virtual string ExamTime7
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗折强度平均值(7天)
        /// </summary>
        [DisplayName("抗折强度平均值(7天)")]
        [StringLength(10)]
        public virtual string Imz7
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 强度平均值(7天)
        /// </summary>
        [DisplayName("强度平均值(7天)")]
        [StringLength(10)]
        public virtual string Str7
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压强度平均值(7天)
        /// </summary>
        [DisplayName("抗压强度平均值(7天)")]
        [StringLength(10)]
        public virtual string Imy7
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 温度(28天)
        /// </summary>
        [DisplayName("温度(28天)")]
        [StringLength(10)]
        public virtual string Temp28
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 湿度(28天)%
        /// </summary>
        [DisplayName("湿度(28天)%")]
        [StringLength(10)]
        public virtual string Hum28
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 试验时间(28天)
        /// </summary>
        [DisplayName("试验时间(28天)")]
        [StringLength(10)]
        public virtual string ExamTime28
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗折强度平均值(28天)
        /// </summary>
        [DisplayName("抗折强度平均值(28天)")]
        [StringLength(10)]
        public virtual string Imz28
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 强度平均值(28天)
        /// </summary>
        [DisplayName("强度平均值(28天)")]
        [StringLength(10)]
        public virtual string Str28
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压强度平均值(28天)
        /// </summary>
        [DisplayName("抗压强度平均值(28天)")]
        [StringLength(10)]
        public virtual string Imy28
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 温度(快测)
        /// </summary>
        [DisplayName("温度(快测)")]
        [StringLength(10)]
        public virtual string TempQe
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 湿度(快测)%
        /// </summary>
        [DisplayName("湿度(快测)%")]
        [StringLength(10)]
        public virtual string HumQe
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 试验时间(快测)
        /// </summary>
        [DisplayName("试验时间(快测)")]
        [StringLength(10)]
        public virtual string ExamTimeQe
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗折强度平均值(快测)
        /// </summary>
        [DisplayName("抗折强度平均值(快测)")]
        [StringLength(10)]
        public virtual string ImzQe
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 强度平均值(快测)
        /// </summary>
        [DisplayName("强度平均值(快测)")]
        [StringLength(10)]
        public virtual string StrQe
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压强度平均值(快测)
        /// </summary>
        [DisplayName("抗压强度平均值(快测)")]
        [StringLength(10)]
        public virtual string ImyQe
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 有效时间
        /// </summary>
        [DisplayName("有效时间")]
        public virtual System.DateTime? AvaTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 标准判定
        /// </summary>
        [DisplayName("标准判定")]
        [StringLength(20)]
        public virtual string StandJudge
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 审批人
        /// </summary>
        [DisplayName("审批人")]
        [StringLength(30)]
        public virtual string Assessor
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 负责人
        /// </summary>
        [DisplayName("负责人")]
        [StringLength(30)]
        public virtual string Principal
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        [DisplayName("是否启用")]
        public virtual bool IsUse
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 检测依据
        /// </summary>
        [DisplayName("检测依据")]
        [StringLength(256)]
        public virtual string ExamGist
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 检测结论
        /// </summary>
        [DisplayName("检测结论")]
        [StringLength(256)]
        public virtual string ExamResult
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual Commission Commission
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
		 
		
        #endregion
    }
}
