using Alabo.Data.People.Suppliers.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Suppliers.Domain.Repositories
{
    public class SupplierRepository : RepositoryMongo<Supplier, ObjectId>, ISupplierRepository
    {
        public SupplierRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}