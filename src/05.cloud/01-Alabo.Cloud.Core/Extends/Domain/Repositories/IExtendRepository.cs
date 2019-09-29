using _01_Alabo.Cloud.Core.Extends.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace _01_Alabo.Cloud.Core.Extends.Domain.Repositories
{
    public interface IExtendRepository : IRepository<Extend, ObjectId>
    {
    }
}