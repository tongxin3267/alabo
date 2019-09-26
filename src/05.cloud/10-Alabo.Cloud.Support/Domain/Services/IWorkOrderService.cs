using System.Collections.Generic;
using Alabo.Cloud.Support.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Support.Domain.Services {

    public interface IWorkOrderService : IService<WorkOrder, ObjectId> {

        ServiceResult Delete(ObjectId id);

        PagedList<WorkOrder> GetPageList(object query);

        List<WorkOrder> GetWorkOrdersList();

        ServiceResult AddWorkOrder(WorkOrder view);

        ServiceResult UpdateWorkOrder(WorkOrder view);
    }
}