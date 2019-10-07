using Alabo.Data.Targets.Iterations.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.Targets.Iterations.Domain.Services
{
    public class IterationService : ServiceBase<Iteration, ObjectId>, IIterationService
    {
        public IterationService(IUnitOfWork unitOfWork, IRepository<Iteration, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}