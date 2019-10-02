using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Data.People.Regionals.Domain.Entities;

namespace Alabo.Data.People.Regionals.Domain.Services {
	public class RegionalService : ServiceBase<Regional, ObjectId>,IRegionalService  {
	public  RegionalService(IUnitOfWork unitOfWork, IRepository<Regional, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
