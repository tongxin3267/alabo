using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.Data.People.Regionals.Domain.Entities;

namespace Alabo.Data.People.Regionals.Domain.Repositories {
	public interface IRegionalRepository : IRepository<Regional, ObjectId>  {
	}
}
