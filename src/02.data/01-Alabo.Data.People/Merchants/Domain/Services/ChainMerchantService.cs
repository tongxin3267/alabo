using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Data.People.Merchants.Domain.Entities;

namespace Alabo.Data.People.Merchants.Domain.Services {
	public class ChainMerchantService : ServiceBase<ChainMerchant, ObjectId>,IChainMerchantService  {
	public  ChainMerchantService(IUnitOfWork unitOfWork, IRepository<ChainMerchant, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
