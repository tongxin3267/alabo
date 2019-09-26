using Alabo.Cloud.Support.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Support.Domain.Repositories {

    public class WorkOrderRepository : RepositoryMongo<WorkOrder, ObjectId>, IWorkOrderRepository {

        public WorkOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}