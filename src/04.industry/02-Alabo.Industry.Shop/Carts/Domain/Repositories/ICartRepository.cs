using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.Carts.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Carts.Domain.Repositories {

    public interface ICartRepository : IRepository<Cart, ObjectId> {
    }
}