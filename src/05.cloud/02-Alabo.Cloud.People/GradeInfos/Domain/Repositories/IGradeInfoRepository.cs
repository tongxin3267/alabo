using Alabo.Cloud.People.GradeInfos.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.People.GradeInfos.Domain.Repositories
{
    public interface IGradeInfoRepository : IRepository<GradeInfo, ObjectId>
    {
    }
}