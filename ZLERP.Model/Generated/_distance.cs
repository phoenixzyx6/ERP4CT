
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public abstract class _distance : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(projectid);
            sb.Append(CastModeid);
            sb.Append(distance);
            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 工程
        /// </summary>
        [ScriptIgnore]
        public virtual Project Project
        {
            get;
            set;
        }

        /// <summary>
        /// 工程ID
        /// </summary>
        [DisplayName("工程")]
        [StringLength(30)]
        [Required]
        public virtual string projectid
        {
            get;
            set;
        }

        

        /// <summary>
        /// 浇筑方式
        /// </summary>
        [ScriptIgnore]
        public virtual Dic CastMode
        {
            get;
            set;
        }

        /// <summary>
        /// 浇筑方式ID
        /// </summary>
        [DisplayName("浇筑方式")]
        [StringLength(50)]
        [Required]
        public virtual string CastModeid
        {
            get;
            set;
        }

        

        /// <summary>
        /// 距离
        /// </summary>
        [DisplayName("距离(米)")]
        [Required]
        public virtual decimal distance
        {
            get;
            set;
        }

        

        #endregion
    }
}
