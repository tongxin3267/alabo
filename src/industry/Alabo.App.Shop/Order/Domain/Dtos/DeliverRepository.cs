using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.App.Shop.Order.Domain.Dtos;

namespace Alabo.App.Shop.Order.Domain.Dtos {
	public class DeliverRepository : RepositoryMongo<Deliver, ObjectId>,IDeliverRepository  {
	public  DeliverRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
