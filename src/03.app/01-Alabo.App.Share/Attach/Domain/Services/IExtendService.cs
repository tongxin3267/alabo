using MongoDB.Bson;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Share.Attach.Domain.Services {

    public interface IExtendService : IService<Extend, ObjectId> {
    }
}