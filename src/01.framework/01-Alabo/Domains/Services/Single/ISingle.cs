using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using System;
using System.Linq.Expressions;

namespace Alabo.Domains.Services.Single
{
    /// <summary>
    ///     获取指定当个实体
    /// </summary>
    public interface ISingle<TEntity, TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     查找实体
        /// </summary>
        /// <param name="id">实体标识</param>
        TEntity GetSingle(object id);

        /// <summary>
        ///     查询单条记录
        /// </summary>
        /// <param name="predicate">查询条件</param>
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     查找实体
        /// </summary>
        /// <param name="query">查询条件</param>
        TEntity GetSingle(IExpressionQuery<TEntity> query);

        /// <summary>
        ///     根据字段属性，和字段属性值。动态查询单条记录
        /// </summary>
        /// <param name="property">字段属性</param>
        /// <param name="value">字段属性值</param>
        /// <returns></returns>
        TEntity GetSingle(string property, object value);

        /// <summary>
        ///     获取默认的一条数据
        /// </summary>
        TEntity FirstOrDefault();

        /// <summary>
        ///     获取最后默认的一条数据
        /// </summary>
        TEntity LastOrDefault();

        /// <summary>
        ///     下一条记录
        ///     如果不存在返回为null
        /// </summary>
        /// <param name="model">The model.</param>
        TEntity Next(TEntity model);

        /// <summary>
        ///     上一条记录，如果不存在返回为空
        /// </summary>
        /// <param name="model">The model.</param>
        TEntity Prex(TEntity model);

        /// <summary>
        ///     下一条记录
        ///     如果不存在返回为null
        /// </summary>
        TEntity Next(object id);

        /// <summary>
        ///     上一条记录，如果不存在返回为空
        /// </summary>
        TEntity Prex(object id);

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