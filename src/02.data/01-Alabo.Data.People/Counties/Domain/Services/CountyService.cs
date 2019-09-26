using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Counties.Domain.Services {

    public class CountyService : ServiceBase<Domain.Entities.County, ObjectId>, ICountyService {

        public CountyService(IUnitOfWork unitOfWork, IRepository<Domain.Entities.County, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}