using Alabo.Data.Targets.Targets.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.Targets.Targets.Domain.Repositories
{
    public class TargetRepository : RepositoryMongo<Target, ObjectId>, ITargetRepository
    {
        public TargetRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}