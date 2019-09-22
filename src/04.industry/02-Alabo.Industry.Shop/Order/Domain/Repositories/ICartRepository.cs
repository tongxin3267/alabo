using MongoDB.Bson;
using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Order.Domain.Repositories {

    public interface ICartRepository : IRepository<Cart, ObjectId> {
    }
}