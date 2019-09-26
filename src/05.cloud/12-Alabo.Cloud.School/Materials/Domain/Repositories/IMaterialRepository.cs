using Alabo.Cloud.School.Materials.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.School.Materials.Domain.Repositories
{
    public interface IMaterialRepository : IRepository<Material, ObjectId>
    {
    }
}