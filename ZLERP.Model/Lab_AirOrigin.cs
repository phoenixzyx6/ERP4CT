using System;
using System.Collections.Generic;
using System.Text;
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 粉煤灰检测原始记录
    /// </summary>
    public class Lab_AirOrigin : _Lab_AirOrigin
    {
        /// <summary>
        /// 样品编号
        /// </summary>
        [DisplayName("样品编号")]
        public virtual string ShowpeieNo
        {
            get
            {
                return this.Lab_Record == null ? "" : this.Lab_Record.ShowpeieNo;
            }
        }

        /// <summary>
        /// 煤灰品种
        /// </summary>
        [DisplayName("煤灰品种")]
        public virtual string StuffName
        {
            get
            {
                return this.Lab_Record == null ? "" : this.Lab_Record.StuffName;
            }
        }
    }
}