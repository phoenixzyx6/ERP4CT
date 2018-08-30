using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated;

namespace ZLERP.Model
{
    public class StuffAccount : _StuffAccount
    {
        /// <summary>
        /// 材料名称
        /// </summary>
        public virtual string StuffName
        {
            get
            {
                return this.StuffInfo == null ? "" : this.StuffInfo.StuffName;
            }
        }
        /// <summary>
        /// 筒仓名称
        /// </summary>
        public virtual string SiloName
        {
            get
            {
                return this.Silo == null ? "" : this.Silo.SiloName;
            }
        }
    }
}
