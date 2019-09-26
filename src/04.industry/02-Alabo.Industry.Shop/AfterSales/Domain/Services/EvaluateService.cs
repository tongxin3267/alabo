using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.AfterSales.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.AfterSales.Domain.Services {
	public class EvaluateService : ServiceBase<Evaluate, ObjectId>,IEvaluateService  {
	public  EvaluateService(IUnitOfWork unitOfWork, IRepository<Evaluate, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
