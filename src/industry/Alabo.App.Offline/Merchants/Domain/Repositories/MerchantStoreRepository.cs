using MongoDB.Bson;
using Alabo.App.Offline.Merchants.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Offline.Merchants.Domain.Repositories
{
    public class MerchantStoreRepository : RepositoryMongo<MerchantStore, ObjectId>, IMerchantStoreRepository
    {
        public MerchantStoreRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}