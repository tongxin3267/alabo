using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Data.People.Suppliers.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.Data.People.Suppliers.Domain.Services {
	public interface ISupplierService : IService<Supplier, ObjectId>  {
	}
	}
