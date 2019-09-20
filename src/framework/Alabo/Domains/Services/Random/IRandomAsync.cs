using System.Threading.Tasks;
using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.Random
{
    public interface IRandomAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        Task<TEntity> GetRandomAsync(long id);
    }
}