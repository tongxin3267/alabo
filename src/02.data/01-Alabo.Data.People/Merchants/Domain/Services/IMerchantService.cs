using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Data.People.Merchants.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.Data.People.Merchants.Domain.Services {
	public interface IMerchantService : IService<Merchant, ObjectId>  {
	}
	}
