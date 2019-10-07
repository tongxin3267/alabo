using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Data.Targets.Reports.Domain.Entities;

namespace Alabo.Data.Targets.Reports.Domain.Services {
	public class TargetReportService : ServiceBase<TargetReport, long>,ITargetReportService  {
	public  TargetReportService(IUnitOfWork unitOfWork, IRepository<TargetReport, long> repository) : base(unitOfWork, repository){
	}
	}
}
