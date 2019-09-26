using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Agent.Citys.Domain.Entities;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Users.Entities;

namespace Alabo.App.Agent.Citys.Domain.Services
{

    public class CityService : ServiceBase<City, ObjectId>, ICityService
    {

        public CityService(IUnitOfWork unitOfWork, IRepository<City, ObjectId> repository) : base(unitOfWork, repository)
        {
        }

        public City GetCityByUserId(long userId)
        {
            return GetSingle(r => r.UserId == userId);
        }

        public ServiceResult ChangeUserStatus(string userId, string Status)
        {
            var user = Resolve<IUserService>().GetSingle(userId);
            if (user == null)
            {
                return ServiceResult.FailedMessage("用户不存在");
            }
            var model = new User()
            {
                Id = Convert.ToInt32(userId),
                Status = Domains.Enums.Status.Normal
            };
            Resolve<IUserService>().Update(model);

            return ServiceResult.Success;
        }

    }
}