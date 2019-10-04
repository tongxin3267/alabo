using Alabo.Data.People.Employes.Domain.Entities;
using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;
using System;
using Alabo.Framework.Basic.PostRoles.Dtos;
using GetLoginToken = Alabo.Data.People.Employes.Dtos.GetLoginToken;

namespace Alabo.Data.People.Employes.Domain.Services
{
    public interface IEmployeeService : IService<Employee, ObjectId>
    {
        /// <summary>
        ///     员工登录
        /// </summary>
        /// <param name="userOutput"></param>
        /// <returns></returns>
        Tuple<ServiceResult, RoleOuput> Login(UserOutput userOutput);

        /// <summary>
        ///     获取登录的Token
        /// </summary>
        /// <returns></returns>
        string GetLoginToken(GetLoginToken loginToken);

        /// <summary>
        ///     根据Token信息登录
        /// </summary>
        /// <param name="loginByToken"></param>
        /// <returns></returns>
        LoginInput LoginByToken(GetLoginToken loginByToken);

        /// <summary>
        /// 数据初始化
        /// </summary>
        void Init();
    }
}