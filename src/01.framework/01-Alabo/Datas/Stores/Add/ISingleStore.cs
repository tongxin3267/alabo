using Alabo.Domains.Entities.Core;
using Alabo.Domains.Query;
using System;
using System.Linq.Expressions;

namespace Alabo.Datas.Stores.Add
{
    /// <summary>
    ///     查找单个实体
    /// </summary>
    /// <typeparam name="TEntity">对象类型</typeparam>
    /// <typeparam name="TKey">对象标识类型</typeparam>
    public interface ISingleStore<TEntity, TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        /// <summary>
        ///     根据Id 查找实体
        /// </summary>
        /// <param name="id">实体标识</param>
        TEntity GetSingle(object id);

        /// <summary>
        ///     查找实体
        /// </summary>
        /// <param name="query">查询条件</param>
        TEntity GetSingle(IExpressionQuery<TEntity> query);

        /// <summary>
        ///     获取默认的一条数据
        /// </summary>
        TEntity FirstOrDefault();

        /// <summary>
        ///     获取最后默认的一条数据
        /// </summary>
        TEntity LastOrDefault();

        /// <summary>
        ///     查询单条记录
        /// </summary>
        /// <param name="predicate">查询条件</param>
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     升序条件查询
        /// </summary>
        /// <param name="keySelector">排序字段</param>
        TEntity GetSingleOrderBy(Expression<Func<TEntity, TKey>> keySelector);

        /// <summary>
        ///     升序条件查询
        /// </summary>
        /// <param name="keySelector">排序字段</param>
        /// <param name="predicate">查询条件</param>
        TEntity GetSingleOrderBy(Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     降序条件查询
        /// </summary>
        /// <param name="keySelector">排序字段</param>
        TEntity GetSingleOrderByDescending(Expression<Func<TEntity, TKey>> keySelector);

        /// <summary>
        ///     降序条件查询
        /// </summary>
        /// <param name="keySelector">排序字段</param>
        /// <param name="predicate">查询条件</param>
        TEntity GetSingleOrderByDescending(Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TEntity, bool>> predicate);
    }
}