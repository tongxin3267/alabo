using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Cloud.Targets.Iterations.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.Cloud.Targets.Iterations.Domain.Services {
	public interface IIterationService : IService<Iteration, ObjectId>  {
	}
	}
