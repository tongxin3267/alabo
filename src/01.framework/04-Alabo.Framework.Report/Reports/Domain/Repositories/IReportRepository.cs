using MongoDB.Bson;
using Alabo.App.Core.Reports.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Reports.Domain.Repositories {

    public interface IReportRepository : IRepository<Report, ObjectId> {
    }
}