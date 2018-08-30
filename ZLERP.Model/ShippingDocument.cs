using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model
{
    /// <summary>
    ///  运输单
    /// </summary>
	public class ShippingDocument : _ShippingDocument
    {

        [DisplayName("施工部位")]
        public override string ConsPos
        {
            get
            {
                return base.ConsPos;
            }
            set
            {
                base.ConsPos = value;
            }
        }

        //comment by:Sky
        //此处代码由邓海波注释，如无特别原因（某功能不能使用）
        //请不要恢复该代码，该处严重影响发货单页面效率，多出很多不必要的关联查询
        //date: 2012-12-10
        ///// <summary>
        ///// 生产任务
        ///// </summary>
        public virtual ProduceTask ProduceTask
        {
            get;
            set;
        }

        [DisplayName("小票票号")]
        public virtual string TicketNO
        {
            get;
            set;
        }

        [DisplayName("异常信息")]
        public virtual string ExceptionInfo
        {
            get;
            set;
        }

        [DisplayName("理论配比名称")]
        public virtual string FormulaName
        {
            get;
            set;
        }

	}
}