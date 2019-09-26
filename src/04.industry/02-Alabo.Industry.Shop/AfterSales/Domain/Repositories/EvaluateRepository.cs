using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.AfterSales.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.AfterSales.Domain.Repositories {
	public class EvaluateRepository : RepositoryMongo<Evaluate, ObjectId>,IEvaluateRepository  {
	public  EvaluateRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
