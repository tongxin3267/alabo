using MongoDB.Bson;
using Alabo.App.Offline.Product.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Offline.Product.Domain.Repositories
{
    public interface IMerchantProductRepository : IRepository<MerchantProduct, ObjectId>
    {
    }
}