using MongoDB.Bson;
using Alabo.App.Offline.Merchants.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Offline.Merchants.Domain.Services
{
    public interface IMerchantService : IService<Merchant, ObjectId>
    {
    }
}