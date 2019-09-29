using Alabo.Data.People.Suppliers.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Suppliers.Domain.Repositories
{
    public interface ISupplierRepository : IRepository<Supplier, ObjectId>
    {
    }
}