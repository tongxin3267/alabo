using MongoDB.Bson;
using Alabo.App.Open.Attach.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Open.Attach.Domain.Repositories {

    public interface IFavoriteRepository : IRepository<Favorite, ObjectId> {
    }
}