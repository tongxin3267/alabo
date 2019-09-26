using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.Carts.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Carts.Domain.Repositories
{
    public class CartRepository : RepositoryMongo<Cart, ObjectId>, ICartRepository
    {
        public CartRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}