using Alabo.Cloud.People.Visitors.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.People.Visitors.Domain.Repositories
{
    public class VisitorRepository : RepositoryMongo<Visitor, ObjectId>, IVisitorRepository
    {
        public VisitorRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}