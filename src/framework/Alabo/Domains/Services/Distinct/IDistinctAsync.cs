using System.Threading.Tasks;
using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.Distinct
{
    public interface IDistinctAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        Task<bool> DistinctAsync(string filedName);
    }
}