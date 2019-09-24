using MongoDB.Bson;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Market.Relationship.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Market.Relationship.Domain.Services {

    public interface IRelationshipIndexService : IService<RelationshipIndex, ObjectId> {

        /// <summary>
        ///     �û�ע����¼�
        /// </summary>
        /// <param name="user"></param>
        void UserRegAfter(User user);
    }
}