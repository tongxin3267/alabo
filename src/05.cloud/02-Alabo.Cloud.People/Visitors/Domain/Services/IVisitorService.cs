using Alabo.Cloud.People.Visitors.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.People.Visitors.Domain.Services {

    public interface IVisitorService : IService<Visitor, ObjectId> {
    }
}