using MongoDB.Bson;
using Alabo.App.Core.Markets.EnterpriseCertification.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Markets.EnterpriseCertification.Domain.Repositories {

    public class EnterpriseRepository : RepositoryMongo<Enterprise, ObjectId>, IEnterpriseRepository {

        public EnterpriseRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}