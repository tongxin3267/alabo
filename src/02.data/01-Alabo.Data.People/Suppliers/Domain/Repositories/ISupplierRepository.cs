using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.Data.People.Suppliers.Domain.Entities;

namespace Alabo.Data.People.Suppliers.Domain.Repositories {
	public interface ISupplierRepository : IRepository<Supplier, ObjectId>  {
	}
}
