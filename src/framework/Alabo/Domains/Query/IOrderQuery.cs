using System;
using System.Linq.Expressions;
using Alabo.Domains.Entities.Core;

namespace Alabo.Domains.Query
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IOrderQuery<TEntity> : IPredicateQuery<TEntity> where TEntity : class, IEntity
    {
        IOrderQuery<TEntity> OrderByAscending<TKey>(Expression<Func<TEntity, TKey>> keySelector);

        IOrderQuery<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);

        IOrderQuery<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector,
            OrderType type = OrderType.Ascending);
    }
}