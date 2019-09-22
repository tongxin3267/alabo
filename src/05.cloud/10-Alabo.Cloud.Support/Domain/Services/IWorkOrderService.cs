using MongoDB.Bson;
using System.Collections.Generic;
using Alabo.App.Cms.Support.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Cms.Support.Domain.Services {

    public interface IWorkOrderService : IService<WorkOrder, ObjectId> {

        ServiceResult Delete(ObjectId id);

        PagedList<WorkOrder> GetPageList(object query);

        List<WorkOrder> GetWorkOrdersList();

        ServiceResult AddWorkOrder(WorkOrder view);

        ServiceResult UpdateWorkOrder(WorkOrder view);
    }
}