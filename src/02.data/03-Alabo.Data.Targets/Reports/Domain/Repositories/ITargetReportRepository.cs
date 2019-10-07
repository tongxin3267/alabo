using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.Data.Targets.Reports.Domain.Entities;

namespace Alabo.Data.Targets.Reports.Domain.Repositories {
	public interface ITargetReportRepository : IRepository<TargetReport, long>  {
	}
}
