using Alabo.Data.People.Provinces.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Provinces.Domain.Repositories
{
    public class ProvinceRepository : RepositoryMongo<Province, ObjectId>, IProvinceRepository
    {
        public ProvinceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}