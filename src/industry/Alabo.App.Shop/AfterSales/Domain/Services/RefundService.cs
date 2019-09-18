using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Shop.AfterSale.Domain.Entities;

namespace Alabo.App.Shop.AfterSale.Domain.Services {
	public class RefundService : ServiceBase<Refund, ObjectId>,IRefundService  {
	public  RefundService(IUnitOfWork unitOfWork, IRepository<Refund, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
