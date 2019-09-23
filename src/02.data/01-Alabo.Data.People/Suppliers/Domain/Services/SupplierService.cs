using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Data.People.Suppliers.Domain.Entities;

namespace Alabo.Data.People.Suppliers.Domain.Services {
	public class SupplierService : ServiceBase<Supplier, ObjectId>,ISupplierService  {
	public  SupplierService(IUnitOfWork unitOfWork, IRepository<Supplier, ObjectId> repository) : base(unitOfWork, repository){
	}
	}
}
