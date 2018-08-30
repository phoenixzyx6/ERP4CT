using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using System.Collections.Specialized;
using ZLERP.Model.Enums;　

namespace ZLERP.Business
{
    public class CarHistoryService : ServiceBase<CarHistory>
    {
        internal CarHistoryService(IUnitOfWork uow)
            : base(uow) 
        { 
            
        }

        public override CarHistory Add(CarHistory entity)
        {
            var ret = base.Add(entity);
            return ret;
        }

        public CarHistory GetLastData(string carID)
        {
            CarHistory obj = this.Query().Where(m => m.CarID == carID).OrderByDescending(m=>m.BuildTime).FirstOrDefault();
            return obj;
        }

        public string[] GetHistoryIDByCarID(string carID)
        {
            try
            {
                string[] IDS = this.Query().Where(m => m.CarID == carID).Select(g => g.ID).ToList().ToArray();
                if (IDS == null || IDS.Length == 0)
                    return null;
                else
                {
                    return IDS;
                }
            }
            catch 
            {
                return null;
            }
        }
    }
}
