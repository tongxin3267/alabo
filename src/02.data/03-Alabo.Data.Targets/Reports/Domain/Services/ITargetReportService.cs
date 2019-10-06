using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Data.Targets.Reports.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.Data.Targets.Reports.Domain.Services {
	public interface ITargetReportService : IService<TargetReport, long>  {
	}
	}
