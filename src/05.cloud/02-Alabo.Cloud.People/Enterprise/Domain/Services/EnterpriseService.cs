using MongoDB.Bson;
using System;
using Alabo.App.Core.Markets.EnterpriseCertification.Domain.Dtos;
using Alabo.App.Core.Markets.EnterpriseCertification.Domain.Entities;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Mapping;

namespace Alabo.App.Core.Markets.EnterpriseCertification.Domain.Services {

    public class EnterpriseService : ServiceBase<Enterprise, ObjectId>, IEnterpriseService {

        public EnterpriseService(IUnitOfWork unitOfWork, IRepository<Enterprise, ObjectId> repository) : base(unitOfWork, repository) {
        }

        public ServiceResult AddOrUpdate(EnterpriseView view) {
            if (view == null) {
                throw new ValidException("输入不能为空");
            }

            Enterprise enterprise = AutoMapping.SetValue<Enterprise>(view);
            enterprise.UserId = view.LoginUserId;

            var find = Resolve<IEnterpriseService>().GetSingle(u => u.LicenseNumber == view.LicenseNumber);
            if (find == null) {
                enterprise.Status = IdentityStatus.IsPost;
                var addResult = Resolve<IEnterpriseService>().Add(enterprise);
                if (addResult) {
                    return ServiceResult.Success;
                }
                return ServiceResult.Failed;
            }

            enterprise.Status = view.Status;
            var result = Resolve<IEnterpriseService>().Update(enterprise);
            if (result) {
                return ServiceResult.Success;
            }
            return ServiceResult.Failed;
        }

        public EnterpriseView GetView(string id) {
            EnterpriseView view = new EnterpriseView();
            if (!id.IsNullOrEmpty()) {
                var EnterpriseView = Resolve<IEnterpriseService>().GetSingle(u => u.Id == id.ToObjectId());
                view = AutoMapping.SetValue<EnterpriseView>(EnterpriseView);
            }

            return view;
        }
    }
}