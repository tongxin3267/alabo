﻿using System.Collections.Generic;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Admin.Domain.Services {

    /// <summary>
    ///     数据库操作服务
    /// </summary>
    public interface ICatalogService : IService {

        /// <summary>
        ///     数据库维护脚本
        /// </summary>
        void UpdateDatabase();

        /// <summary>
        ///     获取所有的Sql表实体
        /// </summary>
        List<string> GetSqlTable();
    }
}