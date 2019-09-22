using MongoDB.Bson;
using Alabo.App.Share.HuDong.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Share.HuDong.Domain.Repositories {

    public class HudongRecordRepository : RepositoryMongo<HudongRecord, ObjectId>, IHudongRecordRepository {

        public HudongRecordRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}