using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    ///  筒仓
    /// </summary>
	public class Silo : _Silo
    {
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

        [Required]
        public override string SiloName
        {
            get
            {
                return base.SiloName;
            }
            set
            {
                base.SiloName = value;
            }
        }


        /// <summary>
        /// 原料名称
        /// </summary>
        [DisplayName("原料名称")]
        public  virtual string StuffName
        {
            get { return this.StuffInfo == null ? "" : this.StuffInfo.StuffName; }
        }
        /// <summary>
        /// 原料简称
        /// </summary>
        [DisplayName("原料简称")]
        public virtual string StuffShortName
        {
            get { return this.StuffInfo == null ? "" : this.StuffInfo.StuffShortName; }
        }
   
        /// <summary>
        /// 原料类型ID
        /// </summary>
        [DisplayName("原料类型")]
        public virtual string StuffID
        {
            get;
            set;
        }

        /// <summary>
        /// 搅拌机组编号
        /// </summary>
        [DisplayName("搅拌机组编号")]
        [StringLength(20)]
        public virtual string ProductLineID
        {
            get;
            set;          
        }

	}
}