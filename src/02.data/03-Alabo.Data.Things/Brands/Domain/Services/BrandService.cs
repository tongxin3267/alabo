using Alabo.Data.Things.Brands.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.Things.Brands.Domain.Services
{
    public class BrandService : ServiceBase<Brand, ObjectId>, IBrandService
    {
        public BrandService(IUnitOfWork unitOfWork, IRepository<Brand, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }
    }
}