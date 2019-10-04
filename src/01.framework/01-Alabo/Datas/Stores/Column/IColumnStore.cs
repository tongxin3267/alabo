using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Column {

    public interface IColumnStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity {

        object GetFieldValue(object id, string field);
    }
}