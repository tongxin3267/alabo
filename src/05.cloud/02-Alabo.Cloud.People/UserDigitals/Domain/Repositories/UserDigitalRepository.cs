using Alabo.Cloud.People.UserDigitals.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.People.UserDigitals.Domain.Repositories
{
    public class UserDigitalRepository : RepositoryMongo<UserDigital, ObjectId>, IUserDigitalRepository
    {
        public UserDigitalRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}