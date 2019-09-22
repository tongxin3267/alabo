using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Stores.Random
{
    public interface IRandomStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        /// <summary>
        ///     随机查询
        /// </summary>
        TEntity GetRandom();
    }
}