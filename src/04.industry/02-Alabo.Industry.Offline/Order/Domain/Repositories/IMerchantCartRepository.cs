using Alabo.Domains.Repositories;
using Alabo.Industry.Offline.Order.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.Order.Domain.Repositories
{
    public interface IMerchantCartRepository : IRepository<MerchantCart, ObjectId>
    {
    }
}