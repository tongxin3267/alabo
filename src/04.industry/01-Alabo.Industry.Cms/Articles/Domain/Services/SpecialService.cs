using System.IO;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Files;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Domain.Services
{
    public class SpecialService : ServiceBase<Special, ObjectId>, ISpecialService
    {
        public SpecialService(IUnitOfWork unitOfWork, IRepository<Special, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }

        public ServiceResult AddOrUpdate(Special model)
        {
            var find = new Special();
            if (model.Id.IsObjectIdNullOrEmpty()) {
                find = GetSingle(r => r.Key == model.Key);
            } else {
                find = GetSingle(r => r.Key == model.Key && r.Id != model.Id);
            }

            if (find != null) {
                return ServiceResult.FailedWithMessage("页面已经存在，请重新输入新的标识");
            }

            AddOrUpdate(model, model.Id.IsObjectIdNullOrEmpty());
            return ServiceResult.Success;
        }
    }
}