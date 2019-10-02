using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Data.People.Regionals.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.Data.People.Regionals.Domain.Repositories;

namespace Alabo.Data.People.Regionals.Domain.Repositories {
	public class RegionalRepository : RepositoryMongo<Regional, ObjectId>,IRegionalRepository  {
	public  RegionalRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
