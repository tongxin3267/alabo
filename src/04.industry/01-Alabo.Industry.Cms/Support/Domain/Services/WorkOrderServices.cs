using MongoDB.Bson;
using System;
using Alabo.App.Cms.Support.Domain.Entities;
using Alabo.App.Cms.Support.Domain.ViewModels;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Cms.Support.Domain.Services {

    public class WorkOrderServices : ServiceBase<WorkOrder, ObjectId>, IWorkOrderServices {

        public ServiceResult AddOrUpdate(ViewWorkOrder view) {
            throw new NotImplementedException();
        }

        public WorkOrder GetSingle(ObjectId id) {
            return GetSingle(r => r.Id == id);
        }

        public ViewWorkOrder GetView(ObjectId id) {
            throw new NotImplementedException();
        }

        public WorkOrderServices(IUnitOfWork unitOfWork, IRepository<WorkOrder, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}