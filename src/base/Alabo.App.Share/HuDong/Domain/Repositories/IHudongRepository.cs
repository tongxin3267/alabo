using MongoDB.Bson;
using Alabo.App.Open.HuDong.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Open.HuDong.Domain.Repositories {

    public interface IHudongRepository : IRepository<Hudong, ObjectId> {
    }
}