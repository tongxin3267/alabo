using Alabo.Domains.Repositories;
using Alabo.Framework.Reports.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Reports.Domain.Repositories {

    public interface IReportRepository : IRepository<Report, ObjectId> {
    }
}