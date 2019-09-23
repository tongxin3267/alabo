using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Cloud.Contracts.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.Cloud.Contracts.Domain.Repositories;

namespace Alabo.Cloud.Contracts.Domain.Repositories {
	public class ContractRecordRepository : RepositoryMongo<ContractRecord, ObjectId>,IContractRecordRepository  {
	public  ContractRecordRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
