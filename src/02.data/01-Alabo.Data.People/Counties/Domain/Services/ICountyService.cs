using Alabo.Data.People.Counties.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Counties.Domain.Services
{
    public interface ICountyService : IService<County, ObjectId>
    {
    }
}