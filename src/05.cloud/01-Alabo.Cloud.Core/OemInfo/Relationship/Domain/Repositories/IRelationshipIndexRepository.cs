using MongoDB.Bson;
using Alabo.App.Market.Relationship.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Market.Relationship.Domain.Repositories {

    public interface IRelationshipIndexRepository : IRepository<RelationshipIndex, ObjectId> {
    }
}