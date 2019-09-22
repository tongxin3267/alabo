using MongoDB.Bson;
using Alabo.App.Offline.Product.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Offline.Product.Domain.Services
{
    public interface IMerchantProductService : IService<MerchantProduct, ObjectId>
    {
    }
}