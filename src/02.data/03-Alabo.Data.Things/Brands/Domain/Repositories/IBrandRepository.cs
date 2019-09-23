using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.Data.Things.Brands.Domain.Entities;

namespace Alabo.Data.Things.Brands.Domain.Repositories {
	public interface IBrandRepository : IRepository<Brand, ObjectId>  {
	}
}
