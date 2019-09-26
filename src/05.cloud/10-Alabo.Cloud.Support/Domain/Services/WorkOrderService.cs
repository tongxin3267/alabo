using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Cms.Support.Domain.Entities;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;

namespace Alabo.App.Cms.Support.Domain.Services {

    public class WorkOrderService : ServiceBase<WorkOrder, ObjectId>, IWorkOrderService {

        public ServiceResult AddWorkOrder(WorkOrder view) {
            var result = Resolve<IWorkOrderService>().Add(view);
            if (!result) {
                return ServiceResult.FailedWithMessage("添加失败");
            }

            var user = Resolve<IUserService>().GetSingle(view.UserId);
            if (!user.Mobile.IsNullOrEmpty() && !user.Mobile.StartsWith("WX")) {
                var timeFormat = "yyyy-MM-dd HH:mm:ss";
                //Resolve<IOpenService>().SendRaw(user.Mobile,
                //  $"尊敬的用户{user.UserName}:您于{DateTime.Now.ToString(timeFormat)}提交的工单已受理，感谢您的理解以及对我们工作的支持。祝您生活愉快！");
            }

            return ServiceResult.Success;
        }

        public ServiceResult Delete(ObjectId id) {
            var result = Resolve<IWorkOrderService>().Delete(u => u.Id == id);
            if (result.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("删除失败");
            }
            return ServiceResult.Success;
        }

        public PagedList<WorkOrder> GetPageList(object query) {
            var view = Resolve<IWorkOrderService>().GetPageList(query);
            return view;
        }

        public List<WorkOrder> GetWorkOrdersList() {
            var result = Resolve<IWorkOrderService>().GetList();
            return result.ToList();
        }

        public ServiceResult UpdateWorkOrder(WorkOrder view) {
            var result = Resolve<IWorkOrderService>().Update(view);
            if (result.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("编辑失败");
            }
            return ServiceResult.Success;
        }

        public WorkOrderService(IUnitOfWork unitOfWork, IRepository<WorkOrder, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}