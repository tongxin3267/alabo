using Alabo.Cloud.People.Relationship.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.People.Relationship.Domain.Repositories {

    public interface IRelationshipIndexRepository : IRepository<RelationshipIndex, ObjectId> {
    }
}