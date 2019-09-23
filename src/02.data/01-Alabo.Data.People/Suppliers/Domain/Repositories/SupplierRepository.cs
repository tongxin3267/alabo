using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Data.People.Suppliers.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.Data.People.Suppliers.Domain.Repositories;

namespace Alabo.Data.People.Suppliers.Domain.Repositories {
	public class SupplierRepository : RepositoryMongo<Supplier, ObjectId>,ISupplierRepository  {
	public  SupplierRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
