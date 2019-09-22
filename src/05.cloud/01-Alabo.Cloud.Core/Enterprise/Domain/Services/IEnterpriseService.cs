using MongoDB.Bson;
using Alabo.App.Core.Markets.EnterpriseCertification.Domain.Dtos;
using Alabo.App.Core.Markets.EnterpriseCertification.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Markets.EnterpriseCertification.Domain.Services {

    public interface IEnterpriseService : IService<Enterprise, ObjectId> {

        ServiceResult AddOrUpdate(EnterpriseView view);

        EnterpriseView GetView(string id);
    }
}