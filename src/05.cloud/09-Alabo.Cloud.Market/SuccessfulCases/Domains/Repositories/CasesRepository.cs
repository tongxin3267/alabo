using MongoDB.Bson;
using Alabo.App.Market.SuccessfulCases.Domains.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Market.SuccessfulCases.Domains.Repositories {

    public class CasesRepository : RepositoryMongo<Cases, ObjectId>, ICasesRepository {

        public CasesRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}