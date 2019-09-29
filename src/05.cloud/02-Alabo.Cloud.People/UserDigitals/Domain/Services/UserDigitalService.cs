using Alabo.Cloud.People.UserDigitals.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.People.UserDigitals.Domain.Services
{
    public class UserDigitalService : ServiceBase<UserDigital, ObjectId>, IUserDigitalService
    {
        public UserDigitalService(IUnitOfWork unitOfWork, IRepository<UserDigital, ObjectId> repository) : base(
            unitOfWork, repository)
        {
        }
    }
}