using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using ZLERP.Model.Enums;　
namespace ZLERP.Business
{
    public  class SysFuncService : ServiceBase<SysFunc>
    {
        public SysFuncService(IUnitOfWork uow)
            : base(uow)
        {

        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="form"></param>
        public override void Update(SysFunc entity, System.Collections.Specialized.NameValueCollection form)
        {
            base.Update(entity, form);
            //auto refresh cache
            CacheHelper.RemoveCache(CacheKeys.AllFuncs);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(SysFunc entity)
        {
            base.Delete(entity);
            //auto refresh cache
            CacheHelper.RemoveCacheByType(null);

        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override SysFunc Add(SysFunc entity)
        {
            var obj = base.Add(entity);
            //auto refresh cache
            CacheHelper.RemoveCache(CacheKeys.AllFuncs);
            return obj;
        }

        private static object lockAllFuncs = new object(); 
        /// <summary>
        /// 所有系统模块[cached]
        /// </summary>
        /// <returns></returns>
        public override IList<SysFunc> All()
        {           
            return CacheHelper.GetCacheItem<IList<SysFunc>>(CacheKeys.AllFuncs,
               lockAllFuncs,
               delegate
               {
                   IList<SysFunc> result = this.Query()
                       .OrderBy(d => d.OrderNum)
                       .OrderBy(d => d.ID)
                       .ToList();

                   return VerifyFuncs(result);
               });
            
        }
        /// <summary>
        /// 根据父节点ID返回下级节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<SysFunc> FindFuncs(string parentId)
        {
            //parentId为空
            if (string.IsNullOrEmpty(parentId))
            {
                return this.All()
                    .Where(d => (String.IsNullOrEmpty(d.ParentID) || d.ParentID == "0"))                  
                    .ToList();
            }
            else
            {
                return this.All()
                           .Where(d => d.ParentID == parentId)                   
                           .ToList();
            }
          
        }
        
    }
}

