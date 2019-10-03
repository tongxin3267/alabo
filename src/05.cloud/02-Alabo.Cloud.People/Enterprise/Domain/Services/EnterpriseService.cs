using Alabo.Cloud.People.Enterprise.Domain.Dtos;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Mapping;
using Alabo.Users.Enum;
using MongoDB.Bson;

namespace Alabo.Cloud.People.Enterprise.Domain.Services
{
    public class EnterpriseService : ServiceBase<Entities.Enterprise, ObjectId>, IEnterpriseService
    {
        public EnterpriseService(IUnitOfWork unitOfWork, IRepository<Entities.Enterprise, ObjectId> repository) : base(
            unitOfWork, repository)
        {
        }

        public ServiceResult AddOrUpdate(EnterpriseView view)
        {
            if (view == null) {
                throw new ValidException("输入不能为空");
            }

            var enterprise = AutoMapping.SetValue<Entities.Enterprise>(view);
            enterprise.UserId = view.LoginUserId;

            var find = Resolve<IEnterpriseService>().GetSingle(u => u.LicenseNumber == view.LicenseNumber);
            if (find == null)
            {
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

        public EnterpriseView GetView(string id)
        {
            var view = new EnterpriseView();
            if (!id.IsNullOrEmpty())
            {
                var EnterpriseView = Resolve<IEnterpriseService>().GetSingle(u => u.Id == id.ToObjectId());
                view = AutoMapping.SetValue<EnterpriseView>(EnterpriseView);
            }

            return view;
        }
    }
}