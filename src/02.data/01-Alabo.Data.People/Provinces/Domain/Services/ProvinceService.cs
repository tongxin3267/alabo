using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Provinces.Domain.Services {

    public class ProvinceService : ServiceBase<Entities.Province, ObjectId>, IProvinceService {

        public ProvinceService(IUnitOfWork unitOfWork, IRepository<Entities.Province, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}