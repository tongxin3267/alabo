using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Random.Mongo
{
    public abstract class RandomMongoStore<TEntity, TKey> : RandomAsyncMongoStore<TEntity, TKey>,
        IRandomStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected RandomMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public TEntity GetRandom()
        {
            //var find = Collection.AsQueryable().Where(x => true).OrderBy(a => Guid.NewGuid()).Take(1).FirstOrDefault();
            var find = FirstOrDefault();
            return find;
        }
    }
}