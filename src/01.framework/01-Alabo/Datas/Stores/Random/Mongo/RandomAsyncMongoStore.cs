using System;
using System.Threading.Tasks;
using Alabo.Datas.Stores.Page.Mongo;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Alabo.Datas.Stores.Random.Mongo
{
    public abstract class RandomAsyncMongoStore<TEntity, TKey> : GetPageMongoStore<TEntity, TKey>,
        IRandomAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected RandomAsyncMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<TEntity> GetRandomAsync()
        {
            return await Collection.AsQueryable().Where(x => true).OrderBy(a => Guid.NewGuid()).Take(1)
                .FirstOrDefaultAsync();
        }
    }
}