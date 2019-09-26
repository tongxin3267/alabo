using Alabo.Data.Things.Brands.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.Things.Brands.Domain.Repositories
{
    public class BrandRepository : RepositoryMongo<Brand, ObjectId>, IBrandRepository
    {
        public BrandRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}