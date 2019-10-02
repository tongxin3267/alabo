using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Data.People.PartnerCompays.Domain.Entities;

namespace Alabo.Data.People.PartnerCompays.Domain.Services {
	public class PartnerCompanyService : ServiceBase<PartnerCompany, ObjectId>,IPartnerCompanyService  {
	public  PartnerCompanyService(IUnitOfWork unitOfWork, IRepository<PartnerCompany, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
