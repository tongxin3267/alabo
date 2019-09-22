using MongoDB.Bson;
using Alabo.App.Core.Reports.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Reports.Domain.Repositories {

    public class ReportRepository : RepositoryMongo<Report, ObjectId>, IReportRepository {

        public ReportRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}