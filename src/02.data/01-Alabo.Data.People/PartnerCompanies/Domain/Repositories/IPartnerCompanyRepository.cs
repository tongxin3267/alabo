using Alabo.Data.People.PartnerCompanies.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.PartnerCompanies.Domain.Repositories {
	public interface IPartnerCompanyRepository : IRepository<PartnerCompany, ObjectId>  {
	}
}
