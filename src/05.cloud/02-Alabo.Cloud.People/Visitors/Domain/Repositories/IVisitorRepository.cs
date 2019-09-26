using Alabo.Cloud.People.Visitors.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.People.Visitors.Domain.Repositories {

    public interface IVisitorRepository : IRepository<Visitor, ObjectId> {
    }
}