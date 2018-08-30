using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.Model.Enums;

namespace ZLERP.Business
{
    public  class RoleService : ServiceBase<Role>
    {

        internal RoleService(IUnitOfWork uow)
            : base(uow) 
        { 
        }

        /// <summary>
        /// 取得角色对应的权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<SysFunc> GetRoleFuncs(string roleId)
        {
            var role = this.Get(roleId);
            if (role != null)
            {
                var funcs = VerifyFuncs(role.SysFuncs);
                return funcs;
            }
            else
                return null;
        }
        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="powers"></param>
        public void SaveRoleFuncs(string[] roleIds, string[] powers)
        {
            var sysFuncRepo = this.m_UnitOfWork.GetRepositoryBase<SysFunc>();
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {

                    var sysFuncs = sysFuncRepo.Query()
                        .Where(f => powers.Contains(f.ID))
                        .ToList();

                    foreach (string uid in roleIds)
                    {
                        var roleInfo = this.Get(uid);
                        if (roleInfo != null)
                        {
                            roleInfo.SysFuncs.Clear();
                            roleInfo.SysFuncs = sysFuncs;
                            this.Update(roleInfo, null);
                            //刷新用户权限缓存
                            CacheHelper.RemoveCache(string.Format(CacheKeys.RoleFuncsFormatString, uid));
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

        ///// <summary>
        ///// 取得角色对应的用户
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //public IList<User> GetRoleUsers(string roleId)
        //{
        //    IList<UserRole> userroleList = this.m_UnitOfWork.GetRepositoryBase<UserRole>().Query().Where(m => m.RoleID == roleId).ToList(); 
        //    IList<User> userList=this.m_UnitOfWork.GetRepositoryBase<User>().Query().ToList();
        //    IList<User> yuserList=new List<User>();
        //    foreach(var ur in userroleList)
        //    {
        //        foreach (var u in userList)
        //        {
        //            if (ur.UserID == u.ID)
        //            {
        //                yuserList.Add(u);
        //            }
        //            //userlist.Where(p => p.ID == ur.UserID).ToList();
        //        }
        //    }
        //    return yuserList;
        //}

        
    }
}
