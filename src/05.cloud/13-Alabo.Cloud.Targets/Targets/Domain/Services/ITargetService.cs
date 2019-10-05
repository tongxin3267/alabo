using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Cloud.Targets.Targets.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.Cloud.Targets.Targets.Domain.Services {
	public interface ITargetService : IService<Target, ObjectId>  {
	}
	}
