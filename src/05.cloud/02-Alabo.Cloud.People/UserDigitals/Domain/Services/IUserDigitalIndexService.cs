using Alabo.Cloud.People.UserDigitals.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.People.UserDigitals.Domain.Services
{
    public interface IUserDigitalIndexService : IService<UserDigitalIndex, ObjectId>
    {
    }
}