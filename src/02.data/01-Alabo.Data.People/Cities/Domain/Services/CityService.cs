using Alabo.Data.People.Cities.Domain.Entities;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Users.Entities;
using MongoDB.Bson;
using System;

namespace Alabo.Data.People.Cities.Domain.Services
{
    public class CityService : ServiceBase<City, ObjectId>, ICityService
    {
        public CityService(IUnitOfWork unitOfWork, IRepository<City, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }

        public City GetCityByUserId(long userId)
        {
            return GetSingle(r => r.UserId == userId);
        }

        public ServiceResult ChangeUserStatus(string userId, string status)
        {
            var user = Resolve<IUserService>().GetSingle(userId);
            if (user == null) return ServiceResult.FailedMessage("用户不存在");
            var model = new User
            {
                Id = Convert.ToInt32(userId),
                Status = Domains.Enums.Status.Normal
            };
            Resolve<IUserService>().Update(model);

            return ServiceResult.Success;
        }
    }
}