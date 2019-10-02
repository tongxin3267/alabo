using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Data.People.Merchants.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.Data.People.Merchants.Domain.Repositories;

namespace Alabo.Data.People.Merchants.Domain.Repositories {
	public class ChainMerchantRepository : RepositoryMongo<ChainMerchant, ObjectId>,IChainMerchantRepository  {
	public  ChainMerchantRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
