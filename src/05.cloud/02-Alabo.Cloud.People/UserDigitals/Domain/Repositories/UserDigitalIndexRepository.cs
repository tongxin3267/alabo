using Alabo.Cloud.People.UserDigitals.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.People.UserDigitals.Domain.Repositories
{
    public class UserDigitalIndexRepository : RepositoryMongo<UserDigitalIndex, ObjectId>, IUserDigitalIndexRepository
    {
        public UserDigitalIndexRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}