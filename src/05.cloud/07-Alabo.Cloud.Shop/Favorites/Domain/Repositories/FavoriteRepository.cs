using MongoDB.Bson;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Share.Attach.Domain.Repositories {

    public class FavoriteRepository : RepositoryMongo<Favorite, ObjectId>, IFavoriteRepository {

        public FavoriteRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}