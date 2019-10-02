using Alabo.Data.People.BranchCompanies.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.BranchCompanies.Domain.Repositories {
	public interface IBranchCompanyRepository : IRepository<BranchCompany, ObjectId>  {
	}
}
