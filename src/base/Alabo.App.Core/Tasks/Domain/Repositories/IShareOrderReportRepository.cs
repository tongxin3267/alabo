using MongoDB.Bson;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Tasks.Domain.Repositories {

    public interface IShareOrderReportRepository : IRepository<ShareOrderReport, ObjectId> {
    }
}