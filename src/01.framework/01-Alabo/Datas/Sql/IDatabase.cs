﻿using Alabo.Aspects;
using System.Data;

namespace Alabo.Datas.Sql {

    /// <summary>
    ///     数据库
    /// </summary>
    [Ignore]
    public interface IDatabase {

        /// <summary>
        ///     获取数据库连接
        /// </summary>
        IDbConnection GetConnection();
    }
}