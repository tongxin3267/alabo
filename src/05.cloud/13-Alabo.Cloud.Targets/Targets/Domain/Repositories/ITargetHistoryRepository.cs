using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.Data.Targets.Targets.Domain.Entities;

namespace Alabo.Data.Targets.Targets.Domain.Repositories {
	public interface ITargetHistoryRepository : IRepository<TargetHistory, ObjectId>  {
	}
}
