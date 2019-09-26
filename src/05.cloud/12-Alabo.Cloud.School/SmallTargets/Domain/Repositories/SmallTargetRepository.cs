using Alabo.Cloud.School.SmallTargets.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.School.SmallTargets.Domain.Repositories
{
    public class SmallTargetRepository : RepositoryMongo<SmallTarget, ObjectId>, ISmallTargetRepository
    {
        public SmallTargetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}