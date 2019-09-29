using Alabo.Cloud.School.SmallTargets.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.School.SmallTargets.Domain.Services
{
    public class SmallTargetService : ServiceBase<SmallTarget, ObjectId>, ISmallTargetService
    {
        public SmallTargetService(IUnitOfWork unitOfWork, IRepository<SmallTarget, ObjectId> repository) : base(
            unitOfWork, repository)
        {
        }
    }
}