using System.Collections.Generic;
using Alabo.Domains.Repositories;
using Alabo.Users.Entities;

namespace Alabo.Framework.Core.Admins.Repositories
{
    public interface ICatalogRepository : IRepository<User, long>
    {
        /// <summary>
        ///     更新数据库
        /// </summary>
        void UpdateDataBase();

        /// <summary>
        ///     会员数据删除
        /// </summary>
        void TruncateTable();

        /// <summary>
        ///     获取所有的Sql表实体
        /// </summary>
        List<string> GetSqlTable();
    }
}