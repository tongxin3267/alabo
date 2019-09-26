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
                return ServiceResult.FailedWithMessage("���ʧ��");
            }

            var user = Resolve<IUserService>().GetSingle(view.UserId);
            if (!user.Mobile.IsNullOrEmpty() && !user.Mobile.StartsWith("WX")) {
                var timeFormat = "yyyy-MM-dd HH:mm:ss";
                //Resolve<IOpenService>().SendRaw(user.Mobile,
                //  $"�𾴵��û�{user.UserName}:����{DateTime.Now.ToString(timeFormat)}�ύ�Ĺ�����������л��������Լ������ǹ�����֧�֡�ף��������죡");
            }

            return ServiceResult.Success;
        }

        public ServiceResult Delete(ObjectId id) {
            var result = Resolve<IWorkOrderService>().Delete(u => u.Id == id);
            if (result.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("ɾ��ʧ��");
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
                return ServiceResult.FailedWithMessage("�༭ʧ��");
            }
            return ServiceResult.Success;
        }

        public WorkOrderService(IUnitOfWork unitOfWork, IRepository<WorkOrder, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}