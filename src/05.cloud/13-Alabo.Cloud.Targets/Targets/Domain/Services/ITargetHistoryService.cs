using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Data.Targets.Targets.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.Data.Targets.Targets.Domain.Services {
	public interface ITargetHistoryService : IService<TargetHistory, ObjectId>  {
	}
	}
