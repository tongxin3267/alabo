using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Data.People.Regionals.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.Data.People.Regionals.Domain.Services {
	public interface IRegionalService : IService<Regional, ObjectId>  {
	}
	}
