using Alabo.App.Share.HuDong.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.App.Share.HuDong.Domain.Repositories
{
    public class HudongRepository : RepositoryMongo<Hudong, ObjectId>, IHudongRepository
    {
        public HudongRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}