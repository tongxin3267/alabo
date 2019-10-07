using Alabo.Data.Targets.Targets.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.Targets.Targets.Domain.Services
{
    public class TargetService : ServiceBase<Target, ObjectId>, ITargetService
    {
        public TargetService(IUnitOfWork unitOfWork, IRepository<Target, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}