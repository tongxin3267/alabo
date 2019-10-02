using Alabo.Data.People.BranchCompanies.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.BranchCompanies.Domain.Repositories {
	public class BranchCompanyRepository : RepositoryMongo<BranchCompany, ObjectId>,IBranchCompanyRepository  {
	public  BranchCompanyRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
