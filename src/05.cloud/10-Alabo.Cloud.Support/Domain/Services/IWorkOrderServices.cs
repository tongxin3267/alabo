using Alabo.Cloud.Support.Domain.Entities;
using Alabo.Cloud.Support.Domain.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Support.Domain.Services {

    public interface IWorkOrderServices : IService<WorkOrder, ObjectId> {
        /// <summary>
        ///     获取单个记录
        /// </summary>
        /// <param name="id">主键ID</param>

        WorkOrder GetSingle(ObjectId id);

        /// <summary>
        ///     视图
        /// </summary>
        /// <param name="id">主键ID</param>

        ViewWorkOrder GetView(ObjectId id);

        ServiceResult AddOrUpdate(ViewWorkOrder view);
    }
}