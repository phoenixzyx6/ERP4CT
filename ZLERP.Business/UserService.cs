using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using System.Collections.Specialized;
using ZLERP.Model.Enums;
using System.Collections;　
namespace ZLERP.Business
{
    public  class UserService : ServiceBase<User>
    {
       
        internal UserService(IUnitOfWork uow)
            : base(uow) 
        { 
        }

       // private static object sync_GetUserFuncs = new object();
        /// <summary>
        /// 取得用户所有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<SysFunc> GetUserFuncs(string userId)
        {
            //string cacheKey = string.Format(CacheKeys.UserFuncsFormatString, userId);
            //return CacheHelper.GetCacheItem<IList<SysFunc>>(cacheKey,
            //    sync_GetUserFuncs, 
            //    delegate
            //    {                    
                    var user = this.Get(userId);
                    if (user != null)
                    {
                        var funcs = VerifyFuncs(user.SysFuncs);  
                        return funcs;
                    }
                    else
                        return null;
            //    });
        }
        /// <summary>
        /// 取得当前用户buttons
        /// </summary>
        /// <param name="funcId"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public dynamic GetUserButtons(string funcId, int flag)
        {
            var buttons = this.GetUserFuncs(AuthorizationService.CurrentUserID);
            return buttons.Where(b => b.ParentID == funcId && b.IsButton && b.Flag == flag && !b.IsDisabled)
                .Select(b => new
                {
                    ID = b.ID,
                    FuncName = b.FuncName,
                    FuncDesc = b.FuncDesc,
                    HandlerName = b.HandlerName,
                    Icon = b.Icon,
                    Url = b.Urls.FirstOrDefault(),
                    ButtonPos = b.ButtonPos
                })
                 .ToList();
        }
        public override User Add(User entity)
        {
            entity.Password = "123456";
            if(!string.IsNullOrEmpty(entity.Password))
                entity.Password = AuthorizationService.EncryptPassword(entity.Password);
            return base.Add(entity);
        }

        public override void Update(User entity, NameValueCollection form)
        {
            if (!string.IsNullOrEmpty(entity.Password) && form != null && form.AllKeys.Contains("Password"))
                entity.Password = AuthorizationService.EncryptPassword(entity.Password);
            base.Update(entity,form);
        }
        /// <summary>
        /// 保存用户权限
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="powers"></param>
        public void SaveUserPowers(string[] userIds, string[] powers)
        {
           var sysFuncRepo = this.m_UnitOfWork.GetRepositoryBase<SysFunc>();
           using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {

                    var sysFuncs = sysFuncRepo.Query()
                        .Where(f => powers.Contains(f.ID))
                        .ToList();

                    foreach (string uid in userIds)
                    {
                        var userInfo = this.Get(uid);
                        if (userInfo != null) {
                            userInfo.SysFuncs.Clear();
                            userInfo.SysFuncs = sysFuncs;
                            this.Update(userInfo, null);
                            //刷新用户权限缓存
                            CacheHelper.RemoveCache(string.Format(CacheKeys.UserFuncsFormatString, uid));
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

        public void CopyPower(string userId, string copyFrom)
        {
            User user = this.Get(userId);
            if (user != null) {
                User copyUser = this.Get(copyFrom);
                if (copyUser != null) {

                    using (var tx = this.m_UnitOfWork.BeginTransaction())
                    {
                        try
                        {
                            user.SysFuncs.Clear();
                            foreach (var f in copyUser.SysFuncs)
                            {
                                user.SysFuncs.Add(f);
                            }
                            this.Update(user, null);
                            tx.Commit();
                        }
                        catch (Exception ex) {
                            tx.Rollback();
                            logger.Error(ex.Message, ex);
                            throw ex;
                        }
                    }
                }
            
            }
        }

        /// <summary>
        /// 新增司机信息
        /// </summary>
        /// <param name="name">司机姓名</param>
        /// <returns></returns>
        public User AddDrive(User entity)
        {
            if (entity.DepartmentID == 0)
            {
                throw new Exception("请选择所属部门");
            }
            User user = new User();
            user.ID = entity.ID;
            user.TrueName = entity.ID;
            user.IsUsed = true;
            user.DepartmentID = entity.DepartmentID;
            user.UserType = UserType.Driver;
            return this.Add(user);
        }

        /// <summary>
        /// 获取用户权限菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<SysFunc> GetUserFuncs2(string userId)
        {
            PublicService ps = new PublicService();
            //获取用户角色关系
            IList<UserRole> urolelist = ps.GetGenericService<UserRole>().Query().Where(p => p.UserID == userId).ToList();
            ArrayList arry = new ArrayList();
            foreach (UserRole u in urolelist)
            {
                arry.Add(u.RoleID);
            }
            //获取角色列表
            IList<Role> rolelist = ps.GetGenericService<Role>().Query().Where(p => arry.Contains(p.ID)).ToList();
            ArrayList arry2 = new ArrayList();
            foreach (Role u in rolelist)
            {
                arry2.Add(u.ID);
            }
            //获取菜单角色关系
            IList<FuncRole> froleList = ps.GetGenericService<FuncRole>().Query().Where(p => arry2.Contains(p.RoleID)).OrderBy(p => p.SysFuncId).ToList();
            ArrayList arry3 = new ArrayList();
            foreach (FuncRole u in froleList)
            {
                if (!arry3.Contains(u.SysFuncId))
                {
                    arry3.Add(u.SysFuncId);
                }
            }
            IList<SysFunc> sysfuncList = ps.GetGenericService<SysFunc>().Query().Where(p => p.IsDisabled == false && arry3.Contains(p.ID)).OrderBy(p => p.OrderNum).ThenBy(p => p.ID).ToList();

            return sysfuncList;

        }
    }
}

