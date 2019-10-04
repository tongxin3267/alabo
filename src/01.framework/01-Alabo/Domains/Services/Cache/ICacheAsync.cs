using Alabo.Domains.Entities;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Cache {

    public interface ICacheAsync<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     从缓存中读取数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id">主键ID</param>
        Task<TEntity> GetSingleFromCacheAsync(TKey id);
    }
}