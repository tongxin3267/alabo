using MongoDB.Bson;
using Alabo.App.Market.Relationship.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Market.Relationship.Domain.Repositories {

    public class RelationshipIndexRepository : RepositoryMongo<RelationshipIndex, ObjectId>,
        IRelationshipIndexRepository {

        public RelationshipIndexRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}