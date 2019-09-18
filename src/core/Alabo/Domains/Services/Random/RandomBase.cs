using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.Random
{
    public abstract class RandomBase<TEntity, TKey> : RandomAsyncBase<TEntity, TKey>, IRandom<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        protected RandomBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public TEntity GetRandom(long id)
        {
            switch (id)
            {
                case 1:
                    return FirstOrDefault();

                case -1:
                    return LastOrDefault();

                default:
                    return FirstOrDefault();
                // return Store.GetRandom();
            }
        }
    }
}