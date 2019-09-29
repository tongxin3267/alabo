using Alabo.Domains.Services;
using System.Collections.Generic;

namespace Alabo.Framework.Core.Admins.Services
{
    /// <summary>
    ///     数据库操作服务
    /// </summary>
    public interface ICatalogService : IService
    {
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