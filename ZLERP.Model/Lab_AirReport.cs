using System;
using System.Collections.Generic;
using System.Text;
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 粉煤灰检测报告
    /// </summary>
    public class Lab_AirReport : _Lab_AirReport
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
        /// 生产厂家
        /// </summary>
        [DisplayName("生产厂家")]
        public virtual string SupplyName
        {
            get
            {
                return this.Lab_Record == null ? "" : this.Lab_Record.SupplyName;
            }
        }

    }
}