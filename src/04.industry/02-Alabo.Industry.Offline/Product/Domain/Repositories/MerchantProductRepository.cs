using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Offline.Product.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.Product.Domain.Repositories
{
    public class MerchantProductRepository : RepositoryMongo<MerchantProduct, ObjectId>, IMerchantProductRepository
    {
        public MerchantProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}