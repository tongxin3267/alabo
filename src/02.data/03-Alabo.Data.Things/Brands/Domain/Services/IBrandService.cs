using Alabo.Data.Things.Brands.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.Things.Brands.Domain.Services
{
    public interface IBrandService : IService<Brand, ObjectId>
    {
    }
}