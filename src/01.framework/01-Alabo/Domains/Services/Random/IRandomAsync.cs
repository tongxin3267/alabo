using Alabo.Domains.Entities;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Random {

    public interface IRandomAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey> {

        Task<TEntity> GetRandomAsync(long id);
    }
}