using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Data.People.PartnerCompays.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.Data.People.PartnerCompays.Domain.Services {
	public interface IPartnerCompanyService : IService<PartnerCompany, ObjectId>  {
	}
	}
