using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated;

namespace ZLERP.Model
{
    public class GoodsAccount : _GoodsAccount
    {
        /// <summary>
        /// 物资名称
        /// </summary>
        public virtual string GoodsName
        {
            get
            {
                return this.GoodsInfo == null ? "" : this.GoodsInfo.GoodsName;
            }
        }
    }
}
