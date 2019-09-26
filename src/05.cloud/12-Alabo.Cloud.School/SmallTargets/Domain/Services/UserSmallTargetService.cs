using Alabo.Cloud.School.SmallTargets.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.School.SmallTargets.Domain.Services {

    public class UserSmallTargetService : ServiceBase<UserSmallTarget, ObjectId>, IUserSmallTargetService {

        public UserSmallTargetService(IUnitOfWork unitOfWork, IRepository<UserSmallTarget, ObjectId> repository) : base(
            unitOfWork, repository) {
        }
    }
}