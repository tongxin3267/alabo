using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Shop.AfterSale.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.App.Shop.AfterSale.Domain.Repositories;

namespace Alabo.App.Shop.AfterSale.Domain.Repositories {
	public class EvaluateRepository : RepositoryMongo<Evaluate, ObjectId>,IEvaluateRepository  {
	public  EvaluateRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
