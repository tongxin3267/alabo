using Alabo.Cloud.People.UserDigitals.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.People.UserDigitals.Domain.Repositories
{
    public interface IUserDigitalRepository : IRepository<UserDigital, ObjectId>
    {
    }
}