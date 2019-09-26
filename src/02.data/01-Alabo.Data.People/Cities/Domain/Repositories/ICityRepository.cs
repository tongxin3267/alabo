using Alabo.Data.People.Cities.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Cities.Domain.Repositories {
	public interface ICityRepository : IRepository<City, ObjectId>  {
	}
}
