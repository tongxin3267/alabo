using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Orders.Dtos {
	public class DeliverRepository : RepositoryMongo<Deliver, ObjectId>,IDeliverRepository  {
	public  DeliverRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
