using Alabo.Data.People.Internals.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Internals.Domain.Services
{
    public interface IInternalService : IService<Internal, ObjectId>
    {
    }
}