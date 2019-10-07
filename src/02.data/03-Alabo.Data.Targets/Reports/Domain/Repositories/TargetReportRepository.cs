using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Data.Targets.Reports.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.Data.Targets.Reports.Domain.Repositories;

namespace Alabo.Data.Targets.Reports.Domain.Repositories
{
    public class TargetReportRepository : RepositoryEfCore<TargetReport, long>, ITargetReportRepository
    {
        public TargetReportRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}