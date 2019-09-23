using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Cloud.Contracts.Domain.Entities;

namespace Alabo.Cloud.Contracts.Domain.Services {
	public class ContractRecordService : ServiceBase<ContractRecord, ObjectId>,IContractRecordService  {
	public  ContractRecordService(IUnitOfWork unitOfWork, IRepository<ContractRecord, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
