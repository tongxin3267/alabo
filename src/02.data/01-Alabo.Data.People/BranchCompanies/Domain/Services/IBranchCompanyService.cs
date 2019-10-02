using Alabo.Data.People.BranchCompanies.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.BranchCompanies.Domain.Services {
	public interface IBranchCompanyService : IService<BranchCompany, ObjectId>  {
	}
	}
