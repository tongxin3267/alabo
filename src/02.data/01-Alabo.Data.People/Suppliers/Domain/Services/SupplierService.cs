using Alabo.Data.People.Suppliers.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Suppliers.Domain.Services
{
    public class SupplierService : ServiceBase<Supplier, ObjectId>, ISupplierService
    {
        public SupplierService(IUnitOfWork unitOfWork, IRepository<Supplier, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }
    }
}