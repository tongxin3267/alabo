using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Distinct
{
    public interface IDistinctStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        bool Distinct(string filedName);
    }
}