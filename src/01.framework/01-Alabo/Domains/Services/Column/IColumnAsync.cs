using Alabo.Domains.Entities;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Column
{
    public interface IColumnAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        Task<object> GetFieldValueAsync(object id, string field);
    }
}