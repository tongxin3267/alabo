using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Shop.Order.Domain.Dtos;

namespace Alabo.App.Shop.Order.Domain.Dtos {
	public class DeliverService : ServiceBase<Deliver, ObjectId>,IDeliverService  {
	public  DeliverService(IUnitOfWork unitOfWork, IRepository<Deliver, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
