using Alabo.Cloud.Shop.Favorites.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.Favorites.Domain.Repositories {

    public class FavoriteRepository : RepositoryMongo<Favorite, ObjectId>, IFavoriteRepository {

        public FavoriteRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}