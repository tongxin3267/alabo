using MongoDB.Bson;
using Alabo.App.Cms.Support.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Cms.Support.Domain.Repositories {

    public class WorkOrderRepository : RepositoryMongo<WorkOrder, ObjectId>, IWorkOrderRepository {

        public WorkOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}