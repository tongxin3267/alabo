using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Data.People.PartnerCompays.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.Data.People.PartnerCompays.Domain.Repositories;

namespace Alabo.Data.People.PartnerCompays.Domain.Repositories {
	public class PartnerCompanyRepository : RepositoryMongo<PartnerCompany, ObjectId>,IPartnerCompanyRepository  {
	public  PartnerCompanyRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
