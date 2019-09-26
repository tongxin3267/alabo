using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Offline.Merchants.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.Merchants.Domain.Repositories
{
    public class MerchantStoreRepository : RepositoryMongo<MerchantStore, ObjectId>, IMerchantStoreRepository
    {
        public MerchantStoreRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}