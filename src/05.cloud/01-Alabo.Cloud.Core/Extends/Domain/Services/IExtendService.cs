using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace ZKCloud.App.Open.Attach.Domain.Services {

    public interface IExtendService : IService<Extend, ObjectId> {
    }
}