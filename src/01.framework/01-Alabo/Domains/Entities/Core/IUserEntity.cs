namespace Alabo.Domains.Entities.Core {

    /// <summary>
    ///     包含UserId的实体
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IUserEntity<TKey> : IEntity<TKey> {

        /// <summary>
        ///     用户Id
        /// </summary>
        long UserId { get; set; }
    }
}