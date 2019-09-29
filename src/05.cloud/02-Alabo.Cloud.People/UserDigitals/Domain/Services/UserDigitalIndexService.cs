using Alabo.Cloud.People.UserDigitals.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.People.UserDigitals.Domain.Services
{
    public class UserDigitalIndexService : ServiceBase<UserDigitalIndex, ObjectId>, IUserDigitalIndexService
    {
        public UserDigitalIndexService(IUnitOfWork unitOfWork, IRepository<UserDigitalIndex, ObjectId> repository) :
            base(unitOfWork, repository)
        {
        }
    }
}