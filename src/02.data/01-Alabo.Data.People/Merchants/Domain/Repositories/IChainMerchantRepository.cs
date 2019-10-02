using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.Data.People.Merchants.Domain.Entities;

namespace Alabo.Data.People.Merchants.Domain.Repositories {
	public interface IChainMerchantRepository : IRepository<ChainMerchant, ObjectId>  {
	}
}
