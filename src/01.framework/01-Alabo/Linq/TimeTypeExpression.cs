using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using System;
using System.Linq.Expressions;

namespace Alabo.Linq
{
    /// <summary>
    ///     时间方式Lamada表达式
    /// </summary>
    public static class TimeTypeExpression
    {
        /// <summary>
        ///     获取时间方式的表达式
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="timeType"></param>
        /// <param name="dateTime"></param>
        /// <param name="predicate"></param>
        public static Expression<Func<TEntity, bool>> GetPredicate<TEntity, TKey>(this TimeType timeType,
            DateTime dateTime, Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, IAggregateRoot<TEntity, TKey>
        {
            var query = new ExpressionQuery<TEntity>();
            if (predicate != null) {
                query.And(predicate);
            }

            query.OrderByAscending(e => e.Id);
            switch (timeType)
            {
                case TimeType.Day:
                    query.And(r =>
                        r.CreateTime.Day == dateTime.Day && r.CreateTime.Month == dateTime.Month &&
                        r.CreateTime.Year == dateTime.Year);
                    break;

                case TimeType.Month:
                    query.And(r => r.CreateTime.Month == dateTime.Month && r.CreateTime.Year == dateTime.Year);
                    break;

                case TimeType.Year:
                    query.And(r => r.CreateTime.Year == dateTime.Year);
                    break;
            }

            var result = query.BuildExpression();
            return result;
        }
    }
}