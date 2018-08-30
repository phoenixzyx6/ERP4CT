using System;
using System.Collections.Generic;
using System.Text;
using ZLERP.Model.Generated;
namespace ZLERP.Model
{
    /// <summary>
    /// 验收取样记录
    /// </summary>
    public class Lab_Record : _Lab_Record
    {
        public virtual string StuffName
        {
            get
            {
                return this.StuffInfo == null ? "" : this.StuffInfo.StuffName;
            }
        }
        public virtual string SupplyName
        {
            get
            {
                return this.SupplyInfo == null ? "" : this.SupplyInfo.SupplyName;
            }
        }
        public virtual string SiloName
        {
            get
            {
                return this.Silo == null ? "" : this.Silo.SiloName;
            }
        }

        #region 粉煤灰实验
        /// <summary>
        /// 粉煤灰-检测报告
        /// </summary>
        public virtual Lab_AirReport Lab_AirReport
        {
            get;
            set;
        }
        /// <summary>
        /// 粉煤灰-检测原始记录
        /// </summary>
        public virtual Lab_AirOrigin Lab_AirOrigin
        {
            get;
            set;
        }
        /// <summary>
        /// 粉煤灰-烧失量
        /// </summary>
        public virtual Lab_BurntInfo Lab_BurntInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 粉煤灰-活性指数
        /// </summary>
        public virtual Lab_ActiveInfo Lab_ActiveInfo
        {
            get;
            set;
        }
        #endregion

        #region 矿粉实验
        /// <summary>
        /// 检测原始记录
        /// </summary>
        public virtual Lab_Air2Origin Lab_Air2Origin
        {
            get;
            set;
        }
        /// <summary>
        /// 密度
        /// </summary>
        public virtual Lab_Air2Density Lab_Air2Density
        {
            get;
            set;
        }
        /// <summary>
        /// 活性指数
        /// </summary>
        public virtual Lab_Air2ActiveInfo Lab_Air2ActiveInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 检测报告
        /// </summary>
        public virtual Lab_Air2Report Lab_Air2Report
        {
            get;
            set;
        }
        #endregion

        public virtual Lab_ADM Lab_ADM
        {
            get;
            set;
        }
        public virtual Lab_ADM_Items Lab_ADM_Items
        {
            get;
            set;
        }
        public virtual Lab_FA Lab_FA
        {
            get;
            set;
        }
        public virtual Lab_FA_Items Lab_FA_Items
        {
            get;
            set;
        }
        public virtual Lab_CA Lab_CA
        {
            get;
            set;
        }
        public virtual Lab_CA_Items Lab_CA_Items
        {
            get;
            set;
        }
       
    }
}