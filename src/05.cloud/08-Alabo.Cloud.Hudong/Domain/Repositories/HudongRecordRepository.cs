using Alabo.App.Share.HuDong.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.App.Share.HuDong.Domain.Repositories
{
    public class HudongRecordRepository : RepositoryMongo<HudongRecord, ObjectId>, IHudongRecordRepository
    {
        public HudongRecordRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}