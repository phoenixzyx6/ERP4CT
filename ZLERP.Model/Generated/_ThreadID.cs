
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
    public abstract class _ThreadID : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(typeID);
            sb.Append(typename);
            sb.Append(currentDate);
            sb.Append(remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties


        [StringLength(50)]
        public virtual string typeID
        {
            get;
            set;
        }

        [StringLength(50)]
        public virtual string typename
        {
            get;
            set;
        }

        public virtual DateTime? currentDate
        {
            get;
            set;
        }

        [StringLength(50)]
        public virtual string remark
        {
            get;
            set;
        }	 
		
        #endregion
    }
}
