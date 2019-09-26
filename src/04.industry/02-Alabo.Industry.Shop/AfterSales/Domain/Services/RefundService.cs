using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.AfterSales.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.AfterSales.Domain.Services {
	public class RefundService : ServiceBase<Refund, ObjectId>,IRefundService  {
	public  RefundService(IUnitOfWork unitOfWork, IRepository<Refund, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
