using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Data.People.BranchCompanys.Domain.Entities;

namespace Alabo.Data.People.BranchCompanys.Domain.Services {
	public class BranchCompanyService : ServiceBase<BranchCompany, ObjectId>,IBranchCompanyService  {
	public  BranchCompanyService(IUnitOfWork unitOfWork, IRepository<BranchCompany, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
