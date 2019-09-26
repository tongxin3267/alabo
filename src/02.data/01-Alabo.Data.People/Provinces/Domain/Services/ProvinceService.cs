using Alabo.Data.People.Provinces.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Provinces.Domain.Services
{
    public class ProvinceService : ServiceBase<Province, ObjectId>, IProvinceService
    {
        public ProvinceService(IUnitOfWork unitOfWork, IRepository<Province, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }
    }
}