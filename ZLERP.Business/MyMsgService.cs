using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;

namespace ZLERP.Business
{
    public class MyMsgService : ServiceBase<MyMsg>
    {
        internal MyMsgService(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// 标记为已读
        /// </summary>
        /// <param name="id">消息编号</param>
        /// <returns></returns>
        public bool SetRead(Int32 id, bool isRead = true)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    MyMsg mm = this.Get(id);
                    mm.IsRead = isRead;
                    base.Update(mm, null);
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 标记为已读（多个）
        /// </summary>
        /// <param name="ids">消息编号数组</param>
        /// <returns></returns>
        public bool toReaded(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    this.SetRead(int.Parse(id));
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 标记为未读（多个）
        /// </summary>
        /// <param name="ids">消息编号数组</param>
        /// <returns></returns>
        public bool toUnRead(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    this.SetRead(int.Parse(id), false);
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 移至回收站（单个）
        /// </summary>
        /// <param name="id">消息编号</param>
        /// <returns></returns>
        public bool Trash(Int32 id)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    MyMsg mm = this.Get(id);
                    mm.DealStatus = -1;
                    base.Update(mm, null);
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 移至回收站（批量）
        /// </summary>
        /// <param name="ids">消息编号数组</param>
        /// <returns></returns>
        public bool Trash(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    this.Trash(int.Parse(id));
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 清空回收站
        /// </summary>
        /// <returns></returns>
        public bool CleanTrash()
        {
            try
            {
                IList<MyMsg> list = this.Query().Where(m => m.DealStatus == -1 && m.UserID == AuthorizationService.CurrentUserID).ToList();
                foreach (MyMsg mm in list)
                {
                    this.Delete(mm);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
