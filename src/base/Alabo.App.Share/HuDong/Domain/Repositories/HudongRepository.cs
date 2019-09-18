using MongoDB.Bson;
using Alabo.App.Open.HuDong.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Open.HuDong.Domain.Repositories {

    public class HudongRepository : RepositoryMongo<Hudong, ObjectId>, IHudongRepository {

        public HudongRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}