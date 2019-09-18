using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Shop.AfterSale.Domain.Entities;

namespace Alabo.App.Shop.AfterSale.Domain.Services {
	public class EvaluateService : ServiceBase<Evaluate, ObjectId>,IEvaluateService  {
	public  EvaluateService(IUnitOfWork unitOfWork, IRepository<Evaluate, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
