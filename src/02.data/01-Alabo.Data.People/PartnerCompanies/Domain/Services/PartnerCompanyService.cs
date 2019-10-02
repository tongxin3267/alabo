using Alabo.Data.People.PartnerCompanies.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.PartnerCompanies.Domain.Services {
	public class PartnerCompanyService : ServiceBase<PartnerCompany, ObjectId>,IPartnerCompanyService  {
	public  PartnerCompanyService(IUnitOfWork unitOfWork, IRepository<PartnerCompany, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
