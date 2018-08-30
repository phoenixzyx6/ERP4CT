using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;

namespace ZLERP.IRepository
{
    public interface IShippingDocumentRepository : IRepositoryBase<ShippingDocument>
    {
        IList<DailyReport> ExecuteShipDoc(string StartDateTime, string EndDateTime, out int total);

        /// <summary>
        /// 运输单批量审核
        /// </summary>
        /// <param name="idstrs"></param>
        /// <param name="CurrentUserID"></param>
        /// <returns></returns>
        bool ShipDocMultiAudit(string idstrs, string CurrentUserID);
    }
}
