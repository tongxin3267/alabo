using MongoDB.Bson;
using Alabo.App.Cms.Support.Domain.Entities;
using Alabo.App.Cms.Support.Domain.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Cms.Support.Domain.Services {

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