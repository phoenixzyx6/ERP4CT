using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  调度日报表统计
    /// </summary>
    public class DailyReport
    {
        /// <summary>
        /// 任务单号
        /// </summary>
        public virtual string TaskID
        {
            get;
            set;
        }

        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual string CustName
        {
            get;
            set;
        }

        /// <summary>
        /// 工程地址
        /// </summary>
        public virtual string ProjectAddr
        {
            get;
            set;
        }

        /// <summary>
        /// 施工部位
        /// </summary>
        public virtual string ConsPos
        {
            get;
            set;
        }

        /// <summary>
        /// 工程名称
        /// </summary>
        public virtual string ProjectName
        {
            get;
            set;
        }

        /// <summary>
        /// 砼强度
        /// </summary>
        public virtual string ConStrength
        {
            get;
            set;
        }

        public virtual string BetonCount
        {
            get;
            set;
        }

        public virtual string SlurryCount
        {
            get;
            set;
        }

        /// <summary>
        /// 浇筑方式
        /// </summary>
        public virtual string CastMode
        {
            get;
            set;
        }

        public virtual string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 生产方量
        /// </summary>
        public virtual string SendCube
        {
            get;
            set;
        }

        /// <summary>
        /// 出票方量
        /// </summary>
        public virtual string Parcube
        {
            get;
            set;
        }

        public virtual string SignInCube
        {
            get;
            set;
        }

        public virtual string TransferCube
        {
            get;
            set;
        }

        public virtual string RemainCube
        {
            get;
            set;
        }

        /// <summary>
        /// 水票
        /// </summary>
        public virtual string WaterTime
        {
            get;
            set;
        }

        public virtual string OtherCube
        {
            get;
            set;
        }

        /// <summary>
        /// 本月方量
        /// </summary>
        public virtual string M_CUBE
        {
            get;
            set;
        }

        /// <summary>
        /// 本年方量
        /// </summary>
        public virtual string Y_CUME
        {
            get;
            set;
        }

        /// <summary>
        /// 本月车数
        /// </summary>
        public virtual string M_TIMES
        {
            get;
            set;
        }

        /// <summary>
        /// 本年车数
        /// </summary>
        public virtual string Y_TIMES
        {
            get;
            set;
        }

        public virtual bool IsAudit
        {
            get;
            set;
        }
    }
}