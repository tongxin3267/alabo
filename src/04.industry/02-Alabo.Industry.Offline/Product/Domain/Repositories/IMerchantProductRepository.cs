using Alabo.Domains.Repositories;
using Alabo.Industry.Offline.Product.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.Product.Domain.Repositories
{
    public interface IMerchantProductRepository : IRepository<MerchantProduct, ObjectId>
    {
    }
}