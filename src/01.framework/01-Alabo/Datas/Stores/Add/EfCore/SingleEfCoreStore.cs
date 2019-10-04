using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Linq;
using System;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Alabo.Datas.Stores.Add.EfCore {

    public class SingleEfCoreStore<TEntity, TKey> : NoTrackingEfCoreStore<TEntity, TKey>, ISingleStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity {

        protected SingleEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// <summary>
        ///     根据Id查找单个实体
        /// </summary>
        /// <param name="id">Id标识</param>
        public TEntity GetSingle(object id) {
            if (id.SafeString().IsEmpty()) {
                return null;
            }

            return Find(id);
        }

        /// <summary>
        ///     查找单个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate) {
            return ToQueryable().FirstOrDefault(predicate);
        }

        public TEntity GetSingle(IExpressionQuery<TEntity> queryCriteria) {
            if (queryCriteria != null) {
                var resultList = queryCriteria.Execute(Set).FirstOrDefault();
                return resultList;
            }

            return FirstOrDefault();
        }

        /// <summary>
        ///     获取最后一条默认数据
        /// </summary>
        public TEntity LastOrDefault() {
            return ToQueryable().LastOrDefault();
        }

        /// <summary>
        ///     获取默认的一条数据
        /// </summary>
        public TEntity FirstOrDefault() {
            return ToQueryable().FirstOrDefault();
        }

        public TEntity GetSingleOrderBy(Expression<Func<TEntity, TKey>> keySelector) {
            return GetSingleOrderBy(keySelector, null);
        }

        public TEntity GetSingleOrderBy(Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TEntity, bool>> predicate) {
            var queryCriteria = new ExpressionQuery<TEntity>();
            queryCriteria.OrderBy(keySelector);
            if (predicate != null) {
                queryCriteria.And(predicate);
            }

            var find = queryCriteria.Execute(Set);
            return find.FirstOrDefault();
        }

        public TEntity GetSingleOrderByDescending(Expression<Func<TEntity, TKey>> keySelector) {
            return GetSingleOrderByDescending(keySelector, null);
        }

        public TEntity GetSingleOrderByDescending(Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TEntity, bool>> predicate) {
            var queryCriteria = new ExpressionQuery<TEntity>();
            queryCriteria.OrderByDescending(keySelector);
            if (predicate != null) {
                queryCriteria.And(predicate);
            }

            var find = queryCriteria.Execute(Set);
            return find.FirstOrDefault();
        }

        public TEntity GetSingleByIdNoTracking(TKey id) {
            var expression = Lambda.Equal<TEntity>("Id", id);
            return FindAsNoTracking().Where(expression).FirstOrDefault();
        }
    }
}