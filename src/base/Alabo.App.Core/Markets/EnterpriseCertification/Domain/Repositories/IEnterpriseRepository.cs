using MongoDB.Bson;
using Alabo.App.Core.Markets.EnterpriseCertification.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Markets.EnterpriseCertification.Domain.Repositories {

    public interface IEnterpriseRepository : IRepository<Enterprise, ObjectId> {
    }
}