using Alabo.Data.Targets.Iterations.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.Targets.Iterations.Domain.Repositories
{
    public class IterationRepository : RepositoryMongo<Iteration, ObjectId>, IIterationRepository
    {
        public IterationRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}