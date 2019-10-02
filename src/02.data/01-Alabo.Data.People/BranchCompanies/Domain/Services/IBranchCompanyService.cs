using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Data.People.BranchCompanys.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.Data.People.BranchCompanys.Domain.Services {
	public interface IBranchCompanyService : IService<BranchCompany, ObjectId>  {
	}
	}
