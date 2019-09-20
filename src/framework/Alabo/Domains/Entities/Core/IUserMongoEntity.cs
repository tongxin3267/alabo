using MongoDB.Bson;

namespace Alabo.Domains.Entities.Core
{
    public interface IUserMongoEntity : IUserEntity<ObjectId>, IMongoEntity
    {
    }
}