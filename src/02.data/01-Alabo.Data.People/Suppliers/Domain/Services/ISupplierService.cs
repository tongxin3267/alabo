using Alabo.Data.People.Suppliers.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Suppliers.Domain.Services
{
    public interface ISupplierService : IService<Supplier, ObjectId>
    {
    }
}