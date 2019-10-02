using Alabo.Data.People.PartnerCompanies.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.PartnerCompanies.Domain.Repositories {
	public class PartnerCompanyRepository : RepositoryMongo<PartnerCompany, ObjectId>,IPartnerCompanyRepository  {
	public  PartnerCompanyRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
