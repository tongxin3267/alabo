using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.Cloud.Contracts.Domain.Entities;

namespace Alabo.Cloud.Contracts.Domain.Repositories {
	public interface IContractRecordRepository : IRepository<ContractRecord, ObjectId>  {
	}
}
