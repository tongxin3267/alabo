using MongoDB.Bson;
using Alabo.App.Cms.Support.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Cms.Support.Domain.Repositories {

    public interface IWorkOrderRepository : IRepository<WorkOrder, ObjectId> {
    }
}