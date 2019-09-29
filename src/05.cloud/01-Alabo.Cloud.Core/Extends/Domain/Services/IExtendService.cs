using _01_Alabo.Cloud.Core.Extends.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace _01_Alabo.Cloud.Core.Extends.Domain.Services
{
    public interface IExtendService : IService<Extend, ObjectId>
    {
    }
}