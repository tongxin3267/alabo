using System;
using System.Linq.Expressions;
using Alabo.Domains.Entities.Core;

namespace Alabo.Domains.Query
{
    /// <summary>
    ///     Interface IPredicateQuery
    /// </summary>
    public interface IPredicateQuery<T> : IExpressionQuery<T> where T : class, IEntity
    {
        /// <summary>
        ///     初始默认值，一般为true
        /// </summary>
        bool DefaultValue { get; set; }

        /// <summary>
        ///     Ands the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        IPredicateQuery<T> And(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     或s the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        IPredicateQuery<T> Or(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     Appends the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="type">The 类型.</param>
        IPredicateQuery<T> Append(Expression<Func<T, bool>> predicate, PredicateLinkType type);

        /// <summary>
        ///     Ands the specified predicate query.
        /// </summary>
        /// <param name="predicateQuery">The predicate query.</param>
        IPredicateQuery<T> And(IPredicateQuery<T> predicateQuery);

        /// <summary>
        ///     或s the specified predicate query.
        /// </summary>
        /// <param name="predicateQuery">The predicate query.</param>
        IPredicateQuery<T> Or(IPredicateQuery<T> predicateQuery);

        /// <summary>
        ///     Appends the specified predicate query.
        /// </summary>
        /// <param name="predicateQuery">The predicate query.</param>
        /// <param name="type">The 类型.</param>
        IPredicateQuery<T> Append(IPredicateQuery<T> predicateQuery, PredicateLinkType type);

        /// <summary>
        ///     Builds the predicate expression.
        /// </summary>
        Expression<Func<T, bool>> BuildPredicateExpression();
    }
}