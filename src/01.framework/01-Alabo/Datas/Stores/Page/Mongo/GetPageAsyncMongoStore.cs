using Alabo.Datas.Stores.Max.Mongo;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Query;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Page.Mongo
{
    public abstract class GetPageAsyncMongoStore<TEntity, TKey> : MaxMongoStore<TEntity, TKey>,
        IGetPageAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected GetPageAsyncMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<PagedList<TEntity>> GetPagedListAsync(IPageQuery<TEntity> query)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate, int pageSize,
            int pageIndex)
        {
            throw new NotImplementedException();
        }
    }
}