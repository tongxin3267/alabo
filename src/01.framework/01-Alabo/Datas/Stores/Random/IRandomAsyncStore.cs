using Alabo.Domains.Entities.Core;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Random {

    public interface IRandomAsyncStore<TEntity, in TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity {

        /// <summary>
        ///     随机查询
        /// </summary>
        Task<TEntity> GetRandomAsync();
    }
}