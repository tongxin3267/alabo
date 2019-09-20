using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alabo.Domains.Entities.Core;
using Alabo.Linq.Expressions;

namespace Alabo.Domains.Query
{
    /// <summary>
    ///     Class ExpressionPredicateQuery.
    /// </summary>
    internal class ExpressionPredicateQuery<TEntity> : IPredicateQuery<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        ///     The predicate list
        /// </summary>
        private readonly IList<PredicateQueryItem> _predicateList = new List<PredicateQueryItem>();

        /// <summary>
        ///     初始默认值，一般为true
        /// </summary>
        public bool DefaultValue { get; set; } = true;

        /// <summary>
        ///     Ands the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        public IPredicateQuery<TEntity> And(Expression<Func<TEntity, bool>> predicate)
        {
            return Append(predicate, PredicateLinkType.And);
        }

        /// <summary>
        ///     或s the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        public IPredicateQuery<TEntity> Or(Expression<Func<TEntity, bool>> predicate)
        {
            return Append(predicate, PredicateLinkType.Or);
        }

        /// <summary>
        ///     Appends the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="type">The 类型.</param>
        public IPredicateQuery<TEntity> Append(Expression<Func<TEntity, bool>> predicate, PredicateLinkType type)
        {
            if (predicate == null) {
                throw new ArgumentNullException(nameof(predicate));
            }

            _predicateList.Add(new PredicateQueryItem(predicate, type));
            return this;
        }

        /// <summary>
        ///     Ands the specified predicate query.
        /// </summary>
        /// <param name="predicateQuery">The predicate query.</param>
        public IPredicateQuery<TEntity> And(IPredicateQuery<TEntity> predicateQuery)
        {
            return Append(predicateQuery, PredicateLinkType.And);
        }

        /// <summary>
        ///     或s the specified predicate query.
        /// </summary>
        /// <param name="predicateQuery">The predicate query.</param>
        public IPredicateQuery<TEntity> Or(IPredicateQuery<TEntity> predicateQuery)
        {
            return Append(predicateQuery, PredicateLinkType.Or);
        }

        /// <summary>
        ///     Appends the specified predicate query.
        /// </summary>
        /// <param name="predicateQuery">The predicate query.</param>
        /// <param name="type">The 类型.</param>
        public IPredicateQuery<TEntity> Append(IPredicateQuery<TEntity> predicateQuery, PredicateLinkType type)
        {
            if (predicateQuery == null) {
                throw new ArgumentNullException(nameof(predicateQuery));
            }

            return Append(predicateQuery.BuildPredicateExpression(), type);
        }

        /// <summary>
        ///     执行查询
        /// </summary>
        /// <param name="query">查询</param>
        public virtual IQueryable<TEntity> Execute(IQueryable<TEntity> query)
        {
            var predicateExpression = BuildPredicateExpression();
            if (predicateExpression == null) {
                return query;
            }

            return query.Where(predicateExpression);
        }

        /// <summary>
        ///     Builds the predicate expression.
        /// </summary>
        public Expression<Func<TEntity, bool>> BuildPredicateExpression()
        {
            if (_predicateList.Count <= 0) {
                return null;
            }

            var parameterExpression = Expression.Parameter(typeof(TEntity), "e");
            Expression predicateExrpession = Expression.Constant(DefaultValue);
            foreach (var item in _predicateList)
            {
                var visitor = new ChangeParameterTypeExpressionVisitor(item.Predicate.Body, parameterExpression);
                if (item.Type == PredicateLinkType.And) {
                    predicateExrpession = Expression.AndAlso(predicateExrpession, visitor.Convert());
                } else {
                    predicateExrpession = Expression.OrElse(predicateExrpession, visitor.Convert());
                }
            }

            return Expression.Lambda<Func<TEntity, bool>>(predicateExrpession, parameterExpression);
        }

        /// <summary>
        ///     Builds the predicate expression.
        ///     构建Linq表达式
        /// </summary>
        public Expression<Func<TEntity, bool>> BuildExpression()
        {
            var predicateExpression = BuildPredicateExpression();
            return predicateExpression;
        }

        /// <summary>
        ///     Class PredicateQueryItem.
        /// </summary>
        private class PredicateQueryItem
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="PredicateQueryItem" /> class.
            /// </summary>
            /// <param name="predicate">The predicate.</param>
            /// <param name="type">The 类型.</param>
            public PredicateQueryItem(Expression<Func<TEntity, bool>> predicate, PredicateLinkType type)
            {
                Predicate = predicate;
                Type = type;
            }

            /// <summary>
            ///     Gets or sets the predicate.
            /// </summary>
            public Expression<Func<TEntity, bool>> Predicate { get; }

            /// <summary>
            ///     Gets or sets the 类型.
            /// </summary>
            public PredicateLinkType Type { get; }
        }
    }
}