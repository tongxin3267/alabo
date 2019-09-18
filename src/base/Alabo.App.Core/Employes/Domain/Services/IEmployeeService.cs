using MongoDB.Bson;
using System;
using Alabo.App.Core.Employes.Domain.Dtos;
using Alabo.App.Core.Employes.Domain.Entities;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Employes.Domain.Services {

    public interface IEmployeeService : IService<Employee, ObjectId> {

        /// <summary>
        /// 员工登录
        /// </summary>
        /// <param name="userOutput"></param>
        /// <returns></returns>
        Tuple<ServiceResult, RoleOuput> Login(UserOutput userOutput);

        /// <summary>
        /// 获取登录的Token
        /// </summary>
        /// <returns></returns>
        string GetLoginToken(GetLoginToken loginToken);

        /// <summary>
        /// 根据Token信息登录
        /// </summary>
        /// <param name="loginByToken"></param>
        /// <returns></returns>
        LoginInput LoginByToken(GetLoginToken loginByToken);
    }
}