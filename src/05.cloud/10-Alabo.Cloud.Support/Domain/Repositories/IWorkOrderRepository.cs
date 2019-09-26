using Alabo.Cloud.Support.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Support.Domain.Repositories {

    public interface IWorkOrderRepository : IRepository<WorkOrder, ObjectId> {
    }
}