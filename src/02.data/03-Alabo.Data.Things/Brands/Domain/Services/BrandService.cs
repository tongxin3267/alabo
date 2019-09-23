using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Data.Things.Brands.Domain.Entities;

namespace Alabo.Data.Things.Brands.Domain.Services {
	public class BrandService : ServiceBase<Brand, ObjectId>,IBrandService  {
	public  BrandService(IUnitOfWork unitOfWork, IRepository<Brand, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
