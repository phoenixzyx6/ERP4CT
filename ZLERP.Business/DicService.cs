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
    public  class DicService : ServiceBase<Dic>
    {
        internal DicService(IUnitOfWork uow)
            : base(uow) 
        { 
        }
        

        /// <summary>
        /// 计算Dic实体的Deep属性
        /// </summary>
        /// <param name="entity"></param>
        void CalcEntityDeep(Dic entity)
        {
            entity.Deep = 0;
            if (!string.IsNullOrEmpty(entity.ParentID))
            {
                Dic parent = this.Get(entity.ParentID);
                if (parent != null)
                    entity.Deep = parent.Deep + 1;
            }
        }
        public override Dic Add(Dic entity)
        {
            CalcEntityDeep(entity);
            var ret = base.Add(entity);
            //刷新缓存
            CacheHelper.RemoveCache(CacheKeys.AllDics);
            return ret;
        }
        public override void Update(Dic entity, NameValueCollection form)
        {
            CalcEntityDeep(entity);
            base.Update(entity, form);
            //刷新缓存
            CacheHelper.RemoveCache(CacheKeys.AllDics);
        }
        public override void Delete(Dic entity)
        {
            base.Delete(entity);
            //刷新缓存
            CacheHelper.RemoveCache(CacheKeys.AllDics);
        }
        private static object lockAllDics = new object(); 
        /// <summary>
        /// 所有字典项[cached]
        /// </summary>
        /// <returns></returns>
        public override IList<Dic> All()
        {
            return CacheHelper.GetCacheItem<IList<Dic>>(CacheKeys.AllDics,
                lockAllDics,
                delegate
                {
                    return this.Query()
                        .OrderBy(d => d.OrderNum)
                        .OrderBy(d => d.ID)
                        .ToList();
                });

        }
        
        /// <summary>
        /// 根据父节点ID返回下级节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<Dic> FindDics(string parentId)
        {
            if (string.IsNullOrEmpty(parentId))
            {
                return this.All()
                    .Where(d => (d.ParentID == "" || d.ParentID == null || d.ParentID == "0"))
                    .ToList();

                //return base.Query()
                //    .Where(d => (d.ParentID == "" || d.ParentID == null || d.ParentID == "0"))
                //    .OrderBy(d=>d.OrderNum)
                //    .OrderBy(d=>d.ID)
                //    .ToList();
            }
            else
            {
                return this.All()
                    .Where(d => d.ParentID == parentId)
                    .ToList();
                //return base.Query().Where(d => d.ParentID == parentId)
                //    .OrderBy(d => d.OrderNum)
                //    .OrderBy(d => d.ID)
                //    .ToList();
            }
        }
        

    }
}

