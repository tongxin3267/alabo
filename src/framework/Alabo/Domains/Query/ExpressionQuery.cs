using System;
using System.Linq;
using System.Linq.Expressions;
using Alabo.Domains.Entities.Core;

namespace Alabo.Domains.Query
{
    /// <summary>
    ///     Class ExpressionQuery. This class cannot be inherited.
    /// </summary>
    public sealed class ExpressionQuery<TEntity> : IPredicateQuery<TEntity>, IOrderQuery<TEntity>, IPageQuery<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        ///     The order query
        /// </summary>
        private readonly IOrderQuery<TEntity> _orderQuery = new ExpressionOrderQuery<TEntity>();

        /// <summary>
        ///     The 分页 query
        /// </summary>
        private readonly IPageQuery<TEntity> _pageQuery = new ExpressionPageQuery<TEntity>();

        /// <summary>
        ///     The predicate query
        /// </summary>
        private readonly IPredicateQuery<TEntity> _predicateQuery = new ExpressionPredicateQuery<TEntity>();

        /// <summary>
        ///     Orders the by ascending.
        /// </summary>
        /// <param name="keySelector">The key selector.</param>
        public IOrderQuery<TEntity> OrderByAscending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return _orderQuery.OrderByAscending(keySelector);
        }

        /// <summary>
        ///     Orders the by descending.
        /// </summary>
        /// <param name="keySelector">The key selector.</param>
        public IOrderQuery<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return _orderQuery.OrderByDescending(keySelector);
        }

        /// <summary>
        ///     Orders the by.
        /// </summary>
        /// <param name="keySelector">The key selector.</param>
        /// <param name="type">The 类型.</param>
        public IOrderQuery<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector,
            OrderType type = OrderType.Ascending)
        {
            return _orderQuery.OrderBy(keySelector, type);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [enable paging].
        /// </summary>
        public bool EnablePaging
        {
            get => _pageQuery.EnablePaging;
            set => _pageQuery.EnablePaging = value;
        }

        /// <summary>
        ///     Gets or sets the index of the 分页.
        /// </summary>
        public int PageIndex
        {
            get => _pageQuery.PageIndex;
            set => _pageQuery.PageIndex = value;
        }

        /// <summary>
        ///     Gets or sets the size of the 分页.
        /// </summary>
        public int PageSize
        {
            get => _pageQuery.PageSize;
            set => _pageQuery.PageSize = value;
        }

        /// <summary>
        ///     Executes the count query.
        /// </summary>
        /// <param name="query">查询</param>
        public int ExecuteCountQuery(IQueryable<TEntity> query)
        {
            if (query == null) {
                return 0;
            }

            return _predicateQuery.Execute(query).Count();
        }

        /// <summary>
        ///     初始默认值，一般为true
        /// </summary>
        public bool DefaultValue
        {
            get => _predicateQuery.DefaultValue;
            set => _predicateQuery.DefaultValue = value;
        }

        /// <summary>
        ///     Ands the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        public IPredicateQuery<TEntity> And(Expression<Func<TEntity, bool>> predicate)
        {
            return _predicateQuery.And(predicate);
        }

        /// <summary>
        ///     或s the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        public IPredicateQuery<TEntity> Or(Expression<Func<TEntity, bool>> predicate)
        {
            return _predicateQuery.Or(predicate);
        }

        /// <summary>
        ///     Appends the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="type">The 类型.</param>
        public IPredicateQuery<TEntity> Append(Expression<Func<TEntity, bool>> predicate, PredicateLinkType type)
        {
            return _predicateQuery.Append(predicate, type);
        }

        /// <summary>
        ///     Ands the specified predicate query.
        /// </summary>
        /// <param name="predicateQuery">The predicate query.</param>
        public IPredicateQuery<TEntity> And(IPredicateQuery<TEntity> predicateQuery)
        {
            return _predicateQuery.And(predicateQuery);
        }

        /// <summary>
        ///     或s the specified predicate query.
        /// </summary>
        /// <param name="predicateQuery">The predicate query.</param>
        public IPredicateQuery<TEntity> Or(IPredicateQuery<TEntity> predicateQuery)
        {
            return _predicateQuery.Or(predicateQuery);
        }

        /// <summary>
        ///     Appends the specified predicate query.
        /// </summary>
        /// <param name="predicateQuery">The predicate query.</param>
        /// <param name="type">The 类型.</param>
        public IPredicateQuery<TEntity> Append(IPredicateQuery<TEntity> predicateQuery, PredicateLinkType type)
        {
            return _predicateQuery.Append(predicateQuery, type);
        }

        /// <summary>
        ///     Builds the predicate expression.
        /// </summary>
        public Expression<Func<TEntity, bool>> BuildPredicateExpression()
        {
            return _predicateQuery.BuildPredicateExpression();
        }

        /// <summary>
        ///     执行查询
        /// </summary>
        /// <param name="query">查询</param>
        public IQueryable<TEntity> Execute(IQueryable<TEntity> query)
        {
            if (query == null) {
                return query;
            }

            query = _predicateQuery.Execute(query);
            query = _orderQuery.Execute(query);
            query = _pageQuery.Execute(query);
            return query;
        }

        /// <summary>
        ///     Builds the predicate expression.
        ///     构建Linq表达式
        /// </summary>
        public Expression<Func<TEntity, bool>> BuildExpression()
        {
            return _predicateQuery.BuildPredicateExpression();
        }
    }
}