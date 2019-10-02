using Alabo.Data.People.BranchCompanies.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.BranchCompanies.Domain.Services {
	public class BranchCompanyService : ServiceBase<BranchCompany, ObjectId>,IBranchCompanyService  {
	public  BranchCompanyService(IUnitOfWork unitOfWork, IRepository<BranchCompany, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
