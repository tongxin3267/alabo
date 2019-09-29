using Alabo.Data.Things.Brands.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.Things.Brands.Domain.Repositories
{
    public interface IBrandRepository : IRepository<Brand, ObjectId>
    {
    }
}