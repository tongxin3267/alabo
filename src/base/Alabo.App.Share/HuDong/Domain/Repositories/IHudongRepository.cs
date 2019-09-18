using MongoDB.Bson;
using Alabo.App.Share.HuDong.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Share.HuDong.Domain.Repositories {

    public interface IHudongRepository : IRepository<Hudong, ObjectId> {
    }
}