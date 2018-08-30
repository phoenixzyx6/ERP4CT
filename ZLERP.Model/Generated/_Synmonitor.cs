
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  车辆信息抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Synmonitor : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(tb_name);
            sb.Append(tb_action);
            sb.Append(key_name);
            sb.Append(key_value);
            sb.Append(key_type);
            sb.Append(ProductLineID);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties


        [StringLength(30)]
        public virtual string tb_name
        {
            get;
			set;			 
        }

        [StringLength(10)]
        public virtual string tb_action
        {
            get;
            set;
        }

        [StringLength(50)]
        public virtual string key_name
        {
            get;
            set;
        }

        [StringLength(100)]
        public virtual string key_value
        {
            get;
            set;
        }

        [StringLength(1)]
        public virtual string key_type
        {
            get;
			set;			 
        }

        public virtual DateTime action_time
        {
            get;
			set;			 
        }

        [StringLength(50)]
        public virtual string ProductLineID
        {
            get;
			set;			 
        
        }
        #endregion
    }
}
