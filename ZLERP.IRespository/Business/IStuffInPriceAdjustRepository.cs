using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.ViewModels;
using ZLERP.Model;

namespace ZLERP.IRepository
{
    public interface IStuffInPriceAdjustRepository : IRepositoryBase<StuffInPriceAdjust>
    {
        /// <summary>
        /// 入库记录单价调整
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        bool StuffInPriceAdjustOper(string beginDate,string endDate);
    }
}
