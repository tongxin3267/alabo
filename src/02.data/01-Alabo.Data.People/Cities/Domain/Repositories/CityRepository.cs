using Alabo.Data.People.Cities.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Cities.Domain.Repositories {
	public class CityRepository : RepositoryMongo<City, ObjectId>,ICityRepository  {
	public  CityRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
