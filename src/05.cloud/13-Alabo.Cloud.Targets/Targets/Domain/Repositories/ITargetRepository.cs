using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.Cloud.Targets.Targets.Domain.Entities;

namespace Alabo.Cloud.Targets.Targets.Domain.Repositories {
	public interface ITargetRepository : IRepository<Target, ObjectId>  {
	}
}
