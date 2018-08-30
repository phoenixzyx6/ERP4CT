using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 针对12版 施工配比子表
    /// </summary>
	public class ConsMixpropItemsHistory : _ConsMixpropItemsHistory
    {
        /// <summary>
        /// 筒仓名称
        /// </summary>
        public virtual string SiloName {
            get { return this.Silo == null ? "" : this.Silo.SiloName; }
        }

        /// <summary>
        /// 材料名称
        /// </summary>
        public virtual string StuffName {
            get { return this.Silo == null ? "" : this.Silo.StuffInfo.StuffName; }
        }

	}
}