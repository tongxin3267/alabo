using Alabo.Cloud.Shop.Favorites.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.Favorites.Domain.Repositories {

    public interface IFavoriteRepository : IRepository<Favorite, ObjectId> {
    }
}