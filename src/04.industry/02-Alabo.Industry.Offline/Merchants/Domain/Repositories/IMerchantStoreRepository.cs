using Alabo.Domains.Repositories;
using Alabo.Industry.Offline.Merchants.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.Merchants.Domain.Repositories
{
    public interface IMerchantStoreRepository : IRepository<MerchantStore, ObjectId>
    {
    }
}