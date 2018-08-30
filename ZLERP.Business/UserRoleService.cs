using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;

namespace ZLERP.Business
{
    public  class UserRoleService : ServiceBase<UserRole>
    {

        internal UserRoleService(IUnitOfWork uow)
            : base(uow) 
        { 
        }

        /// <summary>
        /// 保存角色[用户]
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="powers"></param>
        public void SaveRoleUsers(string roleId, string[] ids)
        {
            
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    foreach (var id in ids)
                    {
                        UserRole urole = new UserRole();
                        urole.RoleID = roleId;
                        urole.UserID = id;
                        if (urole != null)
                        {
                            this.Add(urole);
                            //this.m_UnitOfWork.GetRepositoryBase<UserRole>().Add(urole);
                            //this.m_UnitOfWork.Flush();
                        }
                    }
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message, ex);
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 移除角色[用户]
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="powers"></param>
        public void RemoveRoleUsers(string[] ids)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    foreach (var id in ids)
                    {
                        var urole = this.Get(Convert.ToInt32(id));
                        if (urole != null)
                        {
                            this.Delete(urole);
                            //this.m_UnitOfWork.GetRepositoryBase<UserRole>().Add(urole);
                            //this.m_UnitOfWork.Flush();
                        }
                    }
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message, ex);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 保存用户[角色]
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="powers"></param>
        public void SaveUserRoles(string userId, string[] ids)
        {

            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    foreach (var id in ids)
                    {
                        UserRole urole = new UserRole();
                        urole.UserID = userId;
                        urole.RoleID = id;
                        if (urole != null)
                        {
                            this.Add(urole);
                            //this.m_UnitOfWork.GetRepositoryBase<UserRole>().Add(urole);
                            //this.m_UnitOfWork.Flush();
                        }
                    }
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message, ex);
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 移除用户[角色]
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="powers"></param>
        public void RemoveUserRoles(string[] ids)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    foreach (var id in ids)
                    {
                        var urole = this.Get(Convert.ToInt32(id));
                        if (urole != null)
                        {
                            this.Delete(urole);
                            //this.m_UnitOfWork.GetRepositoryBase<UserRole>().Add(urole);
                            //this.m_UnitOfWork.Flush();
                        }
                    }
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message, ex);
                    throw ex;
                }
            }
        }
    }
}
