using Alabo.Cloud.School.SmallTargets.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.School.SmallTargets.Domain.Repositories
{
    public class UserSmallTargetRepository : RepositoryMongo<UserSmallTarget, ObjectId>, IUserSmallTargetRepository
    {
        public UserSmallTargetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}