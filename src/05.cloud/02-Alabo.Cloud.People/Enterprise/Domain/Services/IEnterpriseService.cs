using Alabo.Cloud.People.Enterprise.Domain.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.People.Enterprise.Domain.Services {

    public interface IEnterpriseService : IService<Entities.Enterprise, ObjectId> {

        ServiceResult AddOrUpdate(EnterpriseView view);

        EnterpriseView GetView(string id);
    }
}