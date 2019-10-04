using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.Distinct {

    public interface IDistinct<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     是否包含重复字段
        /// </summary>
        /// <param name="filedName">字段名称</param>
        bool Distinct(string filedName);
    }
}