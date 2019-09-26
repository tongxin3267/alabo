using Alabo.App.Share.HuDong.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.App.Share.HuDong.Domain.Repositories
{
    public interface IHudongRepository : IRepository<Hudong, ObjectId>
    {
    }
}