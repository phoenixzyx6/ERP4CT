using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace ZLERP.IRepository
{
    public interface IRepositoryBase<TEntity>
    {
        /// <summary>
        /// 取得单条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(object id);
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="entity"></param>
        TEntity Add(TEntity entity);
        /// <summary>
        /// 更新对象，
        /// </summary>
        /// <param name="entity">对象</param>
        /// <param name="form">客户端提交的Form对象，用以判断哪些字段需要更新</param>
        void Update(TEntity entity, NameValueCollection form);
        /// <summary>
        /// 更新对象，注意此方法不能处理实际上未更新的字段也被更新的问题
        /// 使用Get方法从数据库取值后再修改部份字段使用该方法
        /// </summary>
        /// <param name="entity"></param>
        [Obsolete("不建议使用此方法，请使用Update(TEntity entity, NameValueCollection form)替代,明确需要使用何种更新")]
        void Update(TEntity entity);
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
 
        /// <summary>
        /// 所有记录
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IList<TEntity> All();
        /// <summary>
        /// 按指定字段排序的所有记录
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="orderBy"></param>
        /// <param name="asceding"></param>
        /// <returns></returns>
        IList<TEntity> All(string orderBy, bool asceding);

        /// <summary>
        /// 查询所有的记录
        /// </summary>
        /// <param name="columnNames"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        IList<TEntity> All(IList<string> columnNames, string orderBy, bool ascending);

        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        IList<TEntity> All(string condition, string orderBy, bool ascending);

        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <param name="columnNames"></param>
        /// <param name="condition"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        IList<TEntity> All(IList<string> columnNames, string condition, string orderBy, bool ascending);
         /// <summary>
         /// 查询对应的实体
         /// </summary>
         /// <returns></returns>
        IQueryable<TEntity> Query();
        /// <summary>
        /// 根据condition查询对应的实体,　分页
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDir"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        IList<TEntity> Find(string condition, int page, int pageSize, string orderBy, string orderDir, out int total);

        /// <summary>
        /// 根据condition查询对应的实体,　分页,不返回总数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDir"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        IList<TEntity> Find(string condition, int page, int pageSize, string orderBy, string orderDir);

        /// <summary>
        /// 根据condition查询对应的实体,　分页
        /// </summary>
        /// <param name="condition">Example实体对象</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDir"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        IList<TEntity> Find(TEntity condition, int page, int pageSize, string orderBy, string orderDir, out int total);

        /// <summary>
        /// 根据condition查询对应的实体,　分页
        /// </summary>
        /// <param name="columnNames">列名</param>
        /// <param name="condition">Example实体对象</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDir"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        IList<TEntity> Find(IList<string> columnNames,TEntity condition, int page, int pageSize, string orderBy, string orderDir, out int total);
    }
}
