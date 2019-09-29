using Alabo.Domains.Entities.Core;
using MongoDB.Bson;

namespace Alabo.Domains.Entities
{
    /// <summary>
    ///     聚合根
    /// </summary>
    public interface IAggregateMongoRoot : IEntity<ObjectId>, IMongoEntity, IAggregateRoot
    {
    }

    /// <summary>
    ///     聚合根
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IAggregateMongoRoot<TEntity> : IAggregateRoot<TEntity, ObjectId>, IAggregateMongoRoot
        where TEntity : IAggregateRoot
    {
    }
}