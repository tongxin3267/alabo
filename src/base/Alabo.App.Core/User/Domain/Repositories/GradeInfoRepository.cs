using MongoDB.Bson;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.User.Domain.Repositories {

    public class GradeInfoRepository : RepositoryMongo<GradeInfo, ObjectId>, IGradeInfoRepository {

        public GradeInfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}