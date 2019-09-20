using MongoDB.Bson;
using Alabo.App.Core.Markets.Visitors.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Markets.Visitors.Domain.Repositories {

    public interface IVisitorRepository : IRepository<Visitor, ObjectId> {
    }
}