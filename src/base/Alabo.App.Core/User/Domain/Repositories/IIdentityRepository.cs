using MongoDB.Bson;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.User.Domain.Repositories {

    public interface IIdentityRepository : IRepository<Identity, ObjectId> {
    }
}