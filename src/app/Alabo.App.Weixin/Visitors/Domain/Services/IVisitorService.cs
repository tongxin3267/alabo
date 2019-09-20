using MongoDB.Bson;
using Alabo.App.Core.Markets.Visitors.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Markets.Visitors.Domain.Services {

    public interface IVisitorService : IService<Visitor, ObjectId> {
    }
}