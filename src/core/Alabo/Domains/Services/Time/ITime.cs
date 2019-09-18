using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;

namespace Alabo.Domains.Services.Time
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface ITime<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     根据时间与条件查询最后一条记录
        ///     比如：查询当月最后一条记录、周最后一条记录
        /// </summary>
        /// <param name="timeType">时间方式：周、月、小时、分、季度、年等</param>
        /// <param name="dateTime">基准时间</param>
        /// <param name="predicate">查询条件</param>
        TEntity GetLastSingle(TimeType timeType, DateTime dateTime, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     根据时间与条件查询最后一条记录
        ///     比如：查询当月最后一条记录、周最后一条记录
        /// </summary>
        /// <param name="timeType">时间方式：周、月、小时、分、季度、年等</param>
        /// <param name="predicate">查询条件</param>
        TEntity GetLastSingle(TimeType timeType, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     根据时间与条件查询最后一条记录
        ///     比如：查询当月最后一条记录、周最后一条记录
        /// </summary>
        /// <param name="timeType">时间方式：周、月、小时、分、季度、年等</param>
        /// <param name="predicate">查询条件</param>
        IEnumerable<TEntity> GetLastList(TimeType timeType, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     根据时间与条件查询最后一条记录
        ///     比如：查询当月最后一条记录、周最后一条记录
        /// </summary>
        /// <param name="timeType">时间方式：周、月、小时、分、季度、年等</param>
        /// <param name="dateTime">基准时间</param>
        /// <param name="predicate">查询条件</param>
        IEnumerable<TEntity> GetLastList(TimeType timeType, DateTime dateTime,
            Expression<Func<TEntity, bool>> predicate);
    }
}