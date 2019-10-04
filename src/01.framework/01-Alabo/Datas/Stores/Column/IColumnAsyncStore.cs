using Alabo.Domains.Entities.Core;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Column {

    public interface IColumnAsyncStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity {

        Task<object> GetFieldValueAsync(object id, string field);
    }
}