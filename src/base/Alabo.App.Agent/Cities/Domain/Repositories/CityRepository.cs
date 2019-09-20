using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Agent.Citys.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.App.Agent.Citys.Domain.Repositories;

namespace Alabo.App.Agent.Citys.Domain.Repositories {
	public class CityRepository : RepositoryMongo<City, ObjectId>,ICityRepository  {
	public  CityRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
