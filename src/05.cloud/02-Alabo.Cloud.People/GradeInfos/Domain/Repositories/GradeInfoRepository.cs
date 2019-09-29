using Alabo.Cloud.People.GradeInfos.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.People.GradeInfos.Domain.Repositories
{
    public class GradeInfoRepository : RepositoryMongo<GradeInfo, ObjectId>, IGradeInfoRepository
    {
        public GradeInfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}