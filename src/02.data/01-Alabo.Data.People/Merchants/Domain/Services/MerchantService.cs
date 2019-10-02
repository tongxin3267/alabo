using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Data.People.Merchants.Domain.Entities;

namespace Alabo.Data.People.Merchants.Domain.Services {
	public class MerchantService : ServiceBase<Merchant, ObjectId>,IMerchantService  {
	public  MerchantService(IUnitOfWork unitOfWork, IRepository<Merchant, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
