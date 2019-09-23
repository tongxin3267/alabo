using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Data.Things.Brands.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.Data.Things.Brands.Domain.Repositories;

namespace Alabo.Data.Things.Brands.Domain.Repositories {
	public class BrandRepository : RepositoryMongo<Brand, ObjectId>,IBrandRepository  {
	public  BrandRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
