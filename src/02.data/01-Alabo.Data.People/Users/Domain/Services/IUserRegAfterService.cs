﻿using Alabo.Domains.Services;
using Alabo.Users.Entities;

namespace Alabo.Data.People.Users.Domain.Services
{
    /// <summary>
    ///     会员注册以后服务
    /// </summary>
    public interface IUserRegAfterService : IService
    {
        /// <summary>
        ///     会员注册以后，添加队列任务
        /// </summary>
        /// <param name="user">会员用户</param>
        void AddBackJob(User user);

        /// <summary>
        ///     用户注册以后，处理事件
        /// </summary>
        /// <param name="userId"></param>
        void AfterUserRegTask(long userId);
    }
}