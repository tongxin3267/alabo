using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Agent.Citys.Domain.Entities;

namespace Alabo.App.Agent.Citys.Domain.Repositories {
	public interface ICityRepository : IRepository<City, ObjectId>  {
	}
}
