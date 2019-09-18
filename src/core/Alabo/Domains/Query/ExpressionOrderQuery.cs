using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Alabo.Domains.Entities.Core;

namespace Alabo.Domains.Query
{
    internal class ExpressionOrderQuery<TEntity> : ExpressionPredicateQuery<TEntity>, IOrderQuery<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IList<OrderQueryItem> _orderQueryList = new List<OrderQueryItem>();

        public IOrderQuery<TEntity> OrderByAscending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return OrderBy(keySelector, OrderType.Ascending);
        }

        public IOrderQuery<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return OrderBy(keySelector, OrderType.Descending);
        }

        public IOrderQuery<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector,
            OrderType type = OrderType.Ascending)
        {
            if (keySelector == null) {
                throw new ArgumentNullException(nameof(keySelector));
            }

            _orderQueryList.Add(new OrderQueryItem(typeof(TKey), type, keySelector));
            return this;
        }

        public override IQueryable<TEntity> Execute(IQueryable<TEntity> query)
        {
            query = base.Execute(query);
            if (_orderQueryList.Count <= 0) {
                return query;
            }

            foreach (var item in _orderQueryList) {
                query = item.Executor(query, item.KeySelectorExpression);
            }

            return query;
        }

        private class OrderQueryItem
        {
            public OrderQueryItem(Type keyType, OrderType orderType, LambdaExpression keySelectorExpression)
            {
                KeyType = keyType;
                OrderType = orderType;
                KeySelectorExpression = keySelectorExpression;
                ParameterExpression = keySelectorExpression.Parameters.FirstOrDefault();
                Executor = BuildOrderExecutor(KeyType, OrderType);
            }

            public Type KeyType { get; }

            public OrderType OrderType { get; }

            public Expression ParameterExpression { get; }

            public LambdaExpression KeySelectorExpression { get; }

            public Func<IQueryable<TEntity>, Expression, IQueryable<TEntity>> Executor { get; }

            private Func<IQueryable<TEntity>, Expression, IQueryable<TEntity>> BuildOrderExecutor(Type keyType,
                OrderType type)
            {
                MethodInfo method;
                if (type == OrderType.Ascending) {
                    method = typeof(Queryable)
                        .GetMethods(BindingFlags.Static | BindingFlags.Public)
                        .FirstOrDefault(e => e.Name == "OrderBy" && e.GetParameters().Length == 2);
                } else {
                    method = typeof(Queryable)
                        .GetMethods(BindingFlags.Static | BindingFlags.Public)
                        .FirstOrDefault(e => e.Name == "OrderByDescending" && e.GetParameters().Length == 2);
                }

                if (method == null) {
                    return null;
                }

                method = method.MakeGenericMethod(typeof(TEntity), keyType);
                var queryParameterExperssion = Expression.Parameter(typeof(IQueryable<TEntity>));
                var keySelectorParameterExpression = Expression.Parameter(typeof(Expression));
                var expressionType =
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(typeof(TEntity), keyType));
                var keySelectorConvertExpression = Expression.Convert(keySelectorParameterExpression, expressionType);
                var callExpression = Expression.Call(method, queryParameterExperssion, keySelectorConvertExpression);
                var lambdaExpression =
                    Expression.Lambda<Func<IQueryable<TEntity>, Expression, IQueryable<TEntity>>>(callExpression,
                        queryParameterExperssion, keySelectorParameterExpression);
                return lambdaExpression.Compile();
            }
        }
    }
}