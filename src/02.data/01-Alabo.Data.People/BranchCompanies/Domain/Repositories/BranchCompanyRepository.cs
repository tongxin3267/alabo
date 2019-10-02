using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Data.People.BranchCompanys.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.Data.People.BranchCompanys.Domain.Repositories;

namespace Alabo.Data.People.BranchCompanys.Domain.Repositories {
	public class BranchCompanyRepository : RepositoryMongo<BranchCompany, ObjectId>,IBranchCompanyRepository  {
	public  BranchCompanyRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
