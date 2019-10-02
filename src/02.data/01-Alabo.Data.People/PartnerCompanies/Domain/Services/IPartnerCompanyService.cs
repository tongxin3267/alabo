using Alabo.Data.People.PartnerCompanies.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.PartnerCompanies.Domain.Services {
	public interface IPartnerCompanyService : IService<PartnerCompany, ObjectId>  {
	}
	}
