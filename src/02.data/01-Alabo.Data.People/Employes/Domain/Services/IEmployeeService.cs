using System;
using Alabo.Data.People.Employes.Domain.Entities;
using Alabo.Data.People.Employes.Dtos;
using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Employes.Domain.Services {

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