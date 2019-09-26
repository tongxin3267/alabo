using Alabo.Domains.Services;
using Alabo.Industry.Offline.Product.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.Product.Domain.Services
{
    public interface IMerchantProductService : IService<MerchantProduct, ObjectId>
    {
    }
}