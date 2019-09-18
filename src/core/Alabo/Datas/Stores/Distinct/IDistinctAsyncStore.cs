using System.Threading.Tasks;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Distinct
{
    public interface IDistinctAsyncStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        Task<bool> DistinctAsync(string filedName);
    }
}