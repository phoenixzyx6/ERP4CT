
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Text; 


namespace ZLERP.Model.Generated
{
    public class _ManualProduce : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(base.GetType().FullName);
            builder.Append(this.DispatchID);
            builder.Append(this.ShipDocID);
            builder.Append(this.TaskID);
            builder.Append(this.ProductLineID);
            builder.Append(this.ProductLineName);
            builder.Append(this.ProjectName);
            builder.Append(this.ConStrength);
            builder.Append(this.SendCube);
            builder.Append(this.ManualCube);
            builder.Append(this.SendPot);
            builder.Append(this.BeginPot);
            builder.Append(this.ManualPot);
            builder.Append(this.ManualReason);
            builder.Append(this.Operator);
            builder.Append(this.ProduceTime);
            builder.Append(this.Remark);
            builder.Append(this.Version);
            return builder.ToString().GetHashCode();
        }

        #endregion

        #region Properties
        [Required, DisplayName("开始罐次")]
        public virtual int BeginPot { get; set; }

        [Required, DisplayName("车号"), StringLength(30)]
        public virtual string CarID { get; set; }

        [DisplayName("砼强度"), StringLength(50), Required]
        public virtual string ConStrength { get; set; }

        [StringLength(30), DisplayName("生产登记号"), Required]
        public virtual string DispatchID { get; set; }

        [DisplayName("手动方量"), Required]
        public virtual decimal ManualCube { get; set; }

        [DisplayName("手动罐数"), Required]
        public virtual int ManualPot { get; set; }

        [DisplayName("手动原因"), StringLength(0x100), Required]
        public virtual string ManualReason { get; set; }

        [Required, DisplayName("操作员"), StringLength(30)]
        public virtual string Operator { get; set; }

        [DisplayName("生产时间"), Required]
        public virtual DateTime? ProduceTime { get; set; }

        [DisplayName("生产线"), Required, StringLength(20)]
        public virtual string ProductLineID { get; set; }

        [DisplayName("生产线"), StringLength(20)]
        public virtual string ProductLineName { get; set; }

        [DisplayName("工程名称"), StringLength(0x80), Required]
        public virtual string ProjectName { get; set; }

        [DisplayName("备注"), StringLength(0x100)]
        public virtual string Remark { get; set; }

        [Required, DisplayName("调度方量")]
        public virtual decimal SendCube { get; set; }

        [DisplayName("调度罐数"), Required]
        public virtual int SendPot { get; set; }

        [Required, DisplayName("运输单号"), StringLength(30)]
        public virtual string ShipDocID { get; set; }

        [StringLength(30), Required, DisplayName("任务单号")]
        public virtual string TaskID { get; set; }

        #endregion
    }
}
