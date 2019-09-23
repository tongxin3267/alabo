using MongoDB.Bson;
using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Order.Domain.Repositories {

    public class CartRepository : RepositoryMongo<Cart, ObjectId>, ICartRepository {

        public CartRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}