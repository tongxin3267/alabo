using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Agent.Citys.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Agent.Citys.Domain.Services {

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