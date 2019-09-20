using System.Threading.Tasks;
using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Dictionary;

namespace Alabo.Domains.Services.Distinct
{
    public abstract class DistinctAsyncBase<TEntity, TKey> : DictionaryBaseService<TEntity, TKey>,
        IDistinctAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        protected DistinctAsyncBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public async Task<bool> DistinctAsync(string filedName)
        {
            return await Store.DistinctAsync(filedName);
        }
    }
}