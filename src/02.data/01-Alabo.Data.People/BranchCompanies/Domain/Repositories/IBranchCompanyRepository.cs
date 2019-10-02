using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.Data.People.BranchCompanys.Domain.Entities;

namespace Alabo.Data.People.BranchCompanys.Domain.Repositories {
	public interface IBranchCompanyRepository : IRepository<BranchCompany, ObjectId>  {
	}
}
