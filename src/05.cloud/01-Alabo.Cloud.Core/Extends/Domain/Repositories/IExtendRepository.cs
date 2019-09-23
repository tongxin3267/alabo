using MongoDB.Bson;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Share.Attach.Domain.Repositories {

    public interface IExtendRepository : IRepository<Extend, ObjectId> {
    }
}