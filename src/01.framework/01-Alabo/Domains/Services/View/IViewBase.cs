using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.View {

    /// <summary>
    ///     获取视图
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IViewBase<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     获取视图
        /// </summary>
        /// <param name="id">Id</param>
        TEntity GetViewById(object id);
    }
}