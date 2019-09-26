using Alabo.Cloud.People.Relationship.Domain.Entities;
using Alabo.Domains.Services;
using Alabo.Users.Entities;
using MongoDB.Bson;

namespace Alabo.Cloud.People.Relationship.Domain.Services
{
    public interface IRelationshipIndexService : IService<RelationshipIndex, ObjectId>
    {
        /// <summary>
        ///     �û�ע����¼�
        /// </summary>
        /// <param name="user"></param>
        void UserRegAfter(User user);
    }
}