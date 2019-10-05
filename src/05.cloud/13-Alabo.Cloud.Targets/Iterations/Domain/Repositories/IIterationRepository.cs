using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.Cloud.Targets.Iterations.Domain.Entities;

namespace Alabo.Cloud.Targets.Iterations.Domain.Repositories {
	public interface IIterationRepository : IRepository<Iteration, ObjectId>  {
	}
}
