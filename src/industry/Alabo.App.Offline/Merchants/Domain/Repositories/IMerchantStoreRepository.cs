using MongoDB.Bson;
using Alabo.App.Offline.Merchants.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Offline.Merchants.Domain.Repositories
{
    public interface IMerchantStoreRepository : IRepository<MerchantStore, ObjectId>
    {
    }
}