using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.Cache
{
    public interface ICache<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     从缓存中读取数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id">主键ID</param>
        TEntity GetSingleFromCache(TKey id);
    }
}