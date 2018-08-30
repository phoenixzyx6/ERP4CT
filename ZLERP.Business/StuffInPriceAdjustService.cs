using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.Model.ViewModels;
using ZLERP.Model;

namespace ZLERP.Business
{
    public class StuffInPriceAdjustService : ServiceBase<StuffInPriceAdjust>
    {
        internal StuffInPriceAdjustService(IUnitOfWork uow)
            : base(uow)
        {

        }
        /// <summary>
        /// 入库记录单价调整
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool StuffInPriceAdjustOper(string beginDate,string endDate)
        {
            bool issuccess = this.m_UnitOfWork.StuffInPriceAdjustRepository.StuffInPriceAdjustOper(beginDate,endDate);
            return issuccess;
        }
    }
}
