using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Alabo.Datas.Sql.Queries;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.EFCore.Context;
using Alabo.Extensions;
using Alabo.Helpers;

namespace Alabo.Datas.Stores.Add.EfCore
{
    public abstract class EfCoreStoreBase<TEntity, TKey> : StoreBase
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        /// <summary>
        ///     工作单元
        /// </summary>
        private readonly UnitOfWorkBase _unitOfWorkBase;

        /// <summary>
        ///     Sql查询对象
        /// </summary>
        private ISqlQuery _sqlQuery;

        private DbConnection dbContext;

        /// <summary>
        ///     跟踪Id
        /// </summary>
        private Guid traceId = Guid.Empty;

        /// <summary>
        ///     初始化查询存储器
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        protected EfCoreStoreBase(IUnitOfWork unitOfWork)
        {
            _unitOfWorkBase = (UnitOfWorkBase) unitOfWork;
            traceId = _unitOfWorkBase.TraceId;
        }

        public IRepositoryContext RepositoryContext
        {
            get
            {
                var context = new EfCoreRepositoryContext(_unitOfWorkBase);
                return context;
            }
        }

        public DbConnection DbConnection
        {
            get
            {
                if (dbContext == null) {
                    dbContext = _unitOfWorkBase.Database.GetDbConnection();
                }

                return dbContext;
            }
        }

        /// <summary>
        ///     工作单元
        /// </summary>
        protected UnitOfWorkBase UnitOfWork => _unitOfWorkBase;

        /// <summary>
        ///     实体集
        /// </summary>
        protected DbSet<TEntity> Set => _unitOfWorkBase.Set<TEntity>();

        /// <summary>
        ///     Sql查询对象
        /// </summary>
        protected virtual ISqlQuery Sql => _sqlQuery ?? (_sqlQuery = Ioc.Resolve<ISqlQuery>());

        /// <summary>
        ///     获取未跟踪查询对象
        /// </summary>
        public IQueryable<TEntity> FindAsNoTracking()
        {
            var work = HttpWeb.Tenant;
            return Set.AsNoTracking();
        }

        /// <summary>
        ///     获取查询对象
        /// </summary>
        /// <param name="predicate">条件</param>
        public IQueryable<TEntity> ToQueryable(Expression<Func<TEntity, bool>> predicate = null)
        {
            var work = HttpWeb.Tenant;
            if (predicate == null) {
                return Set;
            }

            return Set.Where(predicate);
        }

        /// <summary>
        ///     获取查询对象
        /// </summary>
        /// <param name="predicate">条件</param>
        public IQueryable<TEntity> ToQueryableAsNoTracking(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null) {
                return Set.AsNoTracking();
            }

            return Set.AsNoTracking().Where(predicate);
        }

        /// <summary>
        ///     查找实体
        /// </summary>
        /// <param name="id">标识</param>
        public TEntity Find(object id)
        {
            if (id.SafeString().IsEmpty()) {
                return null;
            }

            if (id.ToString() == "0") {
                return null;
            }

            return Set.Find(id);
        }

        /// <summary>
        ///     查找实体
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="cancellationToken">取消令牌</param>
        public async Task<TEntity> FindAsync(object id,
            CancellationToken cancellationToken = default)
        {
            if (id.SafeString().IsEmpty()) {
                return null;
            }

            return await Set.FindAsync(new[] {id}, cancellationToken);
        }

        /// <summary>
        ///     查找实体列表
        /// </summary>
        /// <param name="ids">标识列表</param>
        public List<TEntity> FindByIds(params TKey[] ids)
        {
            return FindByIds((IEnumerable<TKey>) ids);
        }

        /// <summary>
        ///     查找实体列表
        /// </summary>
        /// <param name="ids">标识列表</param>
        public List<TEntity> FindByIds(IEnumerable<TKey> ids)
        {
            if (ids == null) {
                return null;
            }

            return ToQueryable(t => ids.Contains(t.Id)).ToList();
        }

        /// <summary>
        ///     验证版本号
        /// </summary>
        /// <param name="newEntity">新实体</param>
        /// <param name="oldEntity">旧实体</param>
        protected void ValidateVersion(TEntity newEntity, TEntity oldEntity)
        {
            //if (newEntity.Version == null)
            //  //  throw new ConcurrencyException();
            //if (newEntity.Version.Length != oldEntity.Version.Length)
            //  //  throw new ConcurrencyException();
            //for (int i = 0; i < oldEntity.Version.Length; i++) {
            //    if (newEntity.Version[i] != oldEntity.Version[i])
            //      //  throw new ConcurrencyException();
            //}
        }

        /// <summary>
        ///     验证版本号
        /// </summary>
        /// <param name="newEntities">新实体集合</param>
        /// <param name="oldEntities">旧实体集合</param>
        protected void ValidateVersion(List<TEntity> newEntities, List<TEntity> oldEntities)
        {
            //if (oldEntities == null)
            //    throw new ArgumentNullException(nameof(oldEntities));
            //if (newEntities.Count != oldEntities.Count)
            //    throw new ConcurrencyException();
            //foreach (var entity in newEntities) {
            //    var old = oldEntities.Find(t => t.Id.Equals(entity.Id));
            //    if (old == null)
            //        throw new ConcurrencyException();
            //    ValidateVersion(entity, old);
            //}
        }
    }
}