using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Framework.Reports.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Reports.Domain.Repositories
{
    public class ReportRepository : RepositoryMongo<Report, ObjectId>, IReportRepository
    {
        public ReportRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}