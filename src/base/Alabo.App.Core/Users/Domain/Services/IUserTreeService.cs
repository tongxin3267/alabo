﻿using System.Collections.Generic;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.User.Domain.Services {

    /// <summary>
    ///     对用户表的组织结构进行操作
    /// </summary>
    public interface IUserTreeService : IService {

        /// <summary>
        ///     获取用户组织架构图
        /// </summary>
        /// <param name="userId">用户Id</param>
        List<UserTree> GetUserTree(long userId);
    }
}