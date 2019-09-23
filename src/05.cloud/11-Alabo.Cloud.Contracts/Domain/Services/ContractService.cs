using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Cloud.Contracts.Domain.Entities;

namespace Alabo.Cloud.Contracts.Domain.Services {
	public class ContractService : ServiceBase<Contract, ObjectId>,IContractService  {
	public  ContractService(IUnitOfWork unitOfWork, IRepository<Contract, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
