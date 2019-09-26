using System;
using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.Data.People.Users.Domain.Services {

    /// <summary>
    /// 用户注册，与用户登录等基本服务方法
    /// </summary>
    public interface IUserBaseService : IService {

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginInput"></param>
        /// <returns></returns>
        Tuple<ServiceResult, UserOutput> Login(LoginInput loginInput);

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="loginInput"></param>
        /// <returns></returns>
        Tuple<ServiceResult, UserOutput> Reg(RegInput loginInput);
    }
}