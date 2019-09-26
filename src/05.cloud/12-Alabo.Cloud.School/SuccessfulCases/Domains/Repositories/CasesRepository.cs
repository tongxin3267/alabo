using Alabo.Cloud.School.SuccessfulCases.Domains.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.School.SuccessfulCases.Domains.Repositories {

    public class CasesRepository : RepositoryMongo<Cases, ObjectId>, ICasesRepository {

        public CasesRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}