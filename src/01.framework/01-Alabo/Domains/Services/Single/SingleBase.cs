using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Domains.Services.Add;
using Alabo.Linq;
using Alabo.Mapping;
using System;
using System.Linq.Expressions;

namespace Alabo.Domains.Services.Single
{
    public abstract class SingleBase<TEntity, TKey> : CoreBaseService<TEntity, TKey>, ISingle<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected SingleBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public TEntity GetSingle(object id)
        {
            return Store.GetSingle(id);
        }

        public TEntity GetSingle(string property, object value)
        {
            //    var dynamicWhere = LinqHelper.GetExpression<TEntity, bool>($"entity.{property} == MongoDB.Bson.ObjectId.Parse(\"{value}\")", "entity");
            var expression = Lambda.Equal<TEntity>(property, value);
            var find = GetSingle(expression);

            return find;
        }

        public TEntity FirstOrDefault()
        {
            return Store.FirstOrDefault();
        }

        public TEntity LastOrDefault()
        {
            return Store.LastOrDefault();
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            var find = Store.GetSingle(predicate);
            if (find != null) find = JsonMapping.ConvertToExtension(find);

            return find;
        }

        public TEntity Next(TEntity model)
        {
            var dynamic = (dynamic)model;
            var dynamicWhere = LinqHelper.GetExpression<TEntity, bool>($"entity.Id > {dynamic.Id}", "entity");
            var next = GetSingle(dynamicWhere);
            if (next == null) next = model;

            return next;
        }

        public TEntity Prex(TEntity model)
        {
            var dynamic = (dynamic)model;

            var query = new ExpressionQuery<TEntity>();
            var dynamicWhere = LinqHelper.GetExpression<TEntity, bool>($"entity.Id < {dynamic.Id}", "entity");
            query.And(dynamicWhere);
            //排序方式
            var orderExpression = LinqHelper.GetExpression<TEntity, long>("entity.Id", "entity");
            query.OrderByDescending(orderExpression);
            var prex = GetSingle(query);
            if (prex == null) prex = model;

            return prex;
        }

        public TEntity GetSingleOrderBy(Expression<Func<TEntity, TKey>> keySelector)
        {
            return Store.GetSingleOrderBy(keySelector);
        }

        public TEntity GetSingleOrderBy(Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TEntity, bool>> predicate)
        {
            return Store.GetSingleOrderBy(keySelector, predicate);
        }

        public TEntity GetSingleOrderByDescending(Expression<Func<TEntity, TKey>> keySelector)
        {
            return Store.GetSingleOrderByDescending(keySelector);
        }

        public TEntity GetSingleOrderByDescending(Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TEntity, bool>> predicate)
        {
            return Store.GetSingleOrderByDescending(keySelector, predicate);
        }

        public TEntity GetSingle(IExpressionQuery<TEntity> query)
        {
            return Store.GetSingle(query);
        }

        public TEntity Next(object id)
        {
            var find = GetSingle(id);
            return Next(find);
        }

        public TEntity Prex(object id)
        {
            var find = GetSingle(id);
            return Prex(find);
        }
    }
}