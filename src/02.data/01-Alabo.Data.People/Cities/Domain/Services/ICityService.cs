using Alabo.Data.People.Cities.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Cities.Domain.Services {

    public interface ICityService : IService<City, ObjectId> {

        /// <summary>
        /// ��ȡ���кϻ���
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        City GetCityByUserId(long userId);

        /// <summary>
        /// ����ʵ���̼ҵ�״̬
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ServiceResult ChangeUserStatus(string userId, string Status);
    }
}