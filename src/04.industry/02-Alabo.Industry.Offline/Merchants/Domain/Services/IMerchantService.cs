using Alabo.Domains.Services;
using Alabo.Industry.Offline.Merchants.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.Merchants.Domain.Services
{
    public interface IMerchantService : IService<Merchant, ObjectId>
    {
    }
}