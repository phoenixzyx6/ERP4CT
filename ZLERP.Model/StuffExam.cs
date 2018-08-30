using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  试验编号
    /// </summary>
    public class StuffExam : _StuffExam
    {
        [Required]
        [DisplayName("原料名称")]
        [StringLength(30)]
        public virtual string StuffID
        {
            get;
            set;
        }

        /// <summary>
        /// 技术参数
        /// </summary>
        [Required]
        [DisplayName("技术参数")]
        [StringLength(50)]
        public virtual string TechnicalParam {
            get;
            set;
        }
        /// <summary>
        /// 进场时间
        /// </summary>
        [DisplayName("进场时间")]
        public virtual DateTime? InDate
        {
            get;
            set;
        }

        [DisplayName("试验编号")]
        [StringLength(30)]
        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }

        public virtual string StuffName
        {
            get { return this.StuffInfo == null ? "" : this.StuffInfo.StuffName; }
        }
        public virtual string StuffTypeID {
            get { return this.StuffInfo == null ? "" : this.StuffInfo.StuffTypeID; }
        }
        public virtual string StuffTypeName
        {
            get { return this.StuffInfo.StuffType == null ? "" : this.StuffInfo.StuffType.StuffTypeName; }
        }
    }
}