using Alabo.Cloud.Support.Domain.Entities;
using Alabo.Cloud.Support.Domain.ViewModels;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;
using System;

namespace Alabo.Cloud.Support.Domain.Services
{
    public class WorkOrderServices : ServiceBase<WorkOrder, ObjectId>, IWorkOrderServices
    {
        public WorkOrderServices(IUnitOfWork unitOfWork, IRepository<WorkOrder, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }

        public ServiceResult AddOrUpdate(ViewWorkOrder view)
        {
            throw new NotImplementedException();
        }

        public WorkOrder GetSingle(ObjectId id)
        {
            return GetSingle(r => r.Id == id);
        }

        public ViewWorkOrder GetView(ObjectId id)
        {
            throw new NotImplementedException();
        }
    }
}