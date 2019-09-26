using Alabo.Cloud.People.Relationship.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.People.Relationship.Domain.Repositories
{
    public class RelationshipIndexRepository : RepositoryMongo<RelationshipIndex, ObjectId>,
        IRelationshipIndexRepository
    {
        public RelationshipIndexRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}