using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Cloud.Contracts.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.Cloud.Contracts.Domain.Services {
	public interface IContractService : IService<Contract, ObjectId>  {
	}
	}
