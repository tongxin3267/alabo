using MongoDB.Bson;
using Alabo.App.Offline.Product.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Offline.Product.Domain.Repositories
{
    public class MerchantProductRepository : RepositoryMongo<MerchantProduct, ObjectId>, IMerchantProductRepository
    {
        public MerchantProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}