using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;
namespace ZLERP.Business
{
    public  class RemindinfoService : ServiceBase<Remindinfo>
    {
        internal RemindinfoService(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// 返回倒序第一个调度单的所有未提示的信息
        /// </summary>
        /// <returns></returns>
        public Remindinfo[] GetRemindinfo()
        {
            //查询权限


            Remindinfo r = this.Query().Where(m => m.Status == "0").OrderByDescending(m => m.DispatchID).FirstOrDefault();
            if (r == null)
            {
                r = null;
                return null;
            }
            string disID = r.DispatchID;
            Remindinfo[] objs = this.Query().Where(m => m.DispatchID == disID).ToArray();
            return objs;
        }

        public int UpdateStatus(string DispatchID)
        {
            IGenericTransaction transaction = base.m_UnitOfWork.BeginTransaction();
            try
            {
                Remindinfo[] objs = this.Query().Where(m => m.DispatchID == DispatchID).ToArray();
                //设置状态
                foreach (Remindinfo obj in objs)
                {
                    obj.Status = "1";
                    base.Update(obj);
                }
                transaction.Commit();
                return 1;
            }
            catch
            {
                transaction.Rollback();
                return 0;
            }
        }
    }
}
