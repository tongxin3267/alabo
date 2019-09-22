using Microsoft.EntityFrameworkCore;
using Alabo.Datas.Ef.SqlServer;

namespace Alabo.Datas.UnitOfWorks.SqlServer
{
    /// <summary>
    ///     工作单元
    /// </summary>
    public class SqlServerUnitOfWork : MsSqlUnitOfWork, IUnitOfWork
    {
        /// <summary>
        ///     初始化工作单元
        /// </summary>
        /// <param name="options">配置项</param>
        /// <param name="unitOfWorkManager">工作单元服务</param>
        public SqlServerUnitOfWork(DbContextOptions options, IUnitOfWorkManager unitOfWorkManager) : base(options,
            unitOfWorkManager)
        {
        }
    }
}